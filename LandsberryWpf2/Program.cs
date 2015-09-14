using Boggle.Boggles;
using Boggle.EnglishWordDictionary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] values = { "AWOTOT", "BOAJBO", "HNZNHL", "ISTOES", 
                                "TSDIYT", "PSCAOH", "TYTLRE", "ERXIDL",
                                "FAAFPK", "QuUIHNM", "SNEIUE", "AEANGE",
                                "RVTWEN", "TMUOIC", "WEENGH", "LYVRDE"};

            while (true)
            {

                #region get all random letters
                List<string> randoms = new List<string>();
                Random random = new Random();
                for (int i = 0; i < values.Length; i++)
                {
                    int index = random.Next(0, 6);
                    string dice = values[i].Substring(index, 1);
                    if (i == 9)
                    {
                        if (index == 0)
                        {
                            dice = "Qu";
                        }
                        else
                        {
                            dice = values[i].Substring(index + 1, 1);
                        }
                    }
                    randoms.Add(dice);
                }
                #endregion

                #region randomly throw into grid 4 * 4
                RandomPool<string> pool = new RandomPool<string>(randoms);
                string[,] boggle44 = new string[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        boggle44[i, j] = pool.GetLeftItem();
                    }
                }
                #endregion

                #region use special test case ???
                Console.WriteLine("---------------------------\r\nUse Special Test Case ? Y/N");
                string ra = Console.ReadLine();
                if (ra.ToLower().Contains("y"))
                {
                    boggle44[0, 0] = "S";
                    boggle44[0, 1] = "R";
                    boggle44[0, 2] = "E";
                    boggle44[0, 3] = "L";

                    boggle44[1, 0] = "T";
                    boggle44[1, 1] = "V";
                    boggle44[1, 2] = "R";
                    boggle44[1, 3] = "H";

                    boggle44[2, 0] = "S";
                    boggle44[2, 1] = "O";
                    boggle44[2, 2] = "A";
                    boggle44[2, 3] = "H";

                    boggle44[3, 0] = "J";
                    boggle44[3, 1] = "D";
                    boggle44[3, 2] = "S";
                    boggle44[3, 3] = "N";
                }
                #endregion

                #region print test case
                Console.WriteLine("Test Case: ");
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Console.Write(boggle44[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                #endregion

                #region run with 4*4 boggle
                var watch = Stopwatch.StartNew();
                BoggleHandler boggle = new BoggleHandler(new BoggleGrid(boggle44, 4, 4));
                IEnumerable<string> s = boggle.GetAllEnWords();
                watch.Stop();
                #endregion

                #region print result
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("\r\n Result: " + s.Count() + " Words");
                foreach (var item in s)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine();
                Console.WriteLine("Time Eclipsed: " + watch.ElapsedMilliseconds + " milliseconds");
                #endregion

                #region exit or not


                Console.WriteLine("---------------------------\r\nTest Again? Y/N");
                string exit = Console.ReadLine();
                if (exit.ToLower().Contains("n"))
                {
                    break;
                }
                #endregion
            }

        }
    }
}
