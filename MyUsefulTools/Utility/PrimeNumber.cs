using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MyUsefulTools.Utility
{
    class PrimeNumber
    {
        
        public void GetPrimeNumber()
        {
            bool[] IsPrime = new bool[100010];
            for (int i = 2; i < 100000; i++)
            {
                IsPrime[i] = true;
            }
            for (Int64 i = 2; i <= 100000; i += 2)
            {
                if (IsPrime[i] == true)
                {
                    IsPrime[i] = true;
                    for (Int64 j = i * i; j <= 100000; j += i)
                    {
                        IsPrime[j] = false;
                    }
                }
            }
            int n = 200;
            StreamWriter sw = new StreamWriter("I:\\prime.txt");
            for (int i = 0; i < 100000;i++ )
            {
                if (IsPrime[i])
                {
                    sw.WriteLine(i);
                }
            }
            sw.Close();
        }
    }
}
