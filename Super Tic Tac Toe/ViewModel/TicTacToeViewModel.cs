using SuperTicTacToe.Model;
using System.Windows;
using System.Windows.Controls;

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
    internal OuterBoard board=new OuterBoard();

    public Grid MainGrid { get; private set; } =new Grid();


    public TicTacToeViewModel()
    {
        InitializeDynamicGrid();
    }

    private void InitializeDynamicGrid()
    {
        // Set up the main grid
        for (int i = 0; i < MetaData.BoardSize; i++)
        {
            MainGrid.RowDefinitions.Add(new RowDefinition());
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
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
                            Margin = new Thickness(1)
                        };

                        // Set Grid position for button
                        Grid.SetRow(button, row);
                        Grid.SetColumn(button, col);
                        button.Click += Click; // Attach event handler

                        // Add button to smallGrid
                        smallGrid.Children.Add(button);
                    }
                }

                // Add smallGrid to mainGrid
                MainGrid.Children.Add(smallGrid);
            }
        }


    }

    private void Click(object sender, RoutedEventArgs e)
    {
        Button clickedButton = (Button)sender;

        // Identify which button was clicked and update the game state
        int innerRow = Grid.GetRow(clickedButton);
        int innerColumn = Grid.GetColumn(clickedButton);
        int outerRow = Grid.GetRow((Grid)clickedButton.Parent);
        int outerColumn = Grid.GetColumn((Grid)clickedButton.Parent);


        board.TakeTurn(
            xInner: innerRow,
            yInner: innerColumn, 
            xOuter: outerRow, 
            yOuter: outerColumn, 
            player: turn);

    }

}

