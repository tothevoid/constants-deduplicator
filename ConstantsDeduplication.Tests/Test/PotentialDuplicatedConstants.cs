using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantsDeduplication.Test
{
    public class PotentialDuplicatedConstants
    {
        public const string Value1 = "1212";

        /// <summary>
        /// Duplicated
        /// </summary>
        public readonly string Value2 = "1555";

        /// <summary>
        /// Duplicated
        /// </summary>
        public static string Value3 { get; } = "1666";

        public string Value4 => "5555";

        public class SubPotentialDuplicatedConstants
        {
            /// <summary>
            /// Duplicated
            /// </summary>
            public const string SubValue1 = "12312312";

            public static readonly Guid Test4 = Guid.Parse("3e3c6dcf-66f0-41f3-89c4-c31df475ea4a");

            public class SubSubPotentialDuplicatedConstants
            {
                /// <summary>
                /// Duplicated
                /// </summary>
                public const string SubSubValue = "test22";
            }
        }
    }
}
