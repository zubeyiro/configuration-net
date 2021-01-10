using System;
using System.Reflection;

namespace ConfigurationNET.ParserFactory.Providers
{
    public class EnvironmentVariableProvider : IParser
    {
        public object Parse(object obj, PropertyInfo property)
        {
            return Environment.GetEnvironmentVariable(property.GetValue(obj).ToString());
        }
    }
}
