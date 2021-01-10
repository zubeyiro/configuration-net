using System.Reflection;
using ConfigurationNET.Models;
using ConfigurationNET.ParserFactory.Providers;

namespace ConfigurationNET.ParserFactory
{
    public class Parser : IParser
    {
        private Source _source;
        private FileProvider _fileProvider;
        private EnvironmentVariableProvider _environmentVariableProvider;
        private VolumeMountProvider _volumeMountProvider;

        public Parser(Source source)
        {
            _source = source;
            _fileProvider = new FileProvider();
            _environmentVariableProvider = new EnvironmentVariableProvider();
            _volumeMountProvider = new VolumeMountProvider();
        }

        public object Parse(object obj, PropertyInfo property)
        {
            switch (_source)
            {
                case Source.EnvironmentVariable:
                    return _environmentVariableProvider.Parse(obj, property);
                case Source.VolumeMount:
                    return _volumeMountProvider.Parse(obj, property);
            }

            return _fileProvider.Parse(obj, property);
        }
    }
}
