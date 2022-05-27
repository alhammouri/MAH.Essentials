namespace MAH.Essentials.Tests.Utils
{
    internal class TestItem<T>
    {
        internal T Value { get; set; }
        internal T ExpectedValue { get; set; }

        internal TestItem(T value, T expectedValue)
        {
            Value = value;
            ExpectedValue = expectedValue;
        }
    }
}