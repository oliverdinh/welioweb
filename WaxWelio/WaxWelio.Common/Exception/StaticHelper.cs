using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WaxWelio.Common.Exception
{
    public static class StaticHelper
    {
        public static string DisplayName(this System.Enum value)
        {
            // the following is my variation on the extension method you linked to
            if (value == null)
            {
                return null;
            }
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])field
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return value.ToString();
        }
    }
}
