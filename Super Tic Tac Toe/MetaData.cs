using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SuperTicTacToe;

public static class MetaData
{
    static public int BoardSize { get; set; } = 3;
    public static int howManyToWin { get; set; } = 3;
    public static char DefualtChar { get; set; } = ' ';

    static public readonly Dictionary<bool?,char > values = new()
    {
        {true,'X' },
        {false,'O' },
        {null,DefualtChar }
    };

    public static bool OutOfRange(int x, int y)
    {
        if (x < 0 || x >= MetaData.BoardSize || y < 0 || y >= MetaData.BoardSize) return true;
        return false;
    }


}
