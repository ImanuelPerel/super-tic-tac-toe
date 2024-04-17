using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Super_Tic_Tac_Toe.Model;

public class OuterBoard
{


    InnerBoard[,] Board;
    Location currentInnerBoard0;
    (int, int)? currentInnerBoard1=null;
    public OuterBoard()
    {
        Board = new InnerBoard[MetaData.BoardSize, MetaData.BoardSize];
        for (int i = 0; i < MetaData.BoardSize; i++)
        {
            for (int j = 0; j < MetaData.BoardSize; j++)
            {
                Board[i, j] = new InnerBoard();
            }
        }
        currentInnerBoard0 = Location.ALL;
        currentInnerBoard1 = null;
    }
    private int howManyThere(int x, int y, char player, Direction dir)
    {
        if (MetaData.outOfRange(x, y)) return 0;
        if (Board[x, y].Win != player) return 0;
        return 1 + howManyThere(x + dir.x, y + dir.y, player, dir);
    }
    public char? CalcWin(int x, int y)
    {
        ;
        if (MetaData.outOfRange(x, y)) return null;
        char? player1 = Board[x, y].Win;
        if (player1 == null) return null;
        char player = (char)player1;
        if (1 + howManyThere(x, y, player, new Direction { x = 1, y = 0 }) + howManyThere(x, y, player, new Direction { x = -1, y = 0 }) >= MetaData.howManyToWin) return player;
        if (1 + howManyThere(x, y, player, new Direction { x = 1, y = 1 }) + howManyThere(x, y, player, new Direction { x = -1, y = -1 }) >= MetaData.howManyToWin) return player;
        if (1 + howManyThere(x, y, player, new Direction { x = 0, y = 1 }) + howManyThere(x, y, player, new Direction { x = 0, y = -1 }) >= MetaData.howManyToWin) return player;
        if (1 + howManyThere(x, y, player, new Direction { x = -1, y = 1 }) + howManyThere(x, y, player, new Direction { x = 1, y = -1 }) >= MetaData.howManyToWin) return player;
        return null;
    }

    public void TakeTurn(char player, int x, int y, int xBoard = 0, int yBoard = 0)
    {
        if (currentInnerBoard1 == null)
            Board[xBoard, yBoard].TakeTurn(player, x, y);
        else
        { 
            var tmp = ((int, int))currentInnerBoard1;
            Board[tmp.Item1,tmp.Item2].TakeTurn(player, x, y);
        } 
    }
}
