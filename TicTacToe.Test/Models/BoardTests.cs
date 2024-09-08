using TicTacToe.Models;

namespace TicTacToe.Test.Models;

[TestFixture]
public class BoardTests
{
    private const int Dimension = 3;
    private Board _board;

    [SetUp]
    public void Setup()
    {
        _board = new Board(Dimension);
    }

    /// <summary>
    /// Verifies that the <see cref="Board"/> constructor correctly initializes the dimension of the board.
    /// </summary>
    [Test]
    public void Constructor_WhenInstantiated_StoresDimensionCorrectly()
    {
        // Arrange
        var expectedDimension = Dimension;

        // Act
        var board = new Board(expectedDimension);

        // Assert
        Assert.That(board.Dimension, Is.EqualTo(expectedDimension));
    }

    /// <summary>
    /// Tests that setting a value using the <see cref="Board"/> indexer updates the board cell correctly.
    /// </summary>
    [Test]
    public void Indexer_Get_WhenValueSet_ReturnsCorrectValue()
    {
        // Arrange
        var expectedHand = Hand.X;
        _board[1, 1] = expectedHand;

        // Act
        var actualHand = _board[1, 1];

        // Assert
        Assert.That(actualHand, Is.EqualTo(expectedHand));
    }

    /// <summary>
    /// Tests that assigning a value using the <see cref="Board"/> indexer updates the board cell correctly.
    /// </summary>
    [Test]
    public void Indexer_Set_WhenValueAssigned_UpdatesValueCorrectly()
    {
        // Arrange
        var expectedHand = Hand.O;

        // Act
        _board[2, 2] = expectedHand;
        var actualHand = _board[2, 2];

        // Assert
        Assert.That(actualHand, Is.EqualTo(expectedHand));
    }

    /// <summary>
    /// Verifies that accessing an out-of-range row index via the <see cref="Board"/> indexer throws an
    /// <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    [Test]
    public void Indexer_Get_WhenRowOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        const int row = Dimension;
        const int col = 1;

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            // Access the indexer to trigger the exception
            var _ = _board[row, col];
        });

        // Assert
        Assert.That(exception.ParamName, Is.EqualTo(nameof(row)));
    }

    /// <summary>
    /// Verifies that assigning a value to an out-of-range column index via the <see cref="Board"/> indexer throws an
    /// <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    [Test]
    public void Indexer_Set_WhenColumnOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        const int row = 1;
        const int col = Dimension;

        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _board[row, col] = Hand.X);

        // Assert
        Assert.That(exception.ParamName, Is.EqualTo(nameof(col)));
    }
}
