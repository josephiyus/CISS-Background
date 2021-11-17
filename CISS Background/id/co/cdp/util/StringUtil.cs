using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISS_Background.id.co.cdp.util
{
    public static class StringUtil
    {
        public static string classifiedNumbers(string input)
        {
            string output = "";
            int o = 0;
            foreach (char i in input)
            {
                if (int.TryParse(i.ToString(), out o))
                {
                    if (output.EndsWith("C") || output.Length == 0)
                        output += "N";
                }
                else
                {
                    if (output.EndsWith("N") || output.Length == 0)
                        output += "C";
                }
            }
            return output;
        }
    }
}
