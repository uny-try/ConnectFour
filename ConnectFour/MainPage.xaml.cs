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

        _viewModel.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(_viewModel.ErrorMessage) && !string.IsNullOrEmpty(_viewModel.ErrorMessage))
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("エラー", _viewModel.ErrorMessage, "OK");
                });
            }
        };
    }
}
