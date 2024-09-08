using TicTacToe.Models;

namespace TicTacToe.Test.Models;

[TestFixture]
public class PlayerTests
{
    /// <summary>
    /// Verifies that the <see cref="Player"/> class correctly stores the name when it is instantiated with a name.
    /// </summary>
    [Test]
    public void Name_Set_WhenInstantiated_StoresNameCorrectly()
    {
        // Arrange
        var name = "John Doe";

        // Act
        var player = new Player()
        {
            Name = name
        };

        // Assert
        Assert.That(player.Name, Is.EqualTo(name));
    }
}
