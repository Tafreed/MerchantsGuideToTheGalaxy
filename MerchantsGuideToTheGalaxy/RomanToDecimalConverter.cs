using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantGuideToTheGalaxy
{
    internal class RomanToDecimalConverter : IGalacticNumberSystemConverter
    {
        public int Decimal { get; private set; }
        public int ConvertToDecimal(string Roman)
        {
            try
            {
                ConvertToDecimalHelper(Roman);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Decimal = -1;
            }
            return Decimal;
        }

        protected void ConvertToDecimalHelper(string Roman)
        {
            Decimal = 0;
            Dictionary<char, int> RomanChars = new Dictionary<char, int>() { { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 } };
            if (Roman.Length == 1) 
                Decimal = RomanChars[Roman[0]];
            else if (Roman.Length == 0)
                Decimal= 0;
            else ParseToDecimal(Roman,RomanChars);
        }

        public void Validate(string Roman, Dictionary<char, int> RomanChars)
        {
            int rcount = 0;
            for (int index = 0; index < Roman.Length; index++)
            {
                if (index + 1 < Roman.Length && Roman[index] == Roman[index + 1])
                {
                    rcount++;
                    if (rcount == 3)
                        throw new Exception("Roman Number Invalid");
                }
                else rcount = 0;

                if (index - 2 >= 0 && RomanChars[Roman[index]] > RomanChars[Roman[index - 1]] && RomanChars[Roman[index]] > RomanChars[Roman[index - 2]])
                {
                    throw new Exception("Roman Number Invalid");
                }
            }
        }

        private void ParseToDecimal(string Roman, Dictionary<char, int> RomanChars)
        {
            Validate(Roman, RomanChars);
            for (int index = 0; index < Roman.Length; index++)
            {
                if (index + 1 < Roman.Length && RomanChars[Roman[index]] < RomanChars[Roman[index + 1]])
                {
                    Decimal += RomanChars[Roman[index + 1]] - RomanChars[Roman[index]];
                    index++;
                }
                else
                {
                    Decimal += RomanChars[Roman[index]];
                }
            }
        }
    }
}
