namespace MAH.Essentials.Tests
{
    [TestClass]
    public class TimeSpanExtentionsTests
    {
        [TestMethod]
        public void GetTimeSpanSummary()
        {
            var timespan = new TimeSpan(0, 1, 20, 33, 150);
            Asserts.AreEqual("01:20:33:150", timespan.GetTimeSpanSummary(), "Timespan summery is not as expected");
        }
    }
}