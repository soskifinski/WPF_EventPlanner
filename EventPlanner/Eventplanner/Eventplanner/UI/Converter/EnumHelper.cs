using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Eventplanner.UI.Converter
{
    internal static class EnumHelper
    {
        public static string GetEnumDescriptionOrName(this Enum e)
        {
            var name = e.ToString();
            var memberInfo = e.GetType().GetMember(name)[0];
            var descriptionAttributes = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);

            if (!descriptionAttributes.Any())
                return name;

            return (descriptionAttributes[0] as DescriptionAttribute).Description;
        }
        /// <summary>
        /// Gets the description of a specific enum value.
        /// </summary>
        public static string Description(this Enum eValue)
        {
            var nAttributes = eValue.GetType().GetField(eValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (!nAttributes.Any())
            {
                //TextInfo oTI = CultureInfo.CurrentCulture.TextInfo;
                //return oTI.ToTitleCase(oTI.ToLower(eValue.ToString().Replace("_", " ")));
                return eValue.ToString().Replace("_", " ");
            }

            return (nAttributes.First() as DescriptionAttribute).Description;
        }

        /// <summary>
        /// Returns an enumerable collection of all values and descriptions for an enum type.
        /// </summary>
        public static IEnumerable<EnumValueDescription> GetAllValuesAndDescriptions<TEnum>() where TEnum : Enum //struct, IConvertible, IComparable, IFormattable
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("TEnum must be an Enumeration type");

            return from e in Enum.GetValues(typeof(TEnum)).Cast<Enum>()
                   select new EnumValueDescription() { Value = e, Description = e.Description() };
        }

        public static TEnum StringToEnum<TEnum>(String nameOrDescription) where TEnum : Enum
        {
            var valuesAndDescriptions = GetAllValuesAndDescriptions<TEnum>();

            Enum foundEnum = valuesAndDescriptions.FirstOrDefault(x => x.Value.ToString() == nameOrDescription || x.Description == nameOrDescription)?.Value;
            if (foundEnum is TEnum)
            {
                return (TEnum)foundEnum;
            }
            throw new ArgumentException("Invalid name or description for TEnum");
        }

        public static IEnumerable<EnumValueDescription> GetAllValuesDescriptionsNoUnbekannt(Type t)
        {
            if (!t.IsEnum)
                throw new ArgumentException($"{nameof(t)} must be an enum type");
            var enums = Enum.GetValues(t).Cast<Enum>().Select((e) => new EnumValueDescription() { Value = e, Description = e.Description() }).ToList();
            enums.Remove(enums.Where(x => x.Description == "Unbekannt").FirstOrDefault());
            return enums;
        }
    }

    public class EnumValueDescription
    {
        public Enum Value { get; set; }
        public string Description { get; set; }
    }
}

