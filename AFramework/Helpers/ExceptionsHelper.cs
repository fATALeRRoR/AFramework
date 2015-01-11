using System;

namespace AFramework.Helpers
{
    public static class ExceptionsHelper
    {
        public static void ExecuteSafe(Action action, string userMessage)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
            }
        }
    }
}
