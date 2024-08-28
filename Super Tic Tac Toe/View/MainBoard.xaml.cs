using SuperTicTacToe.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace SuperTicTacToe.View
{
    /// <summary>
    /// Interaction logic for MainBoard.xaml
    /// </summary>
    public partial class MainBoard : Window
    {
        protected TicTacToeViewModel viewModel=new TicTacToeViewModel();
        

        public MainBoard()
        {
            InitializeComponent();
            //this.DataContext = viewModel;
            MainGrid = viewModel.MainGrid;
        }
    }
}
