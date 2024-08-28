

using SuperTicTacToe.ViewModel;
using System.Windows;

namespace SuperTicTacToe;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    TicTacToeViewModel ViewModel;


    //public TicTacToeViewModel MyProperty
    //{
    //    get { return (TicTacToeViewModel)GetValue(MyPropertyProperty); }
    //    set { SetValue(MyPropertyProperty, value); }
    //}

    //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    //public static readonly DependencyProperty MyPropertyProperty =
    //    DependencyProperty.Register("MyProperty", typeof(TicTacToeViewModel), typeof(o), new PropertyMetadata(0));


    public MainWindow()
    {
        InitializeComponent();
        //ViewModel= new TicTacToeViewModel();
        //this.DataContext = ViewModel;
    }
}