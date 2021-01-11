# Configuration extension for different environment (.Net)
This extension allows you to read configuration from different sources on different environments, you might need to read config from appsettings.json directly on your local while you need to read same data from environment variable or volume mount in QA/Prod.

I recently needed to do the same then I made this. Idea is to keep everything in same json file with flags.

If I need to read values from appsettings.json;

```
{
  "AWSCredentials": {
    "Source": "File",
    "Key": "My_Key",
    "Secret": "My_Secret"
  }
}
```

Or I need to read them from Environment Variable;

```
{
  "AWSCredentials": {
    "Source": "EnvironmentVariable",
    "Key": "ENV_VARIABLE_THAT_HOLDS_KEY",
    "Secret": "ENV_VARIABLE_THAT_HOLDS_SECRET"
  }
}
```

Or I need to read them from volume mount;

```
{
  "AWSCredentials": {
    "Source": "VolumeMount",
    "Key": "VOLUME_PATH_THAT_HOLDS_KEY",
    "Secret": "VOLUME_PATH_THAT_HOLDS_SECRET"
  }
}
```

Its also optional, you can read some block from file while readin another from environment variable, it also works with nested sources as well.

```
{
  "AWSCredentials": {
    "Source": "File",
    "Key": "My_Key",
    "Secret": "My_Secret"
  },
  "GoogleCredentials": {
    "Source": "EnvironmentVariable",
    "Key": "ENV_VARIABLE_THAT_HOLDS_KEY",
    "Secret": "ENV_VARIABLE_THAT_HOLDS_SECRET"
  },
  "FacebookInfo": {
    "Source": "File",
    "Name": "My_Name_Will_Be_Read_Directly_From_Here",
    "Credentials": {
      "Source": "VolumeMount",
      "Key": "VOLUME_PATH_THAT_HOLDS_KEY",
      "Secret": "VOLUME_PATH_THAT_HOLDS_SECRET"
    }
  }
}
```

# How it works

Add Nuget package: https://www.nuget.org/packages/ConfigurationNET/

Let's assume you are already using Options Pattern (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-5.0) and you have configuration class like below;

```
	public class AWSCredentials
	{
		public static readonly string SectionName = "AWSConfig";
    public string Key { get; set; }
    public string Secret { get; set; }
	}
```

Instead of getting config like this;

```
services.Configure<AWSCredentials>(_configuration.GetSection(AWSCredentials.SectionName));
```

You'll do this;

```
ConfigurationManager config = new ConfigurationManager();

config.Configure<AWSCredentials>(ref services, AWSCredentials.SectionName);
config.Configure<GoogleCredentials>(ref services, GoogleCredentials.SectionName);
```

That's it.