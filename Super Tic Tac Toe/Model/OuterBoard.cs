using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using MyCsLibrary0.Games.BoardGames;

namespace SuperTicTacToe.Model;

public class OuterBoard : Board<InnerBoard>
{

    //private InnerBoard[,] board;

    /// <summary>
    /// will not be able to change from here anything
    /// </summary>
    new public IReadOnlyInnerBoard this[int x, int y] { get => (board[x, y]); }

    override public InnerBoard Empty
    {
        get => new InnerBoard();
        protected set
        {
            throw new InvalidOperationException($"Cannot change the value of a read-only ({nameof(Empty)}) property.");
        }
    }

    public Winner Win { get; protected set; } = Winner.NO_ONE_YET;

    /// <summary>
    /// represents the current available inner board , or null if all the outer board is available
    /// </summary>
    public (int, int)? CurrentInnerBoard { get; protected set; } = null;
    public OuterBoard() : base(MetaData.BoardSizeRows, MetaData.BoardSizeCols)
    {
        CurrentInnerBoard = null;
    }
    public Winner CalcWin(int x, int y)
    {
        if (MetaData.OutOfRange(x, y)) return Winner.NO_ONE_YET;

        Winner winner = board[x, y].Win;
        if (winner.Equals(Winner.NO_ONE_YET))
            Win = Winner.NO_ONE_YET;
        else if (winner.Equals(Winner.TIE))
        {
            if (IsFull())
                Win = Winner.TIE;
            else
                Win = Winner.NO_ONE_YET;
        }
        else if (WhichIsThereInNInARowDoubleDirection(x, y, MetaData.HowManyToWinOuter, key: innerBoard => innerBoard.Win) is Winner winnerPlayer)
        {
            Win = winnerPlayer;
        }
        else /*winnerPlayer is null*/if (IsFull())
            Win = Winner.TIE;
        else Win = Winner.NO_ONE_YET;

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
        if (board[xInner, yInner].Win.Equals(Winner.NO_ONE_YET)) // still possible to make a move in (xInner,yInner) 
            CurrentInnerBoard = (xInner, yInner);
        else CurrentInnerBoard = null;

    }
}
