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

namespace SuperTicTacToe.View;

/// <summary>
/// Interaction logic for MainBoard.xaml
/// </summary>
public partial class MainBoard : Window
{

    
    public bool Turn
    {
        get { return (bool)GetValue(TurnProperty); }
        set { SetValue(TurnProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Turn.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TurnProperty =
        DependencyProperty.Register(nameof(Turn), typeof(bool), typeof(MainBoard), new PropertyMetadata(true/*, OnTurnPropertyChanged*/));


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
        // Set DataContext to the viewModel instance for binding
        //DataContext = viewModel;

        // Initialize the Turn property to match the ViewModel
        Turn = viewModel.Turn;

        InitializeDynamicGrid();
    }


    private void InitializeDynamicGrid()
    {
        // Set up the main grid
        for (int i = 0; i < MetaData.BoardSizeRows; i++)
        {
            _mainGrid.RowDefinitions.Add(new RowDefinition());
            _mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        // Create the small grids dynamically
        for (int mainRow = 0; mainRow < MetaData.BoardSizeRows; mainRow++)
        {
            for (int mainCol = 0; mainCol < MetaData.BoardSizeRows; mainCol++)
            {
                Grid smallGrid = new Grid
                {
                    Margin = new Thickness(5)
                };

                // Set Grid position for smallGrid
                Grid.SetRow(smallGrid, mainRow);
                Grid.SetColumn(smallGrid, mainCol);

                // Define rows and columns for smallGrid
                for (int i = 0; i < MetaData.BoardSizeRows; i++)
                {
                    smallGrid.RowDefinitions.Add(new RowDefinition());
                    smallGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                // Add buttons to each small grid
                for (int row = 0; row < MetaData.BoardSizeRows; row++)
                {
                    for (int col = 0; col < MetaData.BoardSizeRows; col++)
                    {
                        Button button = new Button
                        {
                            Content =MetaData.DefualtChar, // Placeholder content
                            Margin = new Thickness(1),
                            IsEnabled = true
                        };

                        // Set Grid position for button
                        Grid.SetRow(button, row);
                        Grid.SetColumn(button, col);
                        button.Click += viewModel.Click; // Attach event handler
                        button.Click += turnOnAndOff;
                        button.Click += updateTurn;
                        button.Click += updateInnerBoard;
                        button.Click += viewModel.CheckForWin;
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
    private void updateTurn(object sender,EventArgs e) { Turn = viewModel.Turn; }
    
    private void updateInnerBoard(object sender,EventArgs e)
    {
        if (sender is not Button button|| button.Parent is not Grid smallGrid)
            return;
        int x=Grid.GetRow(smallGrid);
        int y=Grid.GetColumn(smallGrid);
        Winner win = viewModel.PassWinOfInnerBoard(x, y);

        if (!win.Equals(Winner.NO_ONE_YET)) // Check if there is a winner
        {
            MainGrid.Children.Remove(smallGrid);

            
            // Calculate a suitable font size based on the cell dimensions
            double fontSize = Math.Min(
                MainGrid.ColumnDefinitions[y].ActualWidth,
                MainGrid.RowDefinitions[x].ActualHeight
                ) / 2; // Adjust division factor as needed for desired size

            Label label = new Label
            {
                Content = MetaData.SymbolConvertor(win),
                Margin = new Thickness(5),

                // Stretch the text across the entire label
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,

                // Set the alignment to stretch for the label itself
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,

                // Set the background color based on the win state using MetaData.ColorConvertor(win)
                Background = new SolidColorBrush(MetaData.ColorConvertor(win)),

                // Dynamically set the font size
                FontSize = fontSize
            };

            // Set row and column
            Grid.SetRow(label, x);
            Grid.SetColumn(label, y);
            MainGrid.Children.Add(label);
        }
    }
    

    // Callback method for when the DependencyProperty changes
    //private static void OnTurnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //{
    //    if (d is MainBoard mainBoard && mainBoard.viewModel != null &&(e.NewValue is bool booleanValue||e.NewValue is null))
    //    {
    //        // Update the viewModel when the Turn property changes
    //        mainBoard.viewModel.Turn = (bool?)e.NewValue;
    //    }
    //}
}
