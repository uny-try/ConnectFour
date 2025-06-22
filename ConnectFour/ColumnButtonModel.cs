using CommunityToolkit.Mvvm.Input;

namespace ConnectFour;

public class ColumnButtonModel
{
    public int ColumnIndex { get; }
    public IRelayCommand PlayPieceCommand { get; }

    public ColumnButtonModel(int columnIndex, IRelayCommand playPieceCommand)
    {
        ColumnIndex = columnIndex;
        PlayPieceCommand = playPieceCommand;
    }
}