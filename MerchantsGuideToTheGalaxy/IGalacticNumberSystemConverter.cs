using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantGuideToTheGalaxy
{
    internal interface IGalacticNumberSystemConverter
    {
        public void Validate(string s, Dictionary<char,int> d);
        public int ConvertToDecimal(string s);
    }
}
