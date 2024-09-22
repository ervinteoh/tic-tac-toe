using System.Text.RegularExpressions;

namespace TicTacToe.Views.Components;

/// <summary>
/// Represents a form input using a command enum, optional icon, and width.
/// </summary>
/// <param name="text">Input text.</param>
/// <param name="icon">Optional icon.</param>
/// <param name="width">Input width.</param>
/// <param name="focused">Focus state (default: false).</param>
public partial class FormInput(string text, char? icon, int width, bool focused = false)
{
    /// <summary>
    /// Constant height of the form input.
    /// </summary>
    public static readonly int Height = 3;

    /// <summary>
    /// Optional icon next to the input text.
    /// </summary>
    public char? Icon { get; } = icon;

    /// <summary>
    /// Width of the form input.
    /// </summary>
    public int Width { get; } = width;

    /// <summary>
    /// Whether the input is focused.
    /// </summary>
    public bool Focused { get; set; } = focused;

    /// <summary>
    /// Display text of the form input.
    /// </summary>
    public virtual string Text => text;

    /// <summary>
    /// Renders the input as a bordered string.
    /// </summary>
    /// <returns>A string representation of the form input.</returns>
    public override string ToString()
    {
        var input = string.Empty;

        var topLeft = Focused ? '╔' : '┌';
        var topRight = Focused ? '╗' : '┐';
        var bottomLeft = Focused ? '╚' : '└';
        var bottomRight = Focused ? '╝' : '┘';
        var vertical = Focused ? '║' : '│';
        var horizontal = Focused ? '═' : '─';
        var padding = new string(' ', 2);
        var spacing = new string(' ', Width - 4 - Text.Length - (Icon == null ? 0 : 1));

        input += $"{topLeft}{new string(horizontal, Width)}{topRight}\n";
        input += $"{vertical}{padding}{Text}{spacing}{Icon}{padding}{vertical}\n";
        input += $"{bottomLeft}{new string(horizontal, Width)}{bottomRight}";

        return input;
    }

    /// <summary>
    /// Regex pattern to add spaces before capital letters in PascalCase strings.
    /// </summary>
    /// <returns>A regex object that adds spaces in PascalCase strings.</returns>
    [GeneratedRegex("(\\B[A-Z])")]
    protected static partial Regex MedialCapitals();
}
