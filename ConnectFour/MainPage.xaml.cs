using Microsoft.Maui.Controls.Shapes;

namespace ConnectFour;

public partial class MainPage : ContentPage
{
    const int Rows = 6;
    const int Columns = 7;

    public MainPage()
    {
        InitializeComponent();
        CreateBoard();
    }

    void CreateBoard()
    {
        BoardGrid.RowDefinitions.Clear();
        BoardGrid.ColumnDefinitions.Clear();

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
