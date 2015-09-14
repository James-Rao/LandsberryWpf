using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    public class RandomPool<T>
    {
        int begin;
        int end;
        int length;
        List<T> pool = new List<T>();

        public RandomPool(IEnumerable<T> source)
        {
            this.begin = 0;
            this.end = source.Count();
            this.length = end - begin;

            foreach (var item in source)
	        {
		        pool.Add(item);
	        }
        }

        public T GetLeftItem()
        {
            if (length <= 0)
            {
                Console.WriteLine("No Item Left");
                return (T)Convert.ChangeType(0, typeof(T));
            }
            Random r = new Random();
            int index = r.Next(0, length - 1);
            --length;
            T result = pool[index];
            pool.RemoveAt(index);
            return result;
        }
    }
}
