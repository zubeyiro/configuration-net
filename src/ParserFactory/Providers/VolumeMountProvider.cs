using System.IO;
using System.Reflection;

namespace ConfigurationNET.ParserFactory.Providers
{
    public class VolumeMountProvider : IParser
    {
        public object Parse(object obj, PropertyInfo property)
        {
            return ReadMountVolume(property.GetValue(obj).ToString());
        }

        private object ReadMountVolume(string mountPath)
        {
            if (!File.Exists(mountPath))
            {
                return string.Empty;
            }

            return File.ReadAllText(mountPath);
        }
    }
}
