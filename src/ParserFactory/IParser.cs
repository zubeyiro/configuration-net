using System.Reflection;

namespace ConfigurationNET.ParserFactory
{
    public interface IParser
    {
        object Parse(object obj, PropertyInfo property);
    }
}
