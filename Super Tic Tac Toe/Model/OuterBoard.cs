using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace SuperTicTacToe.Model;

public class OuterBoard
{


    InnerBoard[,] Board;
    //Location currentInnerBoard0;
    (int, int)? currentInnerBoard1 = null;
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
        //currentInnerBoard0 = Location.ALL;
        currentInnerBoard1 = null;
    }
    private int howManyThere(int x, int y, bool player, Direction dir)
    {
        if (MetaData.OutOfRange(x, y)) return 0;
        if (Board[x, y].Win != player) return 0;
        return 1 + howManyThere(x + dir.x, y + dir.y, player, dir);
    }
    public bool? CalcWin(int x, int y)
    {
        if (MetaData.OutOfRange(x, y)) return InnerBoard.DefultWin;
        bool? player1 = Board[x, y].Win;
        if (player1 == InnerBoard.Empty) return InnerBoard.DefultWin;
        bool player = (bool)player1!;
        if (1 + howManyThere(x, y, player, new Direction { x = 1, y = 0 }) + howManyThere(x, y, player, new Direction { x = -1, y = 0 }) >= MetaData.howManyToWin) return player;
        else if (1 + howManyThere(x, y, player, new Direction { x = 1, y = 1 }) + howManyThere(x, y, player, new Direction { x = -1, y = -1 }) >= MetaData.howManyToWin) return player;
        else if (1 + howManyThere(x, y, player, new Direction { x = 0, y = 1 }) + howManyThere(x, y, player, new Direction { x = 0, y = -1 }) >= MetaData.howManyToWin) return player;
        else if (1 + howManyThere(x, y, player, new Direction { x = -1, y = 1 }) + howManyThere(x, y, player, new Direction { x = 1, y = -1 }) >= MetaData.howManyToWin) return player;
        return InnerBoard.DefultWin;
    }

    public void TakeTurn(bool player, int xInner, int yInner, int xOuter = 0, int yOuter = 0)
    {
        if (MetaData.OutOfRange(xInner, yInner))
            throw new ArgumentOutOfRangeException();
        if (currentInnerBoard1 == null)
        {
            if (MetaData.OutOfRange(xOuter, yOuter))
                throw new ArgumentOutOfRangeException();
            Board[xOuter, yOuter].TakeTurn(player, xInner, yInner);
        }

        else
        {
            var tmp = ((int, int))currentInnerBoard1;
            Board[tmp.Item1, tmp.Item2].TakeTurn(player, xInner, yInner);

            if (Board[xInner, yInner].Win != InnerBoard.DefultWin) //אם עדיין אפשר לשים ב(xInner,yInner) 
                currentInnerBoard1 = (xInner, yInner);
            else currentInnerBoard1 = null;
        }
        CalcWin(xOuter, yOuter);
    }
}
