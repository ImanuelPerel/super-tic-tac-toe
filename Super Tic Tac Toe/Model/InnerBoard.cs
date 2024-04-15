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
    const int BoardSize = 3;
    const int howManyToWin = 3;
    const char defaultChar = ' ';
    char[,] board;
    public void TakeTurn(char player, int x, int y)
    {
        if (x < 0 || x >= BoardSize) throw new ArgumentOutOfRangeException("index x is out of range");
        if (y < 0 || y >= BoardSize) throw new ArgumentOutOfRangeException("index y is out of range");
        if (board[x, y] == defaultChar)
            board[x, y] = player;
        else throw new AlreadyHasAValueException($"the place at the index {x},{y} is already full.");
    }
    public char? Win = null;

    bool outOfRange(int x, int y)
    {
        if (x < 0 || x > BoardSize || y < 0 || y < BoardSize) return true;
        return false;
    }
    private int howManyThere(int x, int y, char player, Direction dir)
    {
        if (outOfRange(x, y)) return 0;
        if (board[x, y] != player) return 0;
        return 1 + howManyThere(x + dir.x, y + dir.y, player, dir);
    }
    public char? CalcWin(int x, int y)
    {
        if(Win!= null) return Win;
        if (outOfRange(x, y)) return null;
        char player = board[x, y];
        if (player == defaultChar) return null;
        if (1 + howManyThere(x, y, player, new Direction { x = 1, y = 0 }) + howManyThere(x, y, player, new Direction { x = -1, y = 0 }) >= howManyToWin) Win= player;
        if (1 + howManyThere(x, y, player, new Direction { x = 1, y = 1 }) + howManyThere(x, y, player, new Direction { x = -1, y = -1 }) >= howManyToWin) Win = player;
        if (1 + howManyThere(x, y, player, new Direction { x = 0, y = 1 }) + howManyThere(x, y, player, new Direction { x = 0, y = -1 }) >= howManyToWin) Win = player;
        if (1 + howManyThere(x, y, player, new Direction { x = -1, y = 1 }) + howManyThere(x, y, player, new Direction { x = 1, y = -1 }) >= howManyToWin) Win = player;
        return Win;
    }
    public InnerBoard()
    {
        board = new char[BoardSize, BoardSize];
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                board[i, j] = ' ';
            }
        }

    }
}


