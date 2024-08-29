using System;
using System.Security.Policy;

namespace SuperTicTacToe.Model;

public class Winner
{
    private int id;
    private const int forNoOneYet = 1;
    private const int forTie = 2;
    private const int forTrue = 3;
    private const int forFalse = 4;
    private Winner(int id)
    {
        if (id == forNoOneYet || id == forTie || id == forTrue || id == forFalse)
            this.id = id;
        else
            throw new ArgumentException(
                $"{nameof(id)} has to be either " +
                $"{nameof(forNoOneYet)} which is {forNoOneYet}, or" +
                $"{nameof(forTie)} which is {forTie}, or" +
                $"{nameof(forTrue)} which is {forTrue}, or" +
                $"{nameof(forFalse)} which is {forFalse}."
                );
    }

    // Use static readonly for object instances
    public static readonly Winner NO_ONE_YET = new Winner(forNoOneYet);
    public static readonly Winner TIE = new Winner(forTie);
    public static readonly Winner TRUE = new Winner(forTrue); // For player X
    public static readonly Winner FALSE = new Winner(forFalse); // For player O

    public bool ToBool()
    {
        if (this == TRUE)
            return true;
        else if (this == FALSE)
            return false;

        throw new InvalidOperationException($"Invalid state: {NO_ONE_YET} or {TIE} cannot be converted to bool");
    }
    public static Winner FromBool(bool value)
    {
        switch(value) 
        {
            case true: return TRUE;

            case false: return FALSE;
        }
    }

    // Optional: Override Equals and GetHashCode for proper equality checks
    public override bool Equals(object obj)
    {
        if (obj is Winner other)
        {
            return this.id == other.id;
        }
        return false;
    }

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
            forTrue => nameof(TRUE),
            forFalse => nameof(FALSE),
            _ => throw new InvalidOperationException($"it is not possible to have id different from all"+ 
               $"{nameof(forNoOneYet)} which is {forNoOneYet}, and" +
                $"{nameof(forTie)} which is {forTie}, and" +
                $"{nameof(forTrue)} which is {forTrue}, and" +
                $"{nameof(forFalse)} which is {forFalse}.")
        };
    }
}
