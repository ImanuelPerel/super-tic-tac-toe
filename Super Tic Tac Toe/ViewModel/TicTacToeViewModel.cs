﻿using SuperTicTacToe.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperTicTacToe.ViewModel;

/// <summary>
/// Interaction logic for TicTacToeViewModel.xaml
/// </summary>
public class TicTacToeViewModel 
{
    /// <summary>
    /// false for 'O'
    /// true for 'X'
    /// </summary>
    internal bool turn = true;
    internal OuterBoard board = new OuterBoard();

    /// <summary>
    /// (prevInnerBoard,newInnerBoard)
    /// each of type (int,int)?
    /// </summary>
    internal ((int,int)?,(int,int)?) BoardChange { get;private set; }=(null,null);
    public TicTacToeViewModel()
    {
    }



    public void Click(object sender, RoutedEventArgs e)
    {
        BoardChange = (board.CurrentInnerBoard, BoardChange.Item2);

        //Button clickedButton = (Button)sender;
        if (!(sender is Button clickedButton)) return;
        // Identify which button was clicked and update the game state
        int innerRow = Grid.GetRow(clickedButton);
        int innerColumn = Grid.GetColumn(clickedButton);
        int outerRow = Grid.GetRow((Grid)clickedButton.Parent);
        int outerColumn = Grid.GetColumn((Grid)clickedButton.Parent);
        clickedButton.IsEnabled = false;
        clickedButton.Content = MetaData.symbols[turn];
        board.TakeTurn(
            xInner: innerRow,
            yInner: innerColumn,
            xOuter: outerRow,
            yOuter: outerColumn,
            player: turn);


       BoardChange=(BoardChange.Item1,board.CurrentInnerBoard);


        turn = !turn;
    }

    public bool IsEnabled(int innerX,int innerY,int outerX ,int outerY)
    {
        if(MetaData.OutOfRange(innerX,innerY)||MetaData.OutOfRange(outerX,outerY)||
            board[outerX, outerY].Win is not null||
            board[outerX, outerY][innerX,innerY] is not null)
            return false;
        return board.CurrentInnerBoard is not (int,int) currentInnerBoard ||
            (currentInnerBoard.Item1==outerX && currentInnerBoard.Item2==outerY);
    }
}

