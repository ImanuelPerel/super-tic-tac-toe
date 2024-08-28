

using SuperTicTacToe.ViewModel;
using System.Windows;

namespace SuperTicTacToe;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    TicTacToeViewModel ViewModel;
    public MainWindow()
    {
        InitializeComponent();
        ViewModel= new TicTacToeViewModel();
        this.DataContext = ViewModel;
    }
}