using System;
using System.Linq;
using System.Reflection;

namespace AFramework.Helpers
{
    public static class ObjectHelper
    {
        public static object GetValueFromProperty(this object @object, string propertyName)
        {
            var property = @object.GetType().GetProperty(propertyName);

            if (property == null)
            {
                return null;
            }

            return property.GetValue(@object, null);
        }

        public static void CopyProperties(object origin, object destination)
        {
            if (destination == null) throw new ArgumentNullException("destination", "Destination object must first be instantiated.");
            // Loop through each property in the destination17.            
            foreach (var destinationProperty in destination.GetType().GetProperties())
            {// find and set val if we can find a matching property name and matching type in the origin with the origin's value20.                
                if (origin != null && destinationProperty.CanWrite)
                {
                    origin.GetType().GetProperties().Where(x => x.CanRead && (x.Name == destinationProperty.Name && x.PropertyType == destinationProperty.PropertyType))
                        .ToList()
                        .ForEach(x => destinationProperty.SetValue(destination, x.GetValue(origin, null), null));
                }
            }
        }

        public static void SetPropertyValue(object dataItem, string propertyPath, object value)
        {
            int i = 1;

            string[] properties = propertyPath.Split('.');
            PropertyInfo propertyInfo = null;

            foreach (string property in properties)
            {
                propertyInfo = dataItem.GetType().GetProperty(property);

                if (properties.Count() > i)
                {
                    dataItem = propertyInfo.GetValue(dataItem, null);
                }

                i++;
            }

            //if ((value is string) == false)
            //{
            //    value = FormatHelper.Convert(propertyInfo.PropertyType, value);
            //}

            propertyInfo.SetValue(dataItem, value, null);
        }

        public static PropertyInfo GetPropertyInfoFromPath(object dataItem, string propertyPath)
        {
            int i = 1;

            string[] properties = propertyPath.Split('.');
            PropertyInfo propertyInfo = null;

            foreach (string property in properties)
            {
                propertyInfo = dataItem.GetType().GetProperty(property);

                if (properties.Count() > i)
                {
                    dataItem = propertyInfo.GetValue(dataItem, null);
                }

                i++;
            }

            return propertyInfo;
        }
    }
}
