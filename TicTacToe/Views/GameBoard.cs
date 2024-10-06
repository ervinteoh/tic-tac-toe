using System.Text;
using TicTacToe.Games;

namespace TicTacToe.Views;

/// <summary>
/// Represents the visual game board for the Tic-Tac-Toe game.
/// </summary>
public class GameBoard(BaseGame game) : BaseScreen
{
    /// <summary>
    /// Backing field for the tic tac toe game
    /// </summary>
    private readonly BaseGame _game = game;

    /// <summary>
    /// Height of each cell in the game board. Default is 3.
    /// </summary>
    public readonly int CellHeight = 3;

    /// <summary>
    /// Padding around each cell in the game board. Default is 3.
    /// </summary>
    public readonly int CellPadding = 3;

    /// <summary>
    /// The current selected row index on the game board.
    /// </summary>
    public int SelectedRow { get; set; }

    /// <summary>
    /// The current selected column index on the game board.
    /// </summary>
    public int SelectedCol { get; set; }

    /// <summary>
    /// Retrieves the border character with modifications if it is focused (highlighted).
    /// </summary>
    /// <param name="border">The original border character.</param>
    /// <param name="rowVariant">Indicates if a row variant of the border is required.</param>
    /// <param name="colVariant">Indicates if a column variant of the border is required.</param>
    /// <returns>The border character, modified if focused.</returns>
    private static char GetFocusedBorder(char border, bool rowVariant = false, bool colVariant = false)
    {
        return border switch
        {
            '┌' => '┏',
            '┐' => '┓',
            '└' => '┗',
            '┘' => '┛',
            '─' => '━',
            '│' => '┃',
            '├' => rowVariant ? '┡' : '┢',
            '┤' => rowVariant ? '┩' : '┪',
            '┬' => colVariant ? '┱' : '┲',
            '┴' => colVariant ? '┹' : '┺',
            '┼' => rowVariant
                ? (colVariant ? '╃' : '╄')
                : (colVariant ? '╅' : '╆'),
            _ => border,
        };
    }

    /// <inheritdoc />
    public override void Draw()
    {
        var board = new List<string>();
        var cellWidth = CellPadding * 2 + 1;
        var spacing = new string(' ', cellWidth);
        var padding = new string(' ', CellPadding);
        var separator = new string('─', cellWidth);

        board.Add($"┌{separator}{string.Concat(Enumerable.Repeat($"┬{separator}", _game.Board.Dimension - 1))}┐");
        for (int row = 0; row < _game.Board.Dimension; row++)
        {
            board.Add($"│{spacing}{string.Concat(Enumerable.Repeat($"│{spacing}", _game.Board.Dimension - 1))}│");

            var cells = new StringBuilder();
            cells.Append('│');
            for (int col = 0, index = row; col < _game.Board.Dimension; col++, index++)
                cells.Append($"{padding}{_game.Board[row, col]?.ToString() ?? " "}{padding}│");
            board.Add(cells.ToString());

            board.Add($"│{spacing}{string.Concat(Enumerable.Repeat($"│{spacing}", _game.Board.Dimension - 1))}│");
            if (row < _game.Board.Dimension - 1)
                board.Add($"├{separator}{string.Concat(Enumerable.Repeat($"┼{separator}", _game.Board.Dimension - 1))}┤");
        }
        board.Add($"└{separator}{string.Concat(Enumerable.Repeat($"┴{separator}", _game.Board.Dimension - 1))}┘");

        var lastSelectedRow = (SelectedRow + 1) * (CellHeight + 1);
        for (int row = SelectedRow * (CellHeight + 1); row <= lastSelectedRow; row++)
        {
            var rowChars = board[row].ToCharArray();
            var lastSelectedCol = (SelectedCol + 1) * (cellWidth + 1);
            for (int col = SelectedCol * (cellWidth + 1); col <= lastSelectedCol; col++)
            {
                rowChars[col] = GetFocusedBorder(rowChars[col], row == lastSelectedRow, col == lastSelectedCol);
            }
            board[row] = new string(rowChars);
        }

        DrawHeader();
        DrawPadding(_game.Board.Dimension * (CellHeight + 1) + 1);
        board.ForEach(row => DrawLine(row));
        DrawPadding(_game.Board.Dimension * (CellHeight + 1) + 1);
        var message = $"It's {_game.CurrentPlayer.Name}'s turn ({_game.CurrentHand}).";
        if (_game.Board.IsFull && _game.Winner == null)
            message = "It's a tie. Press ESCAPE key to continue.";
        else if (_game.Winner != null)
            message = $"{_game.Winner.Name} wins! Press ESCAPE key to continue.";
        DrawFooter(message);
    }

    /// <inheritdoc />
    public override void Input()
    {
        var consoleKey = Console.ReadKey(intercept: true).Key;
        switch (consoleKey)
        {
            case ConsoleKey.UpArrow:
                if (SelectedRow > 0)
                    SelectedRow--;
                break;
            case ConsoleKey.DownArrow:
                if (SelectedRow < _game.Board.Dimension - 1)
                    SelectedRow++;
                break;
            case ConsoleKey.LeftArrow:
                if (SelectedCol > 0)
                    SelectedCol--;
                break;
            case ConsoleKey.RightArrow:
                if (SelectedCol < _game.Board.Dimension - 1)
                    SelectedCol++;
                break;
            case ConsoleKey.X:
            case ConsoleKey.O:
                if (_game.CurrentHand?.ToString() != consoleKey.ToString() || _game.Winner != null)
                    break;
                _game.Board[SelectedRow, SelectedCol] = _game.CurrentHand;
                _game.UpdateWinner();
                _game.NextTurn();
                break;
            case ConsoleKey.Escape:
                Visible = false;
                break;
            default:
                break;
        }
    }

    /// <inheritdoc />
    public override void Reset()
    {
        Visible = true;
    }
}
