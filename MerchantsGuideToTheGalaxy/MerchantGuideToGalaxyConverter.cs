using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantGuideToTheGalaxy
{
    internal class MerchantGuideToGalaxyConverter
    {
        private Dictionary<string, string> _galacticNumbers = new Dictionary<string, string>();
        private Dictionary<string, float> _metal = new Dictionary<string, float>();
        private IGalacticNumberSystemConverter Converter = new RomanToDecimalConverter();
        public void MerchantGuideToGalaxyConvert(string[] input)
        {
            foreach(var line in input)
            {
                if ((line[line.Length-1]=='I' || line[line.Length - 1] == 'V' || line[line.Length - 1] == 'X' || line[line.Length - 1] == 'L' || line[line.Length - 1] == 'C' || line[line.Length - 1] == 'D' || line[line.Length - 1] == 'M') && (!line.Contains("how") && !line.Contains("Credits")))
                {
                    GParseAssignNumbers(line);
                }
                else if(!line.Contains("how") && line.Contains("Credits"))
                {
                    GParseMetalValueAssign(line);
                }
                else if(line.Contains("how much is"))
                {
                    GParseNumberToDecimalCalculator(line);
                }
                else if(line.Contains("how many Credits is"))
                {
                    GParseMetalValueCalculator(line);
                }
                else
                {
                    Console.WriteLine("I have no idea what you are talking about");
                }

            }
        }


        private void GParseAssignNumbers(string line)
        {
            string result = line.Split(' ')[0];
            _galacticNumbers.Add(result, line[line.Length - 1].ToString());
        }

        private void GParseMetalValueAssign(string line)
        {
            string metalname = "";
            float creditval = 0;
            string Roman = "";

            foreach (string word in line.Split(' '))
            {
                if (_galacticNumbers.ContainsKey(word))
                {
                    Roman += _galacticNumbers[word];
                }
                else
                {
                    metalname = word;
                    break;
                }
            }
            int num = Converter.ConvertToDecimal(Roman);
            creditval = float.Parse(line.Split("is ")[1].Split(" Credit")[0]);
            _metal.Add(metalname, creditval / num);
        }

        private void GParseNumberToDecimalCalculator(string line)
        {
            string Roman = "";
            string enquire = line.Split("is ")[1];
            foreach (var word in enquire.Split(' '))
            {
                if (_galacticNumbers.ContainsKey(word))
                {
                    Roman += _galacticNumbers[word];
                }
            }
            int num = Converter.ConvertToDecimal(Roman);
        }

        private void GParseMetalValueCalculator(string line)
        {
            string Roman = "";
            string metals = "";
            string enquire = line.Split("is ")[1].Split("?")[0];
            foreach (var word in enquire.Split(' '))
            {
                if (_galacticNumbers.ContainsKey(word))
                {
                    Roman += _galacticNumbers[word];
                }
                else
                {
                    metals = word;
                    break;
                }
            }
            int num = Converter.ConvertToDecimal(Roman);
            Console.WriteLine($"{enquire} is {num * _metal[metals]}");
        }
    }
}
