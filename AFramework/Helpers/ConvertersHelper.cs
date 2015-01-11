namespace AFramework.Helpers
{
    public static class ConvertersHelper
    {
        public static int ConvertToInt(string value)
        {
            int result;
            
            if (int.TryParse(value, out result))
            {
                return result;
            }

            return 0;
        }

        public static int? ConvertToNullableInt(string value)
        {
            int result;

            if (int.TryParse(value, out result))
            {
                return result;
            }

            return null;
        }
    }
}
