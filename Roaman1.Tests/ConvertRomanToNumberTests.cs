using NUnit.Framework;
using FluentAssert;
using Roman1;

namespace Roaman1.Tests
{
    public class ConvertRomanToNumberTests
    {
        private RomanNumbers _romanNumbers = new RomanNumbers();

        [Test]
        [TestCase(1, "I")]
        [TestCase(5,"V")]
        [TestCase(10, "X")]
        [TestCase(50, "L")]
        [TestCase(100, "C")]
        [TestCase(500, "D")]
        [TestCase(1000, "M")]
        [TestCase(5000, "V̅")]
        [TestCase(10000, "X̅")]

        public void SingleSymbol(int expected, string val)
        {
            var result = _romanNumbers.ConvertRomanToNumber(val);
            result.ShouldBeEqualTo(expected);
        }

        [Test]
        [TestCase(2, "II")]
        [TestCase(3, "III")]
        [TestCase(6, "VI")]
        [TestCase(7, "VII")]
        [TestCase(8, "VIII")]
        [TestCase(58, "LVIII")]
        [TestCase(358, "CCCLVIII")]
        [TestCase(388, "CCCLXXXVIII")]
        [TestCase(600, "DC")]
        [TestCase(2022, "MMXXII")]

        public void MultiSymbol(int expected, string val)
        {
            var result = _romanNumbers.ConvertRomanToNumber(val);
            result.ShouldBeEqualTo(expected);
        }

        [Test]
        [TestCase(4, "IV")]
        [TestCase(9, "IX")]
        [TestCase(40, "XL")]
        [TestCase(39, "XXXIX")]
        [TestCase(44, "XLIV")]
        [TestCase(45, "XLV")]
        [TestCase(74, "LXXIV")]
        [TestCase(94, "XCIV")]
        [TestCase(249, "CCXLIX")]
        [TestCase(274, "CCLXXIV")]
        [TestCase(399, "CCCXCIX")]
        [TestCase(359, "CCCLIX")]
        [TestCase(400, "CD")]
        [TestCase(444, "CDXLIV")]
        [TestCase(900, "CM")]

        public void PrevNumbers(int expected, string val)
        {
            var result = _romanNumbers.ConvertRomanToNumber(val);
            result.ShouldBeEqualTo(expected);
        }

        [TestCase(4000, "I̅V̅")]
        [TestCase(4321, "I̅V̅CCCXXI")]
        [TestCase(6725, "V̅I̅DCCXXV")]
        [TestCase(9000, "I̅X̅")]
        [TestCase(9521, "I̅X̅DXXI")]
        public void AboveFourK(int expected, string val)
        {
            var result = _romanNumbers.ConvertRomanToNumber(val);
            result.ShouldBeEqualTo(expected);
        }

        [TestCase(10444, "X̅CDXLIV")]
        [TestCase(10999, "X̅CMXCIX")]
        [TestCase(18521, "X̅V̅I̅I̅I̅DXXI")]
        [TestCase(20000, "X̅X̅")]
        [TestCase(30999, "X̅X̅X̅CMXCIX")]
        public void AboveTenK(int expected, string val)
        {
            var result = _romanNumbers.ConvertRomanToNumber(val);
            result.ShouldBeEqualTo(expected);
        }
    }
}