using System;
using System.Collections;

namespace MerchantGuideToTheGalaxy
{
    class Program
    {
        public static void Main(string[] args)
        {
            string[] input = {  "glob is I", 
                                "prok is V", 
                                "pish is X", 
                                "tegj is L",
                                "glob glob Silver is 34 Credits",
                                "glob prok Gold is 57800 Credits",
                                "pish pish Iron is 3910 Credits",
                                "how much is pish tegj glob glob ?",
                                "how many Credits is glob prok Silver ?",
                                "how many Credits is glob prok Gold ?",
                                "May the force be with you",
                                "how many Credits is glob prok Iron ?",
                                "how much wood could a woodchuck chuck if a woodchuck could chuck wood ?"};

            MerchantGuideToGalaxyConverter converter = new MerchantGuideToGalaxyConverter();
            converter.MerchantGuideToGalaxyConvert(input);
        }
    }
}