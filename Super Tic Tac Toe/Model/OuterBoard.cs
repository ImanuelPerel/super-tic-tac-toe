using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace SuperTicTacToe.Model;

public class OuterBoard
{

    private InnerBoard[,] board;
    /// <summary>
    /// will not be able to change from here anything
    /// </summary>
    public IReadOnlyInnerBoard this[int x, int y] { get => (board[x, y]); }

    public Winner Win { get; protected set; } = Winner.NO_ONE_YET;

    /// <summary>
    /// represents the current available inner board , or null if all the outer board is available
    /// </summary>
    public (int, int)? CurrentInnerBoard { get; protected set; } = null;
    public OuterBoard()
    {
        board = new InnerBoard[MetaData.BoardSize, MetaData.BoardSize];
        for (int i = 0; i < MetaData.BoardSize; i++)
        {
            for (int j = 0; j < MetaData.BoardSize; j++)
            {
                board[i, j] = new InnerBoard();
            }
        }
        CurrentInnerBoard = null;
    }
    private int howManyThere(int x, int y, bool player, Direction dir)
    {
        if (MetaData.OutOfRange(x, y)) return 0;
        Winner win = board[x, y].Win;
        if (win.Equals(Winner.NO_ONE_YET) || win.Equals(Winner.TIE) || win.ToBool() != player) return 0;
        return 1 + howManyThere(x + dir.X, y + dir.Y, player, dir);
    }
    public Winner CalcWin(int x, int y)
    {
        if (MetaData.OutOfRange(x, y)) return Winner.NO_ONE_YET;

        Winner winner = board[x, y].Win;
        if (winner.Equals(Winner.NO_ONE_YET))
            Win = Winner.NO_ONE_YET;
        else if (winner.Equals(Winner.TIE))
            if (IsFull())
                Win = Winner.TIE;
            else
                Win = Winner.NO_ONE_YET;
        else
        {
            bool player = winner.ToBool();
            if (-1 + howManyThere(x, y, player, Direction.Up) + howManyThere(x, y, player, Direction.Down) >= MetaData.howManyToWin) Win = winner;
            else if (-1 + howManyThere(x, y, player, Direction.Right) + howManyThere(x, y, player, Direction.Left) >= MetaData.howManyToWin) Win = winner;
            else if (-1 + howManyThere(x, y, player, Direction.UpRight) + howManyThere(x, y, player, Direction.DownLeft) >= MetaData.howManyToWin) Win = winner;
            else if (-1 + howManyThere(x, y, player, Direction.DownLeft) + howManyThere(x, y, player, Direction.UpRight) >= MetaData.howManyToWin) Win = winner;
            else if (IsFull()) { Win = Winner.TIE; }
            else Win = Winner.NO_ONE_YET;
        }
        return Win;
    }
    public bool IsFull()
    {
        // Check if all inner boards have a winner, meaning they are full
        return board.Cast<InnerBoard>().All(innerBoard => !innerBoard.Win.Equals(Winner.NO_ONE_YET));
    }
    public void TakeTurn(bool player, int xInner, int yInner, int xOuter = 0, int yOuter = 0)
    {
        if (MetaData.OutOfRange(xInner, yInner))
            throw new ArgumentOutOfRangeException();
        if (CurrentInnerBoard is not (int, int) currentInnerBoard)
        {
            if (MetaData.OutOfRange(xOuter, yOuter))
                throw new ArgumentOutOfRangeException();
            board[xOuter, yOuter].TakeTurn(player, xInner, yInner);
            CalcWin(xOuter, yOuter);
        }

        else
        {
            board[currentInnerBoard.Item1, currentInnerBoard.Item2].TakeTurn(player, xInner, yInner);
            CalcWin(currentInnerBoard.Item1, currentInnerBoard.Item2);
        }



        //set the new current inner board depending on the xInner and yInner
        if (board[xInner, yInner].Win.Equals(Winner.NO_ONE_YET)) //אם עדיין אפשר לשים ב(xInner,yInner) 
            CurrentInnerBoard = (xInner, yInner);
        else CurrentInnerBoard = null;

    }
}
