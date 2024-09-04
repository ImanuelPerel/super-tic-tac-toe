using SuperTicTacToe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace SuperTicTacToe;

public static class MetaData
{

    static private int boardSizeRows = 2;
    static private int boardSizeCols = boardSizeRows;
    private static int howManyToWinInner = Math.Min(boardSizeRows, boardSizeCols);
    private static int howManyToWinOuter = howManyToWinInner;
    public static char DefualtChar { get; set; } = ' ';
    public static char CharForTie { get; set; } = '-';
    static public int BoardSizeRows
    {
        get => boardSizeRows;
        set
        {
            if (value < 1)
                throw new ArgumentException($"{nameof(BoardSizeRows)} can be only a positive integer");
            boardSizeRows = value;
        }
    }
    static public int BoardSizeCols
    {
        get => boardSizeCols;
        set
        {
            if (value < 1)
                throw new ArgumentException($"{nameof(BoardSizeCols)} can be only a positive integer");
            boardSizeCols = value;
        }
    }
    public static int HowManyToWinInner
    {
        get => howManyToWinInner;
        set
        {
            if (value < 1)
                throw new ArgumentException($"{nameof(HowManyToWinOuter)} can be only a positive integer");
            howManyToWinInner = value;
        }
    }
    public static int HowManyToWinOuter
    {
        get => howManyToWinOuter;
        set
        {
            if (value < 1)
                throw new ArgumentException($"{nameof(HowManyToWinOuter)} can be only a positive integer");
            howManyToWinOuter = value;
        }
    }

    static public readonly Dictionary<bool, char> SymbolsDictFromBool = new()
    {
        { true, 'X' },
        { false, 'O' }
    };

    static public readonly Dictionary<Winner, char> SymbolsDict = new()
    {
        { Winner.NO_ONE_YET, DefualtChar},
        { Winner.Player1, SymbolsDictFromBool[true] },
        { Winner.Player2, SymbolsDictFromBool[false] },
        { Winner.TIE, CharForTie }
    };

    static public readonly Dictionary<Winner, Color> ColorsDict = new()
    {
        { Winner.NO_ONE_YET, Colors.Gray }, // White color for no winner yet
        { Winner.Player1, Colors.Blue },           // Blue color for player X's win
        { Winner.Player2, Colors.Red },            // Red color for player O's win
        { Winner.TIE, Colors.Orange }          // Gray color for a tie
    };
    static public char SymbolConvertor(object obj)
    {
        if (obj is bool booleanValue)
            return SymbolsDictFromBool[booleanValue];
        if (obj is Winner winnerValue)
        {
            if (winnerValue.Equals(Winner.TIE))
                return CharForTie;
            if (winnerValue.Equals(Winner.NO_ONE_YET))
                return DefualtChar;
            return SymbolsDictFromBool[winnerValue.ToBool()];
        }
        if (obj is null)
            return DefualtChar;
        throw new InvalidOperationException($"{nameof(SymbolConvertor)} can't convert object that is not of type {typeof(bool)}, {typeof(Winner)} or null.");
    }

    public static bool OutOfRange(int x, int y)
    {
        if (x < 0 || x >= MetaData.BoardSizeRows || y < 0 || y >= MetaData.BoardSizeCols) return true;
        return false;
    }

    public static Color ColorConvertor(object obj)
    {
        char symbol = SymbolConvertor(obj);
        if (symbol == SymbolsDict[Winner.TIE])
            return ColorsDict[Winner.TIE];
        if (symbol == SymbolsDict[Winner.Player1])
            return ColorsDict[Winner.Player1];
        if (symbol == SymbolsDict[Winner.Player2])
            return ColorsDict[Winner.Player2];

        return ColorsDict[Winner.NO_ONE_YET];


    }
}
