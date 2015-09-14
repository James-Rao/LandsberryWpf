using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boggle;

namespace LandsberryWpf3
{
    /// <summary>
    /// 3rd Landsberry problem: Array Random Filler
    /// 
    /// “Write C# code to implement an efficient algorithm to create a 52 element array containing the numbers 1 to 52, 
    /// randomly ordered. Each number should only occur once in the resultant array.
    /// 
    /// Assume that you have access to a function called Random() that generates a random number in a specified range. 
    /// This function will accept two parameters. The first is the LowNumber of the range and the second is the HighNumber of the range. 
    /// The function will return a value that is >= LowNumber and <=HighNumber.”
    /// 
    /// I have written a very flexible class before, which can perfectly solve this Array Random Filler problem. 
    /// I have referenced it from the project Boggle, which you can regard as a demo for 1st Landsberry problem.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            const int ElementCount = 52;
            int[] aInts = new int[ElementCount];

            // init a list with  1 - 52
            List<int> lInts = new List<int>();
            for (int i = 1; i <= ElementCount; i++)
            {
                lInts.Add(i);
            }

            // test
            while (true)
            {
                RandomPool<int> intPool = new RandomPool<int>(lInts);
                int aIntsIndex = 0;
                for (int i = 0; i < lInts.Count; i++)
                {
                    aInts[aIntsIndex] = intPool.GetLeftItem();
                    ++aIntsIndex;
                }

                // display
                Console.WriteLine();
                Console.WriteLine("----------Result----------");
                foreach (var item in aInts)
                {
                    Console.Write(item + ",");
                }
                Console.WriteLine();

                // test right
                Console.WriteLine("----------Sort to check----------");
                Array.Sort(aInts);
                foreach (var item in aInts)
                {
                    Console.Write(item + ",");
                }
                Console.WriteLine();

                // continue 
                Console.WriteLine("----------Test Again ? Y/N----------");
                string s = Console.ReadLine();
                if (s.ToLower() == "n")
                {
                    break;
                }
            }
            
        }
    }
}
