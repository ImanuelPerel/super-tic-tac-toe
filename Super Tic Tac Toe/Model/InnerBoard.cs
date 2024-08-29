using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using System.Windows.Media;

namespace SuperTicTacToe.Model;
public interface IReadOnlyInnerBoard
{
    Winner Win { get; }
    bool? this[int x, int y] { get; }
}
/// <summary>
/// the board contain bool? type
/// true for 'X'
/// false for 'O'
/// and null for an empty place
/// </summary>
public class InnerBoard : IReadOnlyInnerBoard
{
    public static bool? Empty => null;
    bool?[,] board;
    public bool? this[int x, int y] { get => board[x, y]; }
    public void TakeTurn(bool player, int x, int y)
    {
        if (MetaData.OutOfRange(x, y))
            throw new ArgumentException("one or more of the indices is out of range");
        if (board[x, y] == Empty)
            board[x, y] = player;
        else throw new AlreadyHasAValueException($"the place at the index {x},{y} is already full.");
        CalcWin(x, y);
    }

    public Winner Win { get; protected set; } = Winner.NO_ONE_YET;
    

    private int howManyThere(int x, int y, bool player, Direction dir)
    {
        if (MetaData.OutOfRange(x, y)) return 0;
        if (board[x, y] != player) return 0;
        return 1 + howManyThere(x + dir.X, y + dir.Y, player, dir);
    }
    public Winner? CalcWin(int x, int y)
    {
        if (!Win.Equals(Winner.NO_ONE_YET)) return Win;
        else if (MetaData.OutOfRange(x, y) || (board[x, y] == Empty)) Win = Winner.NO_ONE_YET;
        else
        {
            bool player = (bool)board[x, y]!;


            //asuming there can only be one winner since when one becomes a winner the win automatically changes to be him

            if (-1 + howManyThere(x, y, player, Direction.Up) +
                howManyThere(x, y, player, Direction.Down) >= MetaData.howManyToWin) Win = Winner.FromBool(player);
            else if (-1 + howManyThere(x, y, player, Direction.Right) +
                howManyThere(x, y, player, Direction.Left) >= MetaData.howManyToWin) Win = Winner.FromBool(player);
            else if (-1 + howManyThere(x, y, player, Direction.UpRight) +
                howManyThere(x, y, player, Direction.DownLeft) >= MetaData.howManyToWin) Win = Winner.FromBool(player);
            else if (-1 + howManyThere(x, y, player, Direction.UpLeft) +
                howManyThere(x, y, player, Direction.DownRight) >= MetaData.howManyToWin) Win = Winner.FromBool(player);
            else if (board.Cast<bool?>().All(ch => ch != Empty))
            {
                Win = Winner.TIE;
                //because if they are all not empty and there is a winner it should have been 
                //made him in his Turn
            }
        }

        return Win;
    }
    public InnerBoard()
    {
        board = new bool?[MetaData.BoardSize, MetaData.BoardSize];
        for (int i = 0; i < MetaData.BoardSize; i++)
        {
            for (int j = 0; j < MetaData.BoardSize; j++)
            {
                board[i, j] = InnerBoard.Empty;
            }
        }

    }
}


