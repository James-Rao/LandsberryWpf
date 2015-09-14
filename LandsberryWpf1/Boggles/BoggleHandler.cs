using Boggle.EnglishWordDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Boggle.Boggles
{
    class BoggleHandler
    {
        // database
        IEnWords _enWordsDic;

        // result 
        HashSet<string> _allEnWords = new HashSet<string>();

        // all points
        Stack<BogglePoint> _allPoints = new Stack<BogglePoint>();

        // record used points
        Stack<BogglePoint> _routePoints = new Stack<BogglePoint>();

        // hash table. map point - linked point
        Dictionary<BogglePoint, Stack<BogglePoint>> _pointDictionary = new Dictionary<BogglePoint, Stack<BogglePoint>>();

        public BoggleHandler(BoggleGrid bg)
        {
            _enWordsDic = new EnWordsSample();

            // init Dictionary
            InitDictionary(bg);
        }

        private void InitDictionary(BoggleGrid bg)
        {
            // create 16 points
            for (int i = 0; i < bg.RowLength; i++)
                for (int j = 0; j < bg.ColLength; j++)
                {
                    BogglePoint bp = new BogglePoint() { Letter = bg.Grid[i, j], Coordinate = i + "," + j };
                    _allPoints.Push(bp);
                }

            // link key point with surround points. build relationship
            for (int i = 0; i < bg.RowLength; i++)
                for (int j = 0; j < bg.ColLength; j++)
                {
                    // link 8 points around
                    string coor = i + "," + j;
                    BogglePoint bp = _allPoints.FirstOrDefault(f=>f.Coordinate == coor);
                    LinkAroundPoints(i, j, bg, bp);
                }
        }

        // build relationship. create dictionary: key points-> adjacent points (max 8)
        private void LinkAroundPoints(int i, int j, BoggleGrid bg, BogglePoint keyPoint)
        {
            Stack<BogglePoint> valuePoints = new Stack<BogglePoint>();

            for (int newI = i - 1; newI <= i + 1; newI++)
            {
                if (newI < 0 || newI >= bg.RowLength) 
                {
                    continue;
                }

                for (int newJ = j - 1; newJ <= j + 1; newJ++)
                {
                    if (newJ < 0 || newJ >= bg.ColLength)
                    {
                        continue;
                    }

                    if (newI == i && newJ == j)
                    {
                        continue;
                    }

                    // add to dic
                    var v = _allPoints.FirstOrDefault(f=>f.Coordinate == newI + "," + newJ);
                    valuePoints.Push(v);
                }
            }
            _pointDictionary.Add(keyPoint, valuePoints);
        }

        public HashSet<string> GetAllEnWords()
        {
            foreach (var keyPoint in _pointDictionary.Keys)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Start_With :" + keyPoint.Letter);
                int res = _enWordsDic.CompareTo(keyPoint.Letter);

                if (res >= 0) // keypoint is
                {
                    // add 
                    if (res == 0)
                    {
                        Console.WriteLine(keyPoint.Letter);
                        AddEnWords(keyPoint.Letter);
                    }

                    // 
                    FindAllEnWords(keyPoint, keyPoint.Letter);
                }
            }


            return _allEnWords;
        }

        //Stopwatch w;

        private void FindAllEnWords(BogglePoint keyPoint, string parentWord)
        {
            // 
            //Stopwatch w = Stopwatch.StartNew();
            _routePoints.Push(keyPoint);
            //w.Stop();
            //Console.WriteLine("1: " + w.ElapsedMilliseconds);

            // judge around
            //w = Stopwatch.StartNew();
            Stack<BogglePoint> valuePoints = _pointDictionary[keyPoint];
            //w.Stop();
            //Console.WriteLine("2: " + w.ElapsedMilliseconds);

            foreach (var item in valuePoints)
            {
                //w = Stopwatch.StartNew();
                bool b = _routePoints.Contains(item);
                //w.Stop();
                //Console.WriteLine("3: " + w.ElapsedMilliseconds);

                if (!b) // 

                {
                    //w = Stopwatch.StartNew();
                    string newWord = parentWord + item.Letter;
                    //w.Stop();
                    //Console.WriteLine("4: " + w.ElapsedMilliseconds);

                    //w = Stopwatch.StartNew();
                    int res = _enWordsDic.CompareTo(newWord);
                    //w.Stop();
                    //Console.WriteLine("5: " + w.ElapsedMilliseconds);

                    if (res >= 0) // 
                    {
                        if (res == 0)
                        {
                            Console.WriteLine(newWord);
                            AddEnWords(newWord);
                        }

                        FindAllEnWords(item, newWord);
                    }
                }
            }

            // 
            _routePoints.Pop();
        }

        private void AddEnWords(string s)
        {
            #region whether allow duplicate
            bool bDuplicate = false; // false: now allowed
            #endregion

            if (!bDuplicate)
            {
                var v = _allEnWords.Contains(s);
                if (!v)
                {
                    _allEnWords.Add(s);
                }
            }
        }
    }
}
