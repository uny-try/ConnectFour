using System.Linq.Expressions;
using Microsoft.Maui.Controls.Shapes;

namespace ConnectFour;

public partial class MainPage : ContentPage
{
    const int Rows = 6;
    const int Columns = 7;


    GamePageModel _viewModel;

    public MainPage(GameState gameState)
    {
        InitializeComponent();
        _viewModel = new GamePageModel(gameState);
        BindingContext = _viewModel;
    }
}
