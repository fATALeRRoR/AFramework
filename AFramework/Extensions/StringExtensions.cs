namespace AFramework.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Gets the value or default.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string GetValueOrDefault(this string value, string defaultValue = null)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            //Return default
            return defaultValue;
        }

        public static bool HasValue(this string from)
        {
            return !string.IsNullOrWhiteSpace(@from);
        }
    }
}