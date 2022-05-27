using MAH.Essentials.Tests.Utils;

namespace MAH.Essentials.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void RemoveSpaces()
        {
            var inputStrings = new TestItem<string>[]
            {
                new TestItem<string>("", ""),
                new TestItem<string>(null, ""),
                new TestItem<string>("correct", "correct"),
                new TestItem<string>(" correct  One   ", "correctOne"),
                new TestItem<string>(" correct      One   Another           one", "correctOneAnotherone"),
            };

            foreach (var input in inputStrings)
            {
                Asserts.AreEqual(input.ExpectedValue, input.Value.RemoveSpaces());
            }
        }

        [TestMethod]
        public void RemoveExtraSpaces()
        {
            var inputStrings = new TestItem<string>[]
            {
                new TestItem<string>("", ""),
                new TestItem<string>(null, ""),
                new TestItem<string>("correct", "correct"),
                new TestItem<string>(" correct  One   ", "correct One"),
                new TestItem<string>(" test extra     spaces", "test extra spaces"),
            };

            foreach (var input in inputStrings)
            {
                Asserts.AreEqual(input.ExpectedValue, input.Value.RemoveExtraSpaces());
            }
        }

        [TestMethod]
        public void Capitalize()
        {
            var inputStrings = new TestItem<string>[]
            {
                new TestItem<string>("", ""),
                new TestItem<string>(null, ""),
                new TestItem<string>("correct", "Correct"),
                new TestItem<string>("correct one", "Correct One"),
                new TestItem<string>("correct one", "Correct One"),
                new TestItem<string>("CORRECT ONE", "CORRECT ONE"),
                new TestItem<string>("  correct  One  ", "Correct One"),
                new TestItem<string>("  correct&one  ", "Correct&one"),
                new TestItem<string>(" test extra     spaces", "Test Extra Spaces"),
            };

            foreach (var input in inputStrings)
            {
                Asserts.AreEqual(input.ExpectedValue, input.Value.Capitalize());
            }
        }

        [TestMethod]
        public void CapitalizeWithSpecialCharacters()
        {
            var inputStrings = new TestItem<string>[]
            {
                new TestItem<string>("", ""),
                new TestItem<string>(null, ""),
                new TestItem<string>("correct", "Correct"),
                new TestItem<string>("correct \n\t\t\n one", "Correct One"),
                new TestItem<string>("  correct  One  ", "Correct One"),
                new TestItem<string>("  correct&one  ", "Correct&One"),
                new TestItem<string>("  correct-one  ", "Correct-One"),
                new TestItem<string>("  correct-one  And not-correct one ", "Correct-One And Not-Correct One"),
                new TestItem<string>("  CORRECT-ONE  AND NOT-CORRECT ONE ", "CORRECT-ONE AND NOT-CORRECT ONE"),
                new TestItem<string>(" test extra     spaces", "Test Extra Spaces"),
            };

            foreach (var input in inputStrings)
            {
                Asserts.AreEqual(input.ExpectedValue, input.Value.CapitalizeWithSpecialCharacters());
            }
        }

        [TestMethod]
        public void ToCamelCase()
        {
            var inputStrings = new TestItem<string>[]
            {
                new TestItem<string>("", ""),
                new TestItem<string>(null, ""),
                new TestItem<string>("correct", "Correct"),
                new TestItem<string>("correct one", "CorrectOne"),
                new TestItem<string>("  correct  One  ", "CorrectOne"),
                new TestItem<string>("  correct&one  ", "Correct&One"),
                new TestItem<string>("  correct-one  ", "Correct-One"),
                new TestItem<string>("  correct-one  And not-correct one ", "Correct-OneAndNot-CorrectOne"),
                new TestItem<string>(" test extra     spaces", "TestExtraSpaces"),
            };

            foreach (var input in inputStrings)
            {
                Asserts.AreEqual(input.ExpectedValue, input.Value.ToCamelCase());
            }
        }

        [TestMethod]
        public void AddSpacesAroundNumbers()
        {
            var inputStrings = new TestItem<string>[]
            {
                new TestItem<string>("", ""),
                new TestItem<string>(null, ""),
                new TestItem<string>("correct55", "correct 55"),
                new TestItem<string>("55correct", "55 correct"),
                new TestItem<string>("correct  55  one", "correct 55 one"),
                new TestItem<string>("  correct&one77", "correct&one 77"),
                new TestItem<string>("  correct-1504545-one  ", "correct-1504545-one"),
                new TestItem<string>(" test extra     spaces1990", "test extra spaces 1990"),
            };

            foreach (var input in inputStrings)
            {
                Asserts.AreEqual(input.ExpectedValue, input.Value.AddSpacesAroundNumbers());
            }
        }

        [TestMethod]
        public void Sanitize()
        {
            string fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                                  <entity name='account'>
                                    <attribute name='name' />
                                    <attribute name='primarycontactid' />
                                    <attribute name='telephone1' />
                                    <attribute name='accountid' />
                                    <order attribute='name' descending='false' />
                                    <filter type='and'>
                                      <condition attribute='name' operator='eq' value='test' />
                                      <condition attribute='numberofemployees' operator='eq' value='50' />
                                    </filter>
                                  </entity>
                                </fetch>";

            var expected = "<fetchversion=\"1.0\"output-format=\"xml-platform\"mapping=\"logical\"distinct=\"false\">" +
                "<entityname=\"account\"><attributename=\"name\"/><attributename=\"primarycontactid\"/>" +
                "<attributename=\"telephone1\"/><attributename=\"accountid\"/><orderattribute=\"name\"descending=\"false\"/>" +
                "<filtertype=\"and\"><conditionattribute=\"name\"operator=\"eq\"value=\"test\"/>" +
                "<conditionattribute=\"numberofemployees\"operator=\"eq\"value=\"50\"/></filter></entity></fetch>";

            Asserts.AreEqual(expected, fetchXml.Sanitize());
        }

        [TestMethod]
        public void NormalizeGermanText()
        {
            var inputStrings = new TestItem<string>[]
            {
                new TestItem<string>("", ""),
                new TestItem<string>(null, ""),
                new TestItem<string>("müller", "mueller"),
                new TestItem<string>("Die straße ist natürlich geschloßen", "die strasse ist natuerlich geschlossen"),
                new TestItem<string>("ülöäßü+", "ueloeaessue+"),
            };

            foreach (var input in inputStrings)
            {
                Asserts.AreEqual(input.ExpectedValue, input.Value.NormalizeGermanText());
            }
        }

        [TestMethod]
        public void FormatWithMask()
        {
            var inputStrings = new TestItem<string>[]
            {
                new TestItem<string>("", ""),
                new TestItem<string>(null, ""),
                new TestItem<string>("1234561", "Value: 123456-1"),
                new TestItem<string>("Test", "Value: Test-"),
                new TestItem<string>(" input", "Value: input-"),
            };

            foreach (var input in inputStrings)
            {
                Asserts.AreEqual(input.ExpectedValue, input.Value.FormatWithMask("Value: ######-#"));
            }
        }

        [TestMethod]
        public void In_EmptyValues()
        {
            string[] emptyArray = null;
            string emptyString = null;

            var filledArray = new string[] { "anything" };
            var filledString = "test";

            Asserts.IsFalse(emptyString.In(emptyArray));
            Asserts.IsFalse(emptyString.In(filledArray));
            Asserts.IsFalse(filledString.In(emptyArray));
            Asserts.IsFalse(filledString.In(filledArray));
        }

        [TestMethod]
        public void In_FilledValues()
        {
            var values = new string[] { "in", "my", "anything" };

            Asserts.IsFalse("In".In(values));
            Asserts.IsTrue("in".In(values));
            Asserts.IsFalse("thing".In(values));
            Asserts.IsTrue("my".In(values));
            Asserts.IsTrue("anything".In(values));
        }

        [TestMethod]
        public void ToEnum()
        {
            Asserts.ThrowsException<ArgumentException>(() => { "anything".ToEnum<TestEnum>(); });
            Asserts.AreEqual(TestEnum.FirstEnumValue, "FirstEnumValue".ToEnum<TestEnum>());
            Asserts.AreEqual(TestEnum.FirstEnumValue, "FIRSTENUMVALUE".ToEnum<TestEnum>());
            Asserts.AreEqual(TestEnum.SecondEnmValue, "SecondEnmValue".ToEnum<TestEnum>());
        }

        [TestMethod]
        public void Right()
        {
            var inputStrings = new TestItem<string>[]
            {
                new TestItem<string>("", ""),
                new TestItem<string>(null, ""),
                new TestItem<string>("1234561", "34561"),
                new TestItem<string>("Test", "Test"),
                new TestItem<string>("input test", "test"),
            };

            foreach (var input in inputStrings)
            {
                Asserts.AreEqual(input.ExpectedValue, input.Value.Right(5));
            }
        }

        [TestMethod]
        public void Left()
        {
            var inputStrings = new TestItem<string>[]
            {
                new TestItem<string>("", ""),
                new TestItem<string>(null, ""),
                new TestItem<string>("1234561", "12345"),
                new TestItem<string>("Test", "Test"),
                new TestItem<string>("input test", "input"),
            };

            foreach (var input in inputStrings)
            {
                Asserts.AreEqual(input.ExpectedValue, input.Value.Left(5));
            }
        }

        [TestMethod]
        public void IsNumber()
        {
            string value = null;
            Asserts.IsFalse(value.IsNumeric());
            Asserts.IsFalse("".IsNumeric());
            Asserts.IsFalse("anything".IsNumeric());

            var numberStrings = new string[] { "01", "50", "115154", "22", "02", "0005" };

            foreach (var numberString in numberStrings)
            {
                Asserts.IsTrue(numberString.IsNumeric(), $"Incorrect result. Value is {numberString}");
            }
        }

        [TestMethod]
        public void IsDecimal()
        {
            string value = null;
            Asserts.IsFalse(value.IsDecimal());
            Asserts.IsFalse("".IsDecimal());
            Asserts.IsFalse("anything".IsDecimal());

            var numberStrings = new string[] { "01.156454", "50,1545", "0.0001", "150", "4,0004" };

            foreach (var numberString in numberStrings)
            {
                Asserts.IsTrue(numberString.IsDecimal(), $"Incorrect result. Value is {numberString}");
            }
        }

        [TestMethod]
        public void IsValidEmailAddress()
        {
            string value = null;
            Asserts.IsFalse(value.IsValidEmailAddress());
            Asserts.IsFalse("".IsValidEmailAddress());
            Asserts.IsFalse("test".IsValidEmailAddress());
            Asserts.IsFalse("test@".IsValidEmailAddress());
            Asserts.IsFalse("test@dm".IsValidEmailAddress());
            Asserts.IsTrue("test@dm.com".IsValidEmailAddress());
            Asserts.IsTrue("test@dm.de".IsValidEmailAddress());
            Asserts.IsTrue("test@anything.dm.de".IsValidEmailAddress());
            Asserts.IsTrue("test@anything.dm.com".IsValidEmailAddress());
            Asserts.IsTrue("test-test@anything.dm.de".IsValidEmailAddress());
            Asserts.IsTrue("test_test@anything.dm.de".IsValidEmailAddress());
        }

        [TestMethod]
        public void IsNullOrWhiteSpace()
        {
            string value = null;
            Asserts.IsTrue(value.IsNullOrWhiteSpace());
            Asserts.IsTrue("".IsNullOrWhiteSpace());
            Asserts.IsTrue(" ".IsNullOrWhiteSpace());
            Asserts.IsFalse("a".IsNullOrWhiteSpace());
        }

        [TestMethod]
        public void IsNotNullOrWhiteSpace()
        {
            string value = null;
            Asserts.IsFalse(value.IsNOTNullOrWhiteSpace());
            Asserts.IsFalse("".IsNOTNullOrWhiteSpace());
            Asserts.IsFalse(" ".IsNOTNullOrWhiteSpace());
            Asserts.IsTrue("a".IsNOTNullOrWhiteSpace());
        }

        [TestMethod]
        public void IsBoolean()
        {
            // Shouldn't
            string value = null;
            Asserts.IsFalse(value.IsBoolean());
            Asserts.IsFalse("".IsBoolean());
            Asserts.IsFalse(" ".IsBoolean());
            Asserts.IsFalse("yesco".IsBoolean());
            Asserts.IsFalse("trues".IsBoolean());
            Asserts.IsFalse("anything".IsBoolean());

            // Should
            Asserts.IsTrue("yes".IsBoolean());
            Asserts.IsTrue("Yes".IsBoolean());
            Asserts.IsTrue("ja".IsBoolean());
            Asserts.IsTrue("JA".IsBoolean());
            Asserts.IsTrue("Ja".IsBoolean());
            Asserts.IsTrue("neIN".IsBoolean());
            Asserts.IsTrue("true".IsBoolean());
            Asserts.IsTrue("TRUE".IsBoolean());
            Asserts.IsTrue("False".IsBoolean());
            Asserts.IsTrue("false".IsBoolean());
            Asserts.IsTrue("No".IsBoolean());
            Asserts.IsTrue("NO".IsBoolean());
            Asserts.IsTrue("n".IsBoolean());
            Asserts.IsTrue("N".IsBoolean());
            Asserts.IsTrue("y".IsBoolean());
            Asserts.IsTrue("Y".IsBoolean());
        }

        [TestMethod]
        public void AsBoolean()
        {
            // Shouldn't
            string value = null;
            Asserts.ThrowsException<ArgumentNullException>(() => { value.AsBoolean(); });
            Asserts.ThrowsException<ArgumentNullException>(() => { "".AsBoolean(); });
            Asserts.ThrowsException<ArgumentNullException>(() => { " ".AsBoolean(); });
            Asserts.ThrowsException<ArgumentException>(() => { "yesco".AsBoolean(); });
            Asserts.ThrowsException<ArgumentException>(() => { "trues".AsBoolean(); });

            // Should
            Asserts.IsTrue("yes".AsBoolean());
            Asserts.IsTrue("Yes".AsBoolean());
            Asserts.IsFalse("No".AsBoolean());
            Asserts.IsTrue("ja".AsBoolean());
            Asserts.IsTrue("JA".AsBoolean());
            Asserts.IsTrue("Ja".AsBoolean());
            Asserts.IsFalse("nein".AsBoolean());
            Asserts.IsTrue("true".AsBoolean());
            Asserts.IsTrue("TRUE".AsBoolean());
            Asserts.IsFalse("False".AsBoolean());
            Asserts.IsFalse("false".AsBoolean());
            Asserts.IsTrue("y".AsBoolean());
            Asserts.IsTrue("Y".AsBoolean());
            Asserts.IsFalse("n".AsBoolean());
            Asserts.IsFalse("N".AsBoolean());
        }
    }
}