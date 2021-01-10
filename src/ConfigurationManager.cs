using System;
using System.Linq;
using ConfigurationNET.Models;
using ConfigurationNET.ParserFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationNET
{
    public class ConfigurationManager
    {
        IConfiguration configuration;

        public ConfigurationManager()
        {
            this.configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true)
                .Build();
        }

        public void Configure<T>(ref IServiceCollection services, string sectionName) where T : class
        {
            services.Configure<T>(this.configuration.GetSection(sectionName));
            services.PostConfigure<T>(opt => opt = (T)SetValues((object)opt));
        }

        private object SetValues(object obj)
        {
            Source source = Source.File;
            var properties = obj.GetType().GetProperties().ToList();
            var sourceProp = properties.Where(q => q.Name == "Source").FirstOrDefault();

            if (sourceProp != null)
            {
                Enum.TryParse(sourceProp.GetValue(obj).ToString(), out source);
            }

            Parser parser = new Parser(source);

            foreach (var p in properties.Where(q => q.Name != "Source"))
            {
                if (!p.PropertyType.FullName.StartsWith("System."))
                {
                    p.SetValue(obj, SetValues(p.GetValue(obj)));

                    continue;
                }

                p.SetValue(obj, parser.Parse(obj, p));
            }

            return obj;
        }
    }
}
