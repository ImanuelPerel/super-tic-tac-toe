using System;
using System.Security.Policy;

namespace SuperTicTacToe.Model;

public class Winner
{
    private int id;
    private const int forNoOneYet = 1;
    private const int forTie = 2;
    private const int forPlayer1 = 3;
    private const int forPlayer2 = 4;
    private Winner(int id)
    {
        if (id == forNoOneYet || id == forTie || id == forPlayer1 || id == forPlayer2)
            this.id = id;
        else
            throw new ArgumentException(
                $"{nameof(id)} has to be either " +
                $"{nameof(forNoOneYet)} which is {forNoOneYet}, or" +
                $"{nameof(forTie)} which is {forTie}, or" +
                $"{nameof(forPlayer1)} which is {forPlayer1}, or" +
                $"{nameof(forPlayer2)} which is {forPlayer2}."
                );
    }

    // Use static readonly for object instances
    public static readonly Winner NO_ONE_YET = new Winner(forNoOneYet);
    public static readonly Winner TIE = new Winner(forTie);
    public static readonly Winner Player1 = new Winner(forPlayer1); // For player X
    public static readonly Winner Player2 = new Winner(forPlayer2); // For player O

    public bool ToBool()
    {
        if (this == Player1)
            return true;
        else if (this == Player2)
            return false;

        throw new InvalidOperationException($"Invalid state: {NO_ONE_YET} or {TIE} cannot be converted to bool");
    }
    public static Winner FromBool(bool value)
    {
        switch(value) 
        {
            case true: return Player1;

            case false: return Player2;
        }
    }

    // Optional: Override Equals and GetHashCode for proper equality checks
    public override bool Equals(object? obj)
    {
        if (obj is Winner other)
        {
            return this.id == other.id;
        }
        return false;
    }
    public bool NotEquals(object? obj)
    { return !Equals(obj); }
    public override int GetHashCode()
    {
        return id.GetHashCode();
    }

    // Override ToString for meaningful exception messages
    public override string ToString()
    {
        return id switch
        {
            forNoOneYet => nameof(NO_ONE_YET),
            forTie => nameof(TIE),
            forPlayer1 => nameof(Player1),
            forPlayer2 => nameof(Player2),
            _ => throw new InvalidOperationException($"it is not possible to have id different from all"+ 
               $"{nameof(NO_ONE_YET)} which is {forNoOneYet}, and" +
                $"{nameof(TIE)} which is {forTie}, and" +
                $"{nameof(Player1)} which is {forPlayer1}, and" +
                $"{nameof(Player2)} which is {forPlayer2}.")
        };
    }
}
