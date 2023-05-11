using ConstantsDeduplication.Model;

namespace ConstantsDeduplication.UI
{
    public sealed class ConsoleResultOutput : IResultOutput
    {
        public void Show(IEnumerable<Duplication> duplications)
        {
            foreach (var distr in duplications)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(distr.Value);
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var file in distr.SourceFiles)
                {
                    Console.WriteLine(file);
                }
            }

            Console.WriteLine($"\nTotal: {duplications.Count()}");
        }
    }
}
