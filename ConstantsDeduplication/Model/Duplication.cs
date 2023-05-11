using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantsDeduplication.Model
{
    public sealed class Duplication
    {
        public string Value { get; private init; }

        public List<string> SourceFiles { get; private init; }

        public Duplication(string value, List<string> sourceFiles)
        {
            Value = value;
            SourceFiles = sourceFiles;
        }
    }
}
