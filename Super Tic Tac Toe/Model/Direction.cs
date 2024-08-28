using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTicTacToe.Model
{
    /// <summary>
    /// represents all kind of two-dimantional direction of up,left,down,right, and all four diagonals
    /// </summary>
    public class Direction
    {

        static public Direction Up = new Direction(0, 1);
        static public Direction Down = new Direction(0, -1);
        static public Direction Right = new Direction(1, 0);
        static public Direction Left = new Direction(-1, 0);
        static public Direction UpRight = new Direction(1, 1);
        static public Direction UpLeft = new Direction(-1, 1);
        static public Direction DownRight = new Direction(1, -1);
        static public Direction DownLeft = new Direction(-1, -1);

        private int x;
        public int X { get => x; set { x = Math.Sign(value); } }

        public int y;
        public int Y { get => y; set { y = Math.Sign(value); } }
        public Direction(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}
