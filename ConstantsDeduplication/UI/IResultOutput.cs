using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConstantsDeduplication.Model;

namespace ConstantsDeduplication.UI
{
    internal interface IResultOutput
    {
        public void Show(IEnumerable<Duplication> duplications);
    }
}
