using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTicTacToe;

public static class MetaData
{
    public const int BoardSize = 3;
    public const int howManyToWin = 3;
    public const char defaultChar = ' ';

    public static bool OutOfRange(int x, int y)
    {
        if (x < 0 || x >= BoardSize || y < 0 || y >= BoardSize) return true;
        return false;
    }


}
