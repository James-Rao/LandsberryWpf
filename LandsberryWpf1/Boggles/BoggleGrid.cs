using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle.Boggles
{
    class BoggleGrid
    {
        public string[,] Grid { get; set; }
        public int RowLength { get; set; }
        public int ColLength { get; set; }

        public BoggleGrid(string[,] boggle44, int p1, int p2)
        {
            this.Grid = boggle44;
            RowLength = p1;
            ColLength = p2;
        }
    }
}
