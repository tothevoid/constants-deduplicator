using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantsDeduplication.Core
{
    public class PathsExtractor
    {
        public List<string> GetLocations(string root)
        {
            var csharpSources = Directory.GetFiles(root).Where(file => file.EndsWith(".cs")).ToList();

            foreach (var subDir in Directory.GetDirectories(root))
            {
                csharpSources.AddRange(GetLocations(subDir));
            }

            return csharpSources;
        }
    }
}
