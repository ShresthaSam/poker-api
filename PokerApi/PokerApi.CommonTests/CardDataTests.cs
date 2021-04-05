using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerApi.Common;

namespace PokerApi.CommonTests
{
    [TestClass]
    public class CardDataTests
    {
        [TestMethod]
        public void GetAllCardItems_Should_Return_Valid_List()
        {
            var cards = CardDataStore.GetAllCardItems();
            Assert.IsTrue(cards.Count == 52);
        }
    }
}
