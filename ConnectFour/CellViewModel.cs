using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Graphics;

namespace ConnectFour;

public partial class CellViewModel : ObservableObject
{
    private Brush color = new SolidColorBrush(Colors.White);
    public Brush Color
    {
        get => color;
        set
        {
            if (color != value)
            {
                color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
    }
}