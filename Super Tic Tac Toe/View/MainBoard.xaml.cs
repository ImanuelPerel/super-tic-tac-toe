using SuperTicTacToe.Model;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuperTicTacToe.View
{
    /// <summary>
    /// Interaction logic for MainBoard.xaml
    /// </summary>
    public partial class MainBoard : Window
    {
        protected TicTacToeViewModel viewModel = new TicTacToeViewModel();

        public Grid MainGrid
        {
            get => _mainGrid;
            set
            {
                _mainGrid = value;
            }
        }


        public MainBoard()
        {
            InitializeComponent();
            InitializeDynamicGrid();
        }


        private void InitializeDynamicGrid()
        {
            // Set up the main grid
            for (int i = 0; i < MetaData.BoardSize; i++)
            {
                _mainGrid.RowDefinitions.Add(new RowDefinition());
                _mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Create the small grids dynamically
            for (int mainRow = 0; mainRow < MetaData.BoardSize; mainRow++)
            {
                for (int mainCol = 0; mainCol < MetaData.BoardSize; mainCol++)
                {
                    Grid smallGrid = new Grid
                    {
                        Margin = new Thickness(5)
                    };

                    // Set Grid position for smallGrid
                    Grid.SetRow(smallGrid, mainRow);
                    Grid.SetColumn(smallGrid, mainCol);

                    // Define rows and columns for smallGrid
                    for (int i = 0; i < MetaData.BoardSize; i++)
                    {
                        smallGrid.RowDefinitions.Add(new RowDefinition());
                        smallGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    }

                    // Add buttons to each small grid
                    for (int row = 0; row < MetaData.BoardSize; row++)
                    {
                        for (int col = 0; col < MetaData.BoardSize; col++)
                        {
                            Button button = new Button
                            {
                                Content = InnerBoard.Empty, // Placeholder content
                                Margin = new Thickness(1),
                                IsEnabled = true
                            };

                            // Set Grid position for button
                            Grid.SetRow(button, row);
                            Grid.SetColumn(button, col);
                            button.Click += viewModel.Click; // Attach event handler
                            button.Click += turnOnAndOff;

                            // Add button to smallGrid
                            smallGrid.Children.Add(button);
                        }
                    }

                    // Add smallGrid to mainGrid
                    _mainGrid.Children.Add(smallGrid);
                }

            }

        }
        private void turnOnAndOff(object sender, EventArgs e)
        {
            if (viewModel.BoardChange.Item1 == viewModel.BoardChange.Item2)
                return;
            if (viewModel.BoardChange.Item1 is null || viewModel.BoardChange.Item2 is null)
            //but not both null since we checked for equality before
            {
                int innerRow, innerColumn, outerRow, outerColumn;

                // Iterate through all buttons and update their IsEnabled state
                foreach (UIElement child in _mainGrid.Children)
                {
                    if (child is Grid smallGrid)
                    {
                        outerRow = Grid.GetRow(smallGrid);
                        outerColumn = Grid.GetColumn(smallGrid);

                        foreach (UIElement smallGridChild in smallGrid.Children)
                        {
                            if (smallGridChild is Button button)
                            {
                                innerRow = Grid.GetRow(button);
                                innerColumn = Grid.GetColumn(button);
                           

                                // Update the button's IsEnabled property based on the game state
                                button.IsEnabled = viewModel.IsEnabled(innerRow, innerColumn, outerRow, outerColumn);
                            }
                        }
                    }
                }
            }
            else
            {
                int innerRow, innerColumn, outerRow, outerColumn;
                (int, int) currentInnerBoard;
                //modify only the buttons that are in the smallGrids that are in the place of BoardChange.Item1 and those that are on BoardChagne.Items
                foreach (UIElement child in _mainGrid.Children)
                {
                    if (child is Grid smallGrid)
                    {
                        outerRow= Grid.GetRow(smallGrid);
                        outerColumn = Grid.GetColumn(smallGrid);
                        currentInnerBoard=(outerRow, outerColumn);
                        if(currentInnerBoard==viewModel.BoardChange.Item1||
                            currentInnerBoard==viewModel.BoardChange.Item2)
                        {
                            foreach(UIElement smallGridChild in smallGrid.Children)
                            {
                                if(smallGridChild is Button button)
                                {
                                    innerRow= Grid.GetRow(button);
                                    innerColumn= Grid.GetColumn(button);
                                    button.IsEnabled= 
                                        viewModel.IsEnabled(innerRow, innerColumn,outerRow, outerColumn);
                                }
                            }
                        }
                    }

                }
            }

        }
    }
}
