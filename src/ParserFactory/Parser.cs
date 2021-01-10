using System.Reflection;
using ConfigurationNET.Models;
using ConfigurationNET.ParserFactory.Providers;

namespace ConfigurationNET.ParserFactory
{
    public class Parser : IParser
    {
        private IParser _parser;

        public Parser(Source source)
        {
            switch (source)
            {
                case Source.EnvironmentVariable:
                    _parser = new EnvironmentVariableProvider();
                    break;
                case Source.VolumeMount:
                    _parser = new VolumeMountProvider();
                    break;
            }

            _parser = new FileProvider();
        }

        public object Parse(object obj, PropertyInfo property) => _parser.Parse(obj, property);
    }
}
