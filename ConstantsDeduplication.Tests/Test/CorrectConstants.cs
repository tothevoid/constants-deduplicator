using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantsDeduplication.Test
{
    public class CorrectConstants
    {
        public const string Value1 = "1337";

        public readonly string Value2 = "1555";

        public static string Value3 { get; } = "1666";

        public string Value4 => "1777";

        public class SubCorrectConstants
        {
            public const string SubValue1 = "12312312";

            public static readonly Guid Test4 = Guid.Parse("313c6dcf-66f0-41f3-89c4-c31df475ea4a");

            public class SubSubSubCorrectConstants
            {
                public const string SubValue13 = "test22";
            }
        }
    }
}
