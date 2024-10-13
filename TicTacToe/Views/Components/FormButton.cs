namespace TicTacToe.Views.Components;

/// <summary>
/// Represents a form button that executes a command.
/// </summary>
/// <param name="command">Associated command.</param>
/// <param name="icon">Optional icon.</param>
/// <param name="width">Input width.</param>
/// <param name="focused">Focus state (default: false).</param>
/// <typeparam name="TEnum">The type of the command enum.</typeparam>
public class FormButton<TEnum>(TEnum command, char? icon, int width, bool focused = false) :
    FormInput(command.ToString(), icon, width, focused) where TEnum : Enum
{
    /// <inheritdoc />
    public override string Text => MedialCapitals().Replace(base.Text, " $1");

    /// <summary>
    /// Command associated with the button.
    /// </summary>
    public TEnum Command { get; } = command;
}
