using TicTacToe.Models;

namespace TicTacToe.Games;

/// <summary>
/// Represents the base class for a Tic Tac Toe game.
/// </summary>
public abstract class BaseGame
{
    /// <summary>
    /// Gets the dimension of the game board.
    /// </summary>
    protected abstract int Dimension { get; }

    /// <summary>
    /// The game board.
    /// </summary>
    public readonly Board Board;

    /// <summary>
    /// An array of players participating in the game.
    /// </summary>
    public readonly Player[] Players;

    /// <summary>
    /// The player who has won the game, if any.
    /// </summary>
    public Player? Winner { get; protected set; }

    /// <summary>
    /// The current hand (turn) in the game.
    /// </summary>
    public Hand? CurrentHand { get; protected set; } = Hand.X;

    /// <summary>
    /// The current player (turn) in the game.
    /// </summary>
    public Player CurrentPlayer { get; protected set; }

    protected BaseGame(Player[] players)
    {
        Players = players;
        Board = new Board(Dimension);
        CurrentPlayer = Players[0];
    }

    /// <summary>
    /// Advances the game to the next turn.
    /// </summary>
    /// <remarks>
    /// If the board is full or a winner is already declared, the turn will not change.
    /// </remarks>
    public virtual void NextTurn()
    {
        if (Board.IsFull || Winner != null)
            return;

        CurrentHand = CurrentHand == Hand.X ? Hand.O : Hand.X;
        var currentPlayerIndex = Array.IndexOf(Players, CurrentPlayer);
        CurrentPlayer = Players[(currentPlayerIndex + 1) % Players.Length];
    }

    /// <summary>
    /// Updates the winner of the game based on the current state of the board.
    /// </summary>
    public abstract void UpdateWinner();

    /// <summary>
    /// Creates an instance of a Tic Tac Toe game based on the specified game mode.
    /// </summary>
    /// <param name="mode">The game mode.</param>
    /// <param name="players">The players participating in the game.</param>
    /// <returns>A new instance of a <see cref="BaseGame"/> derived class, representing the selected game mode.</returns>
    public static BaseGame CreateGame(Mode mode, Player[] players)
    {
        return mode switch
        {
            Mode.NormalTicTacToe => new NormalTicTacToe(players),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), "The game mode is unsupported.")
        };
    }
}
