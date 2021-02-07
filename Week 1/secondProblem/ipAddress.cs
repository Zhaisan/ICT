using System;

namespace secondProblem
{
    public class Solution
    {
        public string DefangIPaddr(string address)
        {

            string s = "";
            string res = "[.]";
            for (int i = 0; i < address.Length; i++)
            {
                if (address[i] == '.')
                {
                    s = s + res;
                }
                else
                {

                    s = s + address[i];
                }
            }
            return s;

        }

    }
    
}
