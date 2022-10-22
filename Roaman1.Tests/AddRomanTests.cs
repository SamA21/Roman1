using NUnit.Framework;
using FluentAssert;
using Roman1;

namespace Roaman1.Tests
{
    public class AddRomanTests
    {
        private RomanNumbers _romanNumbers = new RomanNumbers();

        [Test]
        [TestCaseSource(nameof(AddCasesUnder100))]

        public void AddTotalUnder100(RomanValue val1, RomanValue val2, string expectedSym, int expectedVal)
        {
            var result = _romanNumbers.AddRomanValue(val1, val2);
            result.Symbol.ShouldBeEqualTo(expectedSym); 
            result.Value.ShouldBeEqualTo(expectedVal);
        }

        [Test]
        [TestCaseSource(nameof(AddCases100To1000))]

        public void AddTotal100To1000(RomanValue val1, RomanValue val2, string expectedSym, int expectedVal)
        {
            var result = _romanNumbers.AddRomanValue(val1, val2);
            result.Symbol.ShouldBeEqualTo(expectedSym);
            result.Value.ShouldBeEqualTo(expectedVal);
        }

        private static object[] AddCasesUnder100 =
        {
            new object[] { new RomanValue(1, "I"), new RomanValue(1, "I"), "II", 2 },
            new object[] { new RomanValue(2, "II"), new RomanValue(2, "II"), "IV", 4 },
            new object[] { new RomanValue(5, "V"), new RomanValue(5, "V"), "X", 10 }
        };

        private static object[] AddCases100To1000 =
      {
            new object[] { new RomanValue(100, "C"), new RomanValue(100, "C"), "CC", 200 },
            new object[] { new RomanValue(300, "CCC"), new RomanValue(100, "C"), "CD", 400 },
            new object[] { new RomanValue(500, "D"), new RomanValue(500, "D"), "M", 1000 }
        };
    }
}