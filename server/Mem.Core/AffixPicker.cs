using System;

namespace Mem.Core
{
    public static class AffixPicker
    {
        public static string Affix(this IGender entity, string values)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (string.IsNullOrWhiteSpace(values))
            {
                return values;
            }

            var arr = values.Split('|');

            return
                arr.Length > 0 && entity.Gender == Gender.Male ? arr[0] :
                arr.Length > 1 && entity.Gender == Gender.Female ? arr[1] :
                arr.Length > 2 && entity.Gender == Gender.Neutral ? arr[2] :
                arr.Length > 3 && entity.Gender == Gender.Plural ? arr[3] :
                values;
        }
    }
}
