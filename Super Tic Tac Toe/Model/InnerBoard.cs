using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using System.Windows.Media;

namespace Super_Tic_Tac_Toe.Model;

public class InnerBoard
{
    
    char[,] board;
    public void TakeTurn(char player, int x, int y)
    {
        if (x < 0 || x >= MetaData.BoardSize) throw new ArgumentOutOfRangeException("index x is out of range");
        if (y < 0 || y >= MetaData.BoardSize) throw new ArgumentOutOfRangeException("index y is out of range");
        if (board[x, y] == MetaData.defaultChar)
            board[x, y] = player;
        else throw new AlreadyHasAValueException($"the place at the index {x},{y} is already full.");
    }


    //if win=metaData.defaultchar then it means that this board is a tie
    public char? Win = null;

    
    private int howManyThere(int x, int y, char player, Direction dir)
    {
        if (MetaData.outOfRange(x, y)) return 0;
        if (board[x, y] != player) return 0;
        return 1 + howManyThere(x + dir.x, y + dir.y, player, dir);
    }
    public char? CalcWin(int x, int y)
    {
        if(Win!= null) return Win;
        if (MetaData.outOfRange(x, y)) return null;
        char player = board[x, y];

        if (board.Cast<char>().All(ch => ch != MetaData.defaultChar))
        {
            Win = MetaData.defaultChar;
        }
        if (player == MetaData.defaultChar) return null;
        if (1 + howManyThere(x, y, player, new Direction { x = 1, y = 0 }) + howManyThere(x, y, player, new Direction { x = -1, y = 0 }) >= MetaData.howManyToWin) Win= player;
        if (1 + howManyThere(x, y, player, new Direction { x = 1, y = 1 }) + howManyThere(x, y, player, new Direction { x = -1, y = -1 }) >= MetaData.howManyToWin) Win = player;
        if (1 + howManyThere(x, y, player, new Direction { x = 0, y = 1 }) + howManyThere(x, y, player, new Direction { x = 0, y = -1 }) >= MetaData.howManyToWin) Win = player;
        if (1 + howManyThere(x, y, player, new Direction { x = -1, y = 1 }) + howManyThere(x, y, player, new Direction { x = 1, y = -1 }) >= MetaData.howManyToWin) Win = player;


        return Win;
    }
    public InnerBoard()
    {
        board = new char[MetaData.BoardSize, MetaData.BoardSize];
        for (int i = 0; i < MetaData.BoardSize; i++)
        {
            for (int j = 0; j < MetaData.BoardSize; j++)
            {
                board[i, j] = ' ';
            }
        }

    }
}


