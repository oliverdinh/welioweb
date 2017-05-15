using System.ComponentModel;

namespace WaxWelio.Common.Enum
{
    public static class ObjectDescription
    {
        public static string DescriptionAttr<T>(this T source)
        {
            var fi = source.GetType().GetField(source.ToString());

            var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(
                typeof (DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            return source.ToString();
        }
    }
}