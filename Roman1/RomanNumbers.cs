namespace Roman1
{
    public class RomanNumbers
    {
        private static RomanValue One = new RomanValue(1, "I");
        private static RomanValue Five = new RomanValue(5, "V");
        private static RomanValue Ten = new RomanValue(10, "X");
        private static RomanValue Fifty = new RomanValue(50, "L");
        private static RomanValue Hundred = new RomanValue(100, "C");
        private static RomanValue FiveHundred = new RomanValue(500, "D");
        private static RomanValue Thousand = new RomanValue(1000, "M");

        private static readonly RomanValue[] NumerableArray = new RomanValue[7] { Thousand, FiveHundred, Hundred, Fifty, Ten, Five, One };
        private static readonly RomanValue[,] FourPairs = new RomanValue[,] { { Hundred, FiveHundred }, { Ten, Fifty }, { One, Five }  };
        private static readonly RomanValue[,] NinePairs = new RomanValue[,] { { Hundred, Thousand }, { Ten, Hundred }, { One, Ten } };

        public string ConvertNumberToRoman(int val)
        {
            var completeSymbol = "";
            while (val > 0)
            {
                var digitString = val.ToString().First().ToString();
                var digit = int.Parse(digitString);

                switch (digit)
                {
                    default:
                        var loopValues = StandardLoop(val);
                        completeSymbol += loopValues.Item1;
                        val = loopValues.Item2;
                        break;
                    case 4:
                        var loopValues4 = FourNineLoop(val, FourPairs);
                        completeSymbol += loopValues4.Item1;
                        val = loopValues4.Item2;
                        break;
                    case 9:
                        var loopValues9 = FourNineLoop(val, NinePairs);
                        completeSymbol += loopValues9.Item1;
                        val = loopValues9.Item2;
                        break;
                }
            }
            return completeSymbol;
        }
        
        private (string, int) FourNineLoop(int currentVal, RomanValue[,] pairs)
        {
            var returnString = "";
            var val = currentVal;
            if (val > 1000)
            {
                return StandardLoop(val);
            }
            else
            {
                var zeros = val.ToString().Length - 1;
                var currentDigitString = val.ToString().First();
                var zeroString = MultipleSymbols(zeros, "0");
                var testValueString = currentDigitString + zeroString;
                var testValue = int.Parse(testValueString);
                for (var i = 0; i < pairs.Length / 2; i++)
                {
                    if (testValue == pairs[i, 1].Value - pairs[i, 0].Value)
                    {
                        returnString = pairs[i, 0].Symbol + pairs[i, 1].Symbol;
                        val = val - (pairs[i, 1].Value - pairs[i, 0].Value);
                    }
                }
            }
            return (returnString, val);
        }

        private (string, int) StandardLoop(int val)
        {
            var returnString = "";
            bool skip = false;
            for (var x = 0; x < NumerableArray.Length; x++)
            {
                if (!skip)
                {
                    var count = val / NumerableArray[x].Value;
                    var noLimit = false;
                    if (val > 4000)
                    {
                        noLimit = true;
                    }

                    if (count > 0 && ((count <= 3 && !noLimit) || noLimit))
                    {
                        returnString = returnString + MultipleSymbols(count, NumerableArray[x].Symbol);
                        val -= count * NumerableArray[x].Value;
                        var lastVal = val.ToString().Last();
                        var lastValDigit = lastVal.ToString();
                        if (int.Parse(lastValDigit) == 9)
                        {
                            skip = true;
                        }
                    }
                }
            }
            return (returnString, val);
        }

        private string MultipleSymbols(int extra, string symbol)
        {
            var result = "";
            for(var i = 0; i < extra; i++)
            {
                result += symbol;
            }
            return result;
        }
    }
}