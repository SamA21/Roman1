using NUnit.Framework;
using FluentAssert;
using Roman1;

namespace Roaman1.Tests
{
    public class Tests
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

        public void SingleSymbol(int val, string expected)
        {
            var result = _romanNumbers.ConvertNumberToRoman(val);
            //Assert.That(result, Is.EqualTo(expected));
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
        [TestCase(4321, "MMMMCCCXXI")]

        public void MultiSymbol(int val, string expected)
        {
            var result = _romanNumbers.ConvertNumberToRoman(val);
            //Assert.That(result, Is.EqualTo(expected));
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
        [TestCase(900, "CM")]
        [TestCase(12345, "MMMMMMMMMMMMCCCXLV")]
        public void PrevNumbers(int val, string expected)
        {
            var result = _romanNumbers.ConvertNumberToRoman(val);
            //Assert.That(result, Is.EqualTo(expected));
            result.ShouldBeEqualTo(expected);
        }
    }
}