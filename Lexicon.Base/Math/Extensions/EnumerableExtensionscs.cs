using System.Collections.Generic;
using System.Data;
using FastMember;

namespace Lexicon.Base.Math.Extensions
{
    public static class EnumerableExtensionscs
    {
        public static DataTable AsTable<T>(this IEnumerable<T> source)
        {
            var dataSetAsTable = new DataTable();
            using (var reader = ObjectReader.Create(source))
            {
                dataSetAsTable.Load(reader);
            }

            return dataSetAsTable;
        }
    }
}
