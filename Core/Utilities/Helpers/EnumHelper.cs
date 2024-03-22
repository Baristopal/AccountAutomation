using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Core.Utilities.Helpers
{
    public static class EnumHelper<T> where T : struct, Enum // This constraint requires C# 7.3 or later.
    {
        public static T Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static Dictionary<string, string> GetNamesAndDisplayNames(Type enumType)
        {
            var enumNames = enumType.GetEnumNames().ToList();
            var dictionary = new Dictionary<string, string>();
            foreach (var enumName in enumNames)
            {
                var enumDisplayValue = GetDisplayValue(Parse(enumName));
                dictionary.Add(enumName, enumDisplayValue);
            }
            return dictionary;
        }

        private static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            var resourceKeyProperty = resourceManagerProvider.GetProperty(resourceKey,
                BindingFlags.Static | BindingFlags.Public, null, typeof(string),
                new Type[0], null);
            if (resourceKeyProperty != null)
            {
                return (string)resourceKeyProperty.GetMethod.Invoke(null, null);
            }

            return resourceKey; // Fallback with the key name
        }

        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

            if (descriptionAttributes == null) return string.Empty;
            var asd = descriptionAttributes.Length > 0 ? descriptionAttributes[0].Name : value.ToString();
            return asd;
        }
    }
}