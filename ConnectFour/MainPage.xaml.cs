using System.Linq.Expressions;
using Microsoft.Maui.Controls.Shapes;

namespace ConnectFour;

public partial class MainPage : ContentPage
{
    const int Rows = 6;
    const int Columns = 7;

    private string[] pieces = new string[42];

    GameState _gameState;
    public MainPage(GameState gameState)
    {
        InitializeComponent();
        _gameState = gameState;
        _gameState.ResetBoard();
        CreateColumnButtons();
        CreateBoard();
    }

    void CreateColumnButtons()
    {
        ButtonGrid.ColumnDefinitions.Clear();
        ButtonGrid.Children.Clear();

        for (int c = 0; c < Columns; c++)
            ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });

        for (int c = 0; c < Columns; c++)
        {
            var btn = new Button
            {
                Text = "↓",
                FontSize = 24,
                WidthRequest = 50,
                HeightRequest = 40,
                // Tagプロパティに列番号を格納
                BindingContext = c
            };
            btn.Clicked += OnColumnButtonClicked;
            ButtonGrid.Add(btn, c, 0);
        }
    }

    void OnColumnButtonClicked(object? sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is int col)
        {
            int row;
            try
            {
                row = _gameState.PlayPiece(col);
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "Invalid move: " + ex.Message, "OK");
                return;
            }

            if (row >= 0)
            {
                // 円の色を変更
                var ellipse = BoardGrid.Children
                    .OfType<Ellipse>()
                    .FirstOrDefault(x => Grid.GetRow(x) == row && Grid.GetColumn(x) == col);

                if (ellipse != null)
                {
                    ellipse.Fill = _gameState.PlayerTurn == 1 ? Colors.Red : Colors.Yellow;
                }

                // 勝利判定
                GameState.WinState winState = _gameState.CheckForWin();
                if (winState != 0)
                {
                    if (winState == GameState.WinState.Tie)
                    {
                        DisplayAlert("Game Over", "It's a tie!", "OK");
                    }
                    else
                    {
                        // 勝利したプレイヤーの番号を表示
                        int winner = winState == GameState.WinState.Player1_Wins ? 1 : 2;
                        DisplayAlert("Game Over", $"Player {winner} wins!", "OK");
                        _gameState.ResetBoard();
                        CreateBoard();
                    }
                }
            }
        }
    }

    void CreateBoard()
    {
        BoardGrid.RowDefinitions.Clear();
        BoardGrid.ColumnDefinitions.Clear();
        BoardGrid.Children.Clear();

        for (int r = 0; r < Rows; r++)
            BoardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(70) });

        for (int c = 0; c < Columns; c++)
            BoardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });

        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                var ellipse = new Ellipse
                {
                    Stroke = Colors.White,
                    StrokeThickness = 2,
                    Fill = Colors.LightGray,
                    WidthRequest = 70,
                    HeightRequest = 70,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                BoardGrid.Add(ellipse, c, r);
            }
        }
    }
}
