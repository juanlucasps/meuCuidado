using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace meuCuidado.Dominio.Extensions
{
    public static class EnumExtension
    {
        public enum TipoUsuario
        {
            [Description("Idoso")]
            Idoso = 1,

            [Description("Tutor")]
            Tutor = 2,

            [Description("Cuidador")]
            Cuidador = 3,

            [Description("Fisioterapeuta")]
            Fisioterapeuta = 4
        }

        public static IEnumerable<SelectListItem> ToSelectList<TEnum>() where TEnum : struct
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("O tipo fornecido não é um enum.");
            }

            return Enum.GetValues(enumType)
                       .Cast<TEnum>()
                       .Select(e => new SelectListItem
                       {
                           Value = ((int)(object)e).ToString(),
                           Text = GetEnumDescription(e as Enum)
                       });
        }

        public static string GetEnumDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : value.ToString();
        }
    }
}