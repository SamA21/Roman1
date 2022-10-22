using NUnit.Framework;
using FluentAssert;
using Roman1;

namespace Roaman1.Tests
{
    public class SubtractRomanTests
    {
        private RomanNumbers _romanNumbers = new RomanNumbers();

        [Test]
        [TestCaseSource(nameof(SubtractCasesUnder100))]

        public void SubtractTotalUnder100(RomanValue val1, RomanValue val2, string expectedSym, int expectedVal)
        {
            var result = _romanNumbers.SubtractRomanValue(val1, val2);
            result.Symbol.ShouldBeEqualTo(expectedSym); 
            result.Value.ShouldBeEqualTo(expectedVal);
        }

        [Test]
        [TestCaseSource(nameof(SubtractCases100To1000))]

        public void SubtractTotal100To1000(RomanValue val1, RomanValue val2, string expectedSym, int expectedVal)
        {
            var result = _romanNumbers.SubtractRomanValue(val1, val2);
            result.Symbol.ShouldBeEqualTo(expectedSym);
            result.Value.ShouldBeEqualTo(expectedVal);
        }

        private static object[] SubtractCasesUnder100 =
        {
            new object[] { new RomanValue(2, "II"), new RomanValue(1, "I"), "I", 1 },
            new object[] { new RomanValue(10, "X"), new RomanValue(2, "II"), "VIII", 8 },
            new object[] { new RomanValue(6, "VI"), new RomanValue(5, "V"), "I", 1 }
        };

        private static object[] SubtractCases100To1000 =
      {
            new object[] { new RomanValue(200, "CC"), new RomanValue(100, "C"), "C", 100 },
            new object[] { new RomanValue(500, "D"), new RomanValue(200, "CC"), "CCC", 300 },
            new object[] { new RomanValue(1000, "M"), new RomanValue(500, "D"), "D", 500 }
        };
    }
}