using System.Reflection;

namespace Bellatrix.Playwright.Settings.Extensions;
public static class TimeoutSettingsExtensions
{
    public static TimeoutSettings InMilliseconds(this TimeoutSettings settings)
    {
        TimeoutSettings clonedObject = new();

        PropertyInfo[] properties = typeof(TimeoutSettings).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            if (property.PropertyType == typeof(int))
            {
                int originalValue = (int)property.GetValue(settings);

                property.SetValue(clonedObject, originalValue * 1000);
            }
        }

        return clonedObject;
    }
}
