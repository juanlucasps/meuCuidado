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

        public enum TipoDocumento
        {
            [Description("Foto do Documento")]
            FotoDocumento = 1,

            [Description("Documento de Identificação")]
            Documento = 2,

            [Description("Certificado de Bons Antecedentes")]
            CertificadoBonsAntecedentes = 3,

            [Description("Certificado de Dispensa")]
            CertificadoDispensa = 4
        }

        public enum TipoExtensaoDocumento
        {
            [Description("Imagem JPG")]
            JPG = 1,

            [Description("Imagem PNG")]
            PNG = 2,

            [Description("Documento PDF")]
            PDF = 3,

            [Description("Documento Word")]
            DOCX = 4,

            [Description("Documento Excel")]
            XLSX = 5
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