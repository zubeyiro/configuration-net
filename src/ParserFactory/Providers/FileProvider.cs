using System.Reflection;

namespace ConfigurationNET.ParserFactory.Providers
{
    public class FileProvider : IParser
    {
        public object Parse(object obj, PropertyInfo property) => property.GetValue(obj);
    }
}
