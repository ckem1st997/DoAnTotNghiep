using Microsoft.AspNetCore.Mvc.Rendering;
using Share.Base.Core.Extensions;
using Share.Base.Service.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Service.Security
{
    public static class GetKeyRoleHelper
    {
        public static List<SelectListItem> GetKeyItems()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            Assembly assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes()
                .Where(t => t.Namespace == "Share.Base.Service.Security.ListRole");

            foreach (Type type in types)
            {
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

                foreach (FieldInfo field in fields)
                {
                    string value = field.GetValue(null).ToString();
                    string text = GetKeyNameRoleAttributeText(field);
                    if (!text.IsNullOrEmpty())
                        items.Add(new SelectListItem { Value = value, Text = text });
                }
            }

            return items;
        }

        private static string GetKeyNameRoleAttributeText(FieldInfo field)
        {
            var attributes = field.GetCustomAttributes(typeof(KeyNameRoleAttribute), false);

            if (attributes.Length > 0)
            {
                return ((KeyNameRoleAttribute)attributes.FirstOrDefault()).Name;
            }

            return string.Empty;
        }
    }
}
