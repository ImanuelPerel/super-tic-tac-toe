using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Tic_Tac_Toe.Model;

public class InnerBoard
{
    const int BoardSize = 3;
    char[,] board;
    public void TakeTurn(char player,int x,int y)
    {
        if (x < 0 || x > BoardSize) throw new ArgumentOutOfRangeException("index x is out of range");
        if (y < 0 || y > BoardSize) throw new ArgumentOutOfRangeException("index y is out of range");
        if (board[x,y]==' ')
            board[x,y] = player;
        else throw
    }
    public char? Win = null;
    public char? CalcWin(int x, int y) 
    {
        throw new NotImplementedException();

    }
    public InnerBoard()
    {
        char[,] board = new char[BoardSize, BoardSize];
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                board[i, j] = ' ';
            }
        }

    }
}
