using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using ConstantsDeduplication.Model;
using System.Collections.Concurrent;

namespace ConstantsDeduplication.Core
{
    public sealed class Deduplicator
    {
        public IEnumerable<Duplication> Deduplicate(string rootPath)
        {
            var locations = new PathsExtractor().GetLocations(rootPath);
            var chunks = locations.Chunk((int)Math.Ceiling(locations.Count * 1.0 / 5));
            var dictionaryConstantsDistribution = new ConcurrentDictionary<string, List<string>>();

            Parallel.ForEach(chunks, (chunk) =>
            {
                foreach (var file in chunk)
                {
                    var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file));
                    var root = (CompilationUnitSyntax)tree.GetRoot();
                    var modelCollector = new ConstantsCollector();

                    void ModelCollector_OnElementsLoaded(HashSet<string> fileConstants)
                    {
                        foreach (var constant in fileConstants)
                        {
                            if (dictionaryConstantsDistribution.ContainsKey(constant))
                            {
                                dictionaryConstantsDistribution[constant].Add(file);
                            }
                            else
                            {
                                dictionaryConstantsDistribution[constant] = new List<string>() { file };
                            }
                        }
                    }

                    modelCollector.OnElementsLoaded += ModelCollector_OnElementsLoaded;
                    modelCollector.Visit(root);
                }
            });

            return dictionaryConstantsDistribution
                .Where(distribution => distribution.Value.Count > 1)
                .Select(distribution => new Duplication(distribution.Key, distribution.Value))
                .ToList();
        }
    }
}
