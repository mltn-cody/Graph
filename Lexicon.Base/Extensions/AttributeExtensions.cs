using System;
using System.Collections.Generic;
using System.Linq;

namespace Lexicon.Base.Extensions
{
    public static class AttributeExtensions
    {
        public static IEnumerable<TValue> GetAttributeValues<TAttribute, TValue>(
            this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(
                typeof(TAttribute), true).ToList() as List<TAttribute>;

            return att?.Select(valueSelector);
        }
    }
}
