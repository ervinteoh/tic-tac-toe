namespace TicTacToe.Models;

/// <summary>
/// Represents a player in the TicTacToe game.
/// </summary>
public class Player
{
    /// <summary>
    /// Gets or sets the name of the player.
    /// </summary>
    /// <value>
    /// A string representing the name of the player.
    /// </value>
    public required string Name { get; set; }
}
