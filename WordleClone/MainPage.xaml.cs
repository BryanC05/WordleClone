namespace WordleClone;

public partial class MainPage : ContentPage
{
    // This is the grid of 6 rows and 5 columns (for the UI)
    private readonly Label[,] _letterTiles = new Label[6, 5];

    // Game state
    private int _currentRow = 0;
    private int _currentCol = 0;
	private string _secretWord = "MAUI";
	private readonly List<string> _wordList = new() 
    { 
        "ADMIN", "STORE", "MAUI", "APPLE", "HOUSE", 
        "BLINK", "SHARE", "VIDEO", "WATER", "STACK" 
    };

    // ADD THIS: A random number generator
    private readonly Random _random = new();

    // Colors
    private readonly Color _colorCorrect = Color.FromArgb("#3aa394");
    private readonly Color _colorPresent = Color.FromArgb("#d3ad69");
    private readonly Color _colorAbsent = Color.FromArgb("#312a2c");
	private readonly Color _colorBorder = Colors.Grey;
	private readonly Color _colorActive = Colors.Blue;

	private string GetRandomWord()
	{
		int index = _random.Next(_wordList.Count);
		return _wordList[index].ToUpper();
	}
	
	public MainPage()
	{
		InitializeComponent();
		_secretWord = GetRandomWord(); // Set a random word
		CreateGameBoard();
	}
	

    private void CreateGameBoard()
    {
        // This method creates the 30 tiles (Border + Label)
        for (int r = 0; r < 6; r++)
		{
			for (int c = 0; c < 5; c++)
			{
				var label = new Label
		{
			Text = " ",
			FontSize = 32,
			FontAttributes = FontAttributes.Bold,
			TextColor = Colors.White,
			VerticalOptions = LayoutOptions.Center,
			HorizontalOptions = LayoutOptions.Center
		};

        var border = new Border
        {
            Content = label,
            WidthRequest = 60,
            HeightRequest = 60,
            BackgroundColor = Colors.White
            // Set Stroke and StrokeThickness based on the row
        };

        if (r == 0)
        {
            // First row is active
            border.Stroke = _colorActive;
            border.StrokeThickness = 4;
        }
        else
        {
            // Other rows are inactive
            border.Stroke = _colorBorder;
            border.StrokeThickness = 2;
        }
        
        _letterTiles[r, c] = label;
        GameBoardGrid.Add(border, c, r);
    }
}
    }

	private void OnKeyboardClicked(object sender, EventArgs e)
	{
		var button = (Button)sender;
		string letter = button.Text;

		// Only add if we are not at the end of the row
		if (_currentCol < 5)
		{
			_letterTiles[_currentRow, _currentCol].Text = letter;
			_currentCol++; // Move to the next column
			// Get the border of the tile we just typed in
			var border = (Border)_letterTiles[_currentRow, _currentCol - 1].Parent;
			// Set its background to the "typed" color
			border.BackgroundColor = _colorAbsent;
		}
	}
	

    // Runs when "DEL" is clicked
    private void OnDeleteClicked(object sender, EventArgs e)
    {
        // Only delete if we are not at the start
        if (_currentCol > 0)
        {
            _currentCol--; // Move back one column
			_letterTiles[_currentRow, _currentCol].Text = " ";
			// Get the border of the tile we just deleted
			var border = (Border)_letterTiles[_currentRow, _currentCol].Parent;
			// Set its background back to the "empty" color
			border.BackgroundColor = Colors.White;
        }
    }

    // Runs when "ENTER" is clicked
    private async void OnEnterClicked(object sender, EventArgs e)
    {
        // 1. Check if the row is full
        if (_currentCol < 5)
        {
            await DisplayAlert("Error", "Not enough letters", "OK");
            return;
        }

        // 2. Build the guess word
        string guess = "";
        for (int c = 0; c < 5; c++)
        {
            guess += _letterTiles[_currentRow, c].Text;
        }

        // 3. Check the guess
        CheckGuess(guess);

        // 4. Check for win/loss
        if (guess == _secretWord)
        {
            await DisplayAlert("You Win!", "Congratulations!", "Play Again");
            ResetGame();
        }
        else if (_currentRow == 5)
        {
            // We are at the end (row 5) and didn't win
            await DisplayAlert("You Lose!", $"The word was: {_secretWord}", "Play Again");
            ResetGame();
        }
        else
		{
			// 1. De-highlight the current row
			for (int c = 0; c < 5; c++)
			{
				var border = (Border)_letterTiles[_currentRow, c].Parent;
				border.Stroke = _colorBorder;
				border.StrokeThickness = 2;
			}

			// 2. Move to the next row
			_currentRow++;
			_currentCol = 0;

			// 3. Highlight the new current row
			for (int c = 0; c < 5; c++)
			{
				var border = (Border)_letterTiles[_currentRow, c].Parent;
				border.Stroke = _colorActive;
				border.StrokeThickness = 4;
			}
		}
    }

    private void CheckGuess(string guess)
    {
        // This is the core Wordle logic
        for (int c = 0; c < 5; c++)
        {
            var label = _letterTiles[_currentRow, c];
            var border = (Border)label.Parent;
            
            if (guess[c] == _secretWord[c])
            {
                // 1. Correct letter, correct position
                border.BackgroundColor = _colorCorrect;
            }
            else if (_secretWord.Contains(guess[c]))
            {
                // 2. Correct letter, wrong position
                border.BackgroundColor = _colorPresent;
            }
            else
            {
                // 3. Wrong letter
                border.BackgroundColor = _colorAbsent;
            }
        }
    }

    private void ResetGame()
    {
        // Set a new secret word
        _secretWord = GetRandomWord(); // Set a new random word
    	_currentRow = 0;
        _currentCol = 0;

        // Clear all the tiles
        for (int r = 0; r < 6; r++)
		{
		for (int c = 0; c < 5; c++)
		{
			_letterTiles[r, c].Text = " ";
			var border = (Border)_letterTiles[r, c].Parent;
			border.BackgroundColor = Colors.White;
				if (r == 0)
				{
					border.Stroke = _colorActive;
					border.StrokeThickness = 4;
				}
				else
				{
					border.Stroke = _colorBorder;
					border.StrokeThickness = 2;
				}
			}
		}
    }
}