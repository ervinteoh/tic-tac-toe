using TicTacToe.Models;

namespace TicTacToe;

/// <summary>
/// Represents a game board with a fixed dimension.
/// </summary>
public class Board(int dimension)
{
    private readonly Hand?[,] _cells = new Hand?[dimension, dimension];

    /// <summary>
    /// Gets the dimension of the board (number of rows/columns).
    /// </summary>
    public int Dimension { get; } = dimension;

    /// <summary>
    /// Gets or sets the <see cref="Hand"/> at the specified row and column.
    /// </summary>
    /// <param name="row">The row index (0-based).</param>
    /// <param name="col">The column index (0-based).</param>
    /// <returns>The <see cref="Hand"/> at the specified location.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the row or column index is out of range of the board dimensions.
    /// </exception>
    public Hand? this[int row, int col]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(row, 0, nameof(row));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, Dimension, nameof(row));
            ArgumentOutOfRangeException.ThrowIfLessThan(col, 0, nameof(col));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(col, Dimension, nameof(col));

            return _cells[row, col];
        }
        set
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(row, 0, nameof(row));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, Dimension, nameof(row));
            ArgumentOutOfRangeException.ThrowIfLessThan(col, 0, nameof(col));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(col, Dimension, nameof(col));

            _cells[row, col] = value;
        }
    }
}
