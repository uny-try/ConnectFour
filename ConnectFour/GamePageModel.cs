using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;

namespace ConnectFour;

public partial class GamePageModel : ObservableObject
{
	const int Rows = 6;
	const int Columns = 7;
	GameState _gameState;

	public ObservableCollection<Brush> BoardColors { get; } = new ObservableCollection<Brush>();
	public ObservableCollection<ColumnButtonModel> ColumnButtons { get; }

	public GamePageModel(GameState gameState)
	{
		_gameState = gameState;
		_gameState.ResetBoard();

		// BoardColorsを白色で初期化
		BoardColors.Clear();
		for (int i = 0; i < Rows * Columns; i++)
		{
			BoardColors.Add(new SolidColorBrush(Colors.White));
		}

		ColumnButtons = new ObservableCollection<ColumnButtonModel>(
			Enumerable.Range(0, 7).Select(i => new ColumnButtonModel(i, PlayPieceCommand))
		);
	}

	private string? errorMessage;
	public string? ErrorMessage
	{
		get => errorMessage;
		set
		{
			if (errorMessage != value)
			{
				errorMessage = value;
				OnPropertyChanged(nameof(ErrorMessage));
			}
		}
	}

	[RelayCommand]
	public void PlayPiece(int col)
	{
		int row;
		try
		{
			row = _gameState.PlayPiece(col);
			ErrorMessage = null; // 成功時はクリア
		}
		catch (Exception ex)
		{
			ErrorMessage = "Invalid move: " + ex.Message;
			return;
		}

		BoardColors[row * Columns + col] = _gameState.PlayerTurn == 1
			? new SolidColorBrush(Colors.Red)
			: new SolidColorBrush(Colors.Yellow);

		GameState.WinState winState = _gameState.CheckForWin();
		if (winState != GameState.WinState.No_Winner)
		{
			ErrorMessage = "Game Over: " + winState.ToString();
		}
	}

	[RelayCommand]
	public void Reset()
	{
		_gameState.ResetBoard();
		for (int i = 0; i < Rows * Columns; i++)
		{
			BoardColors[i] = new SolidColorBrush(Colors.White);
		}
		ErrorMessage = null;
	}
}