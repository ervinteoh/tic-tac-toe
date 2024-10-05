using System.Text;
using TicTacToe.Models;

namespace TicTacToe.Views;

/// <summary>
/// Represents the visual game board for the Tic-Tac-Toe game.
/// </summary>
public class GameBoard(Board board) : BaseScreen
{
    /// <summary>
    /// Backing field for the board state
    /// </summary>
    private readonly Board _board = board;

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

        board.Add($"┌{separator}{string.Concat(Enumerable.Repeat($"┬{separator}", _board.Dimension - 1))}┐");
        for (int row = 0; row < _board.Dimension; row++)
        {
            board.Add($"│{spacing}{string.Concat(Enumerable.Repeat($"│{spacing}", _board.Dimension - 1))}│");

            var cells = new StringBuilder();
            cells.Append('│');
            for (int col = 0, index = row; col < _board.Dimension; col++, index++)
                cells.Append($"{padding}{_board[row, col]?.ToString() ?? " "}{padding}│");
            board.Add(cells.ToString());

            board.Add($"│{spacing}{string.Concat(Enumerable.Repeat($"│{spacing}", _board.Dimension - 1))}│");
            if (row < _board.Dimension - 1)
                board.Add($"├{separator}{string.Concat(Enumerable.Repeat($"┼{separator}", _board.Dimension - 1))}┤");
        }
        board.Add($"└{separator}{string.Concat(Enumerable.Repeat($"┴{separator}", _board.Dimension - 1))}┘");

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
        DrawPadding(_board.Dimension * (CellHeight + 1) + 1);
        board.ForEach(row => DrawLine(row));
        DrawPadding(_board.Dimension * (CellHeight + 1) + 1);
        DrawFooter("");
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
                if (SelectedRow < _board.Dimension - 1)
                    SelectedRow++;
                break;
            case ConsoleKey.LeftArrow:
                if (SelectedCol > 0)
                    SelectedCol--;
                break;
            case ConsoleKey.RightArrow:
                if (SelectedCol < _board.Dimension - 1)
                    SelectedCol++;
                break;
            case ConsoleKey.X:
            case ConsoleKey.O:
                throw new NotImplementedException();
            case ConsoleKey.Escape:
                return;
            default:
                break;
        }
    }
}
