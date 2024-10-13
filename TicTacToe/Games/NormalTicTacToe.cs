using TicTacToe.Models;

namespace TicTacToe.Games;

/// <summary>
/// Represents a normal Tic Tac Toe game.
/// </summary>
public class NormalTicTacToe(Player[] players) : BaseGame(players)
{
    /// <inheritdoc />
    protected override int Dimension => 3;

    /// <inheritdoc />
    public override void UpdateWinner()
    {
        // Check rows
        for (int row = 0; row < Dimension; row++)
        {
            Hand?[] hands = new Hand?[Dimension];
            for (int col = 0; col < Dimension; col++)
                hands[col] = Board[row, col];
            if (hands.All(hand => hand.HasValue && hand == hands[0]))
                Winner = CurrentPlayer;
        }

        // Check columns
        for (int col = 0; col < Dimension; col++)
        {
            Hand?[] hands = new Hand?[Dimension];
            for (int row = 0; row < Dimension; row++)
                hands[row] = Board[row, col];
            if (hands.All(hand => hand.HasValue && hand == hands[0]))
                Winner = CurrentPlayer;
        }

        // Check primary diagonal
        Hand?[] primaryDiagonal = new Hand?[Dimension];
        for (int index = 0; index < Dimension; index++)
            primaryDiagonal[index] = Board[index, index];
        if (primaryDiagonal.All(hand => hand.HasValue && hand == primaryDiagonal[0]))
            Winner = CurrentPlayer;

        // Check secondary diagonal
        Hand?[] secondaryDiagonal = new Hand?[Dimension];
        for (int i = 0; i < Dimension; i++)
            secondaryDiagonal[i] = Board[i, Dimension - 1 - i];
        if (secondaryDiagonal.All(hand => hand.HasValue && hand == secondaryDiagonal[0]))
            Winner = CurrentPlayer;
    }
}
