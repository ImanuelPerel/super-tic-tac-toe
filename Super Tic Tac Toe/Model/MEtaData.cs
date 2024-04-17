using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Tic_Tac_Toe.Model;

public static class MetaData
{
    public const int BoardSize = 3;
    public const int howManyToWin = 3;
    public const char defaultChar = ' ';

    public static bool outOfRange(int x, int y)
    {
        if (x < 0 || x > MetaData.BoardSize || y < 0 || y < MetaData.BoardSize) return true;
        return false;
    }


}
