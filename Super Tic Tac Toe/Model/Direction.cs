using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Tic_Tac_Toe.Model
{
    public class Direction
    {
        public int x { get { return x; } set { if (value > 0) x = 1; if (value < 0) x = -1; if (value == 0) x = 0; } }
        public int y { get { return y; } set { if (value > 0) y = 1; if (value < 0) y = -1; if (value == 0) y = 0; } }


    }
}
