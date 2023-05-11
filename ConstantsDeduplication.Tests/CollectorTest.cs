using ConstantsDeduplication.Core;

namespace ConstantsDeduplication.Tests
{
    [TestClass]
    public class CollectorTest
    {
        [TestMethod]
        public void SameConstants()
        {
            var duplications = new Deduplicator().Deduplicate(Directory.GetCurrentDirectory());
            Assert.AreEqual(duplications.Count(), 4);
        }
    }
}