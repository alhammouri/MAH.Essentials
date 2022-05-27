namespace MAH.Essentials.Tests
{
    [TestClass]
    public class IEnumerableExtensionsTests
    {
        [TestMethod]
        public void RunBatchesEmptySource()
        {
            List<string> source = null;

            // Null action is passed
            Asserts.ThrowsException<ArgumentNullException>(() => { source.RunBatches(5, null); });

            // With empty source the action shouldn't be exected
            source.RunBatches(50, (subList, pageNumber) => { Asserts.IsTrue(false); });
        }


        [TestMethod]
        public void RunBatches()
        {
            // To check the number of pages which they got executed
            var ranPages = 0;
            var pageSize = 3;

            var source = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            source.RunBatches(pageSize, (batch, pageNumber) =>
            {
                if (pageNumber == 3)
                {
                    Asserts.AreEqual(source[9], batch.ElementAt(0), "incorrect");
                }
                else
                {
                    // Check the retrieved values
                    var startingIndex = pageNumber * pageSize;
                    Asserts.AreEqual(source[startingIndex], batch.ElementAt(0), "incorrect");
                    Asserts.AreEqual(source[startingIndex + 1], batch.ElementAt(1), "incorrect");
                    Asserts.AreEqual(source[startingIndex + 2], batch.ElementAt(2), "incorrect");
                }

                ranPages++;
            });

            Asserts.AreEqual(4, ranPages, "The paging wasn't working correctly");
        }
    }
}