using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Boggle.EnglishWordDictionary
{
    class EnWordsSample : IEnWords
    {
        private string[] EnWords { get; set; }

        public EnWordsSample()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var textStreamReader = new StreamReader(assembly.GetManifestResourceStream("Boggle.EnglishWordDictionary.EnWordsA.txt"));
                string words = textStreamReader.ReadToEnd().ToUpper();
                EnWords = words.Split(new char[] { '\r', '\n', ' '}, StringSplitOptions.RemoveEmptyEntries);
                Array.Sort(EnWords);
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.Message);
            }
        }

        // 
        public int CompareTo(string s)
        {
            return CompareTo(s, 0, EnWords.Length - 1);
        }

        // 0 exist; 1 part; -1 not exist
        // based on 0-index
        private int CompareTo(string s, int beginIndex, int endIndex)
        {
            int result = -2;

            int count = endIndex - beginIndex + 1;
            if (count > 0)
            {
                int midIndex = (count / 2) + beginIndex;
                string midWord = EnWords[midIndex];
                int r = midWord.CompareTo(s);
                if (r == 0) // exist
                {
                    result = 0;
                }
                else if (r == 1) // maybe part. search down.
                {
                    if (midWord.StartsWith(s))
                    {
                        result = 1;
                        int moreResult = CompareTo(s, beginIndex, midIndex - 1);
                        result = ( moreResult == 0 )? moreResult : result;
                    }
                    else 
                    {
                        result = CompareTo(s, beginIndex, midIndex - 1);
                    }
                }
                else // search up
                {
                    result = CompareTo(s, midIndex + 1, endIndex);
                }

                return result;
            }
            else 
            {
                return -1;
            }
        }
    }
}
