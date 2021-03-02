using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal static class Expansion
    {
        internal static string ToStringOrNull(this object str)
        {
            if (str != null)
            {
                return str.ToString();
            }
            return string.Empty;
        }
        internal static DateTime ToDateTimeOrNull(this object date) {
            if (date != null && DateTime.TryParse(date.ToString(),out DateTime dt))
            {
                return dt;
            }
            return default(DateTime);
        }
        internal static int ToIntOrNull(this object i)
        {
            if( i!= null&& int.TryParse(i.ToString(),out int _i))
            {
                return _i;
            }
            return 0;
        }


    }
}
