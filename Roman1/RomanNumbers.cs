namespace Roman1
{
    public sealed class RomanNumbers
    {
        private static RomanValue One = new RomanValue(1, "I");
        private static RomanValue Five = new RomanValue(5, "V");
        private static RomanValue Ten = new RomanValue(10, "X");
        private static RomanValue Fifty = new RomanValue(50, "L");
        private static RomanValue Hundred = new RomanValue(100, "C");
        private static RomanValue FiveHundred = new RomanValue(500, "D");
        private static RomanValue Thousand = new RomanValue(1000, "M");
        private static RomanValue ThousandHat = new RomanValue(1000, "I̅");
        private static RomanValue FiveThousand = new RomanValue(5000, "V̅");
        private static RomanValue TenThousand = new RomanValue(10000, "X̅");

        private static readonly RomanValue[] NumerableArray = new RomanValue[9] { TenThousand, FiveThousand, Thousand, FiveHundred, Hundred, Fifty, Ten, Five, One };
        private static readonly RomanValue[,] FourPairs = new RomanValue[,] { { ThousandHat, FiveThousand }, { Hundred, FiveHundred }, { Ten, Fifty }, { One, Five }  };

        private static readonly RomanValue[,] NinePairs = new RomanValue[,] { { ThousandHat, TenThousand }, { Hundred, Thousand }, { Ten, Hundred }, { One, Ten } };

        private bool InitallyAbove4k = false;

        public RomanValue AddRomanValue(RomanValue number1, RomanValue number2)
        {
            int newInt = number1.Value + number2.Value;
            if (newInt < 40000)
            {
                var romanString = ConvertNumberToRoman(newInt);
                return new RomanValue(newInt, romanString);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Added numbers are unsupported");
            }
        }

        public string AddRomanValue(string numerable1, string numerable2)
        {
            int number1 = ConvertRomanToNumber(numerable1);
            int number2 = ConvertRomanToNumber(numerable2);
            int newInt = number1 + number2;
            if (newInt < 40000)
            {
                return ConvertNumberToRoman(newInt);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Added numbers are unsupported");
            }
        }

        public int ConvertRomanToNumber(string numerable)
        {
            int value = 0;           
            for(var x = 0; x < numerable.Length; x++)
            {
                foreach (var roman in NumerableArray)
                {
                    try
                    {
                        if (numerable[x+1] == '̅')
                        {
                            int? hatVal = NumerableArray.FirstOrDefault(y => y.Symbol == numerable[x].ToString() + numerable[x+1].ToString())?.Value;
                            if (hatVal.HasValue)
                            {
                                value += hatVal.Value;
                                break;
                            }
                            else if (ThousandHat.Symbol == numerable[x].ToString() + numerable[x + 1].ToString())
                            {
                                value += ThousandHat.Value;
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //fail silent as if not hat or too long it shouldn't matter
                    }

                    if (numerable[x].ToString() == roman.Symbol)
                        value += roman.Value;                 
                }               
            }

            if (numerable.Contains("IV") || numerable.Contains("IX"))
                value -= 2;

            if (numerable.Contains("XL") || numerable.Contains("XC"))
                value -= 20;

            if (numerable.Contains("CD") || numerable.Contains("CM"))
                value -= 200;

            if (numerable.Contains("I̅V̅") || numerable.Contains("I̅X̅"))
                value -= 2000;

            return value;
        }

        public string ConvertNumberToRoman(int val)
        {
            InitallyAbove4k = false;
            if (val >= 4000)
                InitallyAbove4k = true;

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
            if (val > 10000)
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
                    if (InitallyAbove4k)
                    {
                        var digitString = val.ToString().First().ToString();
                        var digit = int.Parse(digitString);

                        switch (digit)
                        {
                            default:
                                if (count > 0 && count <= 3)
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
                                break;
                            case 4:
                                var loopValues4 = FourNineLoop(val, FourPairs);
                                returnString += loopValues4.Item1;
                                val = loopValues4.Item2;
                                break;
                            case 9:
                                var loopValues9 = FourNineLoop(val, NinePairs);
                                returnString += loopValues9.Item1;
                                val = loopValues9.Item2;
                                break;
                        }
                    }
                    else{
                        if (count > 0 && count <= 3)
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
            }
            return (returnString, val);
        }
        private string MultipleSymbols(int extra, string symbol)
        {
            if(InitallyAbove4k && Thousand.Symbol == symbol) 
                symbol = ThousandHat.Symbol;

            var result = "";
            for (var i = 0; i < extra; i++)
            {
                result += symbol;
            }
            return result;
        }
    }
}