using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Bellatrix;

public sealed class Settings
{
    private static IConfigurationRoot _root;

    static Settings()
    {
        _root = InitializeConfiguration();
    }

    public static TSection GetSection<TSection>()
      where TSection : class, new()
    {
        string sectionName = MakeFirstLetterToLower(typeof(TSection).Name);
        return _root.GetSection(sectionName).Get<TSection>();
    }

    private static string MakeFirstLetterToLower(string text)
    {
        return char.ToLower(text[0]) + text.Substring(1);
    }

    private static IConfigurationRoot InitializeConfiguration()
    {
        var builder = new ConfigurationBuilder();
        string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    var filesInExecutionDir = Directory.GetFiles(assemblyFolder);
        var settingsFile = filesInExecutionDir.FirstOrDefault(x => x.Equals("testimizeSettings.json"));
        if (settingsFile != null)
        {
            builder.AddJsonFile(settingsFile, optional: true, reloadOnChange: true);
        }

        return builder.Build();
    }
}