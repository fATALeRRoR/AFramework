using System;
using System.Collections.Specialized;
using System.Configuration;

namespace AFramework.Helpers
{
    /// <summary>
    /// Application settings helper
    /// </summary>
    public static class AppSettingsHelper
    {
        private static NameValueCollection _AppSettings;

        static AppSettingsHelper()
        {
            ReadSettings();
        }

        private static void ReadSettings()
        {
            _AppSettings = ConfigurationManager.AppSettings;
        }

        public static bool GetBoolean(string name)
        {
            var val = GetString(name);
            return val.Equals("true");
        }

        public static string GetString(string name)
        {
            return GetValue(name, true, null);
        }

        public static string GetString(string name, string defaultValue)
        {
            return GetValue(name, false, defaultValue);
        }

        public static string[] GetStringArray(string name, string separator)
        {
            return GetStringArray(name, separator, true, null);
        }

        public static string[] GetStringArray(string name, string separator, string[] defaultValue)
        {
            return GetStringArray(name, separator, false, defaultValue);
        }

        private static string[] GetStringArray(string name, string separator, bool requireValue, string[] defaultValue)
        {
            string value = GetValue(name, requireValue, null);

            if (value != null)
                return value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            return defaultValue;
        }

        private static string GetValue(string name, bool requireValue, string defaultValue)
        {
            var value = _AppSettings[name];

            if (value != null)
            {
                return value;
            }

            if (requireValue)
            {
                //Value is null and it is required
                throw new InvalidOperationException(string.Format("Could not find required app setting '{0}'", name));
            }

            return defaultValue;
        }
    }
}