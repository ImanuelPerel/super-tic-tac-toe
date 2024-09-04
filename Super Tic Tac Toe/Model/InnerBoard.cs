using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using System.Windows.Media;
using MyCsLibrary0.Games.BoardGames;



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
public class InnerBoard : Board<bool?>, IReadOnlyInnerBoard
{
    //static new public bool? Empty => null;
    //bool?[,] board;
    //public bool? this[int x, int y] { get => board[x, y]; }
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


    public Winner? CalcWin(int x, int y)
    {
        if (!Win.Equals(Winner.NO_ONE_YET)) return Win;
        else if (MetaData.OutOfRange(x, y) || (board[x, y] == Empty)) Win = Winner.NO_ONE_YET;
        //asuming there can only be one winner since when one becomes a winner the win automatically changes to be him
        else if (WhichIsThereInNInARowDoubleDirection(x, y, MetaData.HowManyToWinInner) is bool player)
            Win = Winner.FromBool(player);
        else /*player is null*/if (board.Cast<bool?>().All(ch => ch != Empty))
        {
            Win = Winner.TIE;
            //because if they are all not empty and there was a winner before it should have been 
            //made him the winner in his Turn when he won
        }
        return Win;
    }
    public InnerBoard() : base(MetaData.BoardSizeRows, MetaData.BoardSizeCols)
    {
    }

}


