namespace MAH.Essentials.Tests
{
    [TestClass]
    public class DatetimeTests
    {
        [TestMethod]
        public void AsDateTimeStamp()
        {
            var datetime = new DateTime(2022, 11, 25, 5, 40, 9);
            Asserts.AreEqual("202211250540090000", datetime.AsDateTimeStamp(), "Timestamp wasn't correct");
        }

        [TestMethod]
        public void AsGermanShortDate()
        {
            var datetime = new DateTime(2022, 11, 25, 5, 40, 9);
            Asserts.AreEqual("25.11.2022", datetime.AsGermanShortDate());
        }
    }
}