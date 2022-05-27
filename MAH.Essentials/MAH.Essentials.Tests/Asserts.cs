namespace MAH.Essentials.Tests
{
    public static class Asserts
    {
        public static void AreEqual<T>(T expected, T actual)
        {
            InternalAreEqual(expected, actual, $"Expected: {expected}\nActual: {actual}");
        }

        public static void AreEqual<T>(T expected, T value, string message)
        {
            InternalAreEqual(expected, value, $"{message}\nExpected: {expected}\nValue: {value}");
        }

        private static void InternalAreEqual<T>(T? expected, T? actual, string message)
        {
            Assert.AreEqual(expected, actual, message);
        }

        public static void AreNotEqual<T>(T notExpected, T actual)
        {
            InternalAreNotEqual(notExpected, actual, $"\nExpected: {notExpected}\nActual: {actual}");
        }

        public static void AreNotEqual<T>(T notExpected, T value, string message)
        {
            InternalAreNotEqual(notExpected, value, $"\n{message}\nNotExpected:{notExpected}\nValue: {value}");
        }

        private static void InternalAreNotEqual<T>(T notExpected, T value, string message)
        {
            Assert.AreNotEqual(notExpected, value, message);
        }

        public static void IsTrue(bool? condition)
        {
            Assert.IsTrue(condition, "\nExpectValue is true but got false");
        }

        public static void IsTrue(bool? condition, string message)
        {
            Assert.IsTrue(condition, $"\n{message}");
        }

        public static void IsTrue(bool? condition, string message, params object[] args)
        {
            Assert.IsTrue(condition, string.Format("\n{0}", message), args);
        }


        public static void IsFalse(bool? condition)
        {
            Assert.IsFalse(condition, "\nExpectValue is False but got true");
        }

        public static void IsFalse(bool? condition, string message)
        {
            Assert.IsFalse(condition, $"\n{message}");
        }


        public static void ThrowsException<T>(Action action) where T : Exception
        {
            Assert.ThrowsException<T>(action, string.Format("\nExpectedError: {0} but was running successfully", typeof(T)));
        }

        public static void ThrowsException<T>(Action action, string message) where T : Exception
        {
            Assert.ThrowsException<T>(action, $"\n{message}");
        }


        public static void IsNotNull(object value)
        {
            InternalIsNotNull(value, "Null object was not expected");
        }

        public static void IsNotNull(object value, string message)
        {
            InternalIsNotNull(value, message);
        }


        public static void IsNotNull(object value, string message, params object[] args)
        {
            InternalIsNotNull(value, string.Format(message, args));
        }

        private static void InternalIsNotNull(object value, string message)
        {
            if (value is string)
            {
                var valueIsNotEmpty = value != null && value.ToString().IsNOTNullOrWhiteSpace();
                Assert.IsTrue(valueIsNotEmpty, $"\nString IsNotNull check\n{message}");
            }
            else
            {
                Assert.IsNotNull(value, $"\n{message}");
            }
        }

        public static void IsNull(object value)
        {
            InternalIsNull(value, "Null object was not expected");
        }

        public static void IsNull(object value, string message)
        {
            InternalIsNull(value, message);
        }

        public static void IsNull(object value, string message, params object[] args)
        {
            InternalIsNull(value, string.Format(message, args));
        }

        private static void InternalIsNull(object value, string message)
        {
            if (value is string)
            {
                var valueString = value as string;
                Assert.IsTrue(valueString.IsNullOrWhiteSpace(), $"\nString IsNull check\n{message}");
            }
            else
            {
                Assert.IsNull(value, $"\n{message}\nNull object was not expected");
            }
        }
    }
}