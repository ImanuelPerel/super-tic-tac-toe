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
/// <summary>
/// the board contain bool? type
/// true for 'X'
/// false for 'O'
/// and null for an empty place
/// </summary>
public class InnerBoard
{
    public static bool? Empty => null;
    bool?[,] board;
    public bool? this[int x, int y] { get => board[x, y]; }
    public void TakeTurn(bool player, int x, int y)
    {
        if (x < 0 || x >= MetaData.BoardSize) throw new ArgumentOutOfRangeException("index x is out of range");
        if (y < 0 || y >= MetaData.BoardSize) throw new ArgumentOutOfRangeException("index y is out of range");
        if (board[x, y] == Empty)
            board[x, y] = player;
        else throw new AlreadyHasAValueException($"the place at the index {x},{y} is already full.");
    }

    public static bool? DefultWin => null;
    //if win=metaData.defaultchar then it means that this board is a tie
    public bool? Win = null;


    private int howManyThere(int x, int y, bool player, Direction dir)
    {
        if (MetaData.OutOfRange(x, y)) return 0;
        if (board[x, y] != player) return 0;
        return 1 + howManyThere(x + dir.x, y + dir.y, player, dir);
    }
    public bool? CalcWin(int x, int y)
    {
        if (Win != DefultWin) return Win;
        if (MetaData.OutOfRange(x, y)) return DefultWin;
        if (board[x, y] == Empty) return DefultWin;
        bool player = (bool)board[x, y]!;

        if (board.Cast<bool?>().All(ch => ch != Empty))
        {
            Win = DefultWin;
            //because if they are all not empty and there is a winner it should have been 
            //made him in his turn
        }
        //asuming there can only be one winner since when one becomes a winner the win automatically changes to be him
        else if (player == Empty) return DefultWin;
        else if (1 + howManyThere(x, y, player, new Direction { x = 1, y = 0 }) + howManyThere(x, y, player, new Direction { x = -1, y = 0 }) >= MetaData.howManyToWin) Win = player;
        else if (1 + howManyThere(x, y, player, new Direction { x = 1, y = 1 }) + howManyThere(x, y, player, new Direction { x = -1, y = -1 }) >= MetaData.howManyToWin) Win = player;
        else if (1 + howManyThere(x, y, player, new Direction { x = 0, y = 1 }) + howManyThere(x, y, player, new Direction { x = 0, y = -1 }) >= MetaData.howManyToWin) Win = player;
        else if (1 + howManyThere(x, y, player, new Direction { x = -1, y = 1 }) + howManyThere(x, y, player, new Direction { x = 1, y = -1 }) >= MetaData.howManyToWin) Win = player;


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


