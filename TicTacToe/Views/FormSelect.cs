namespace TicTacToe.Views;

/// <summary>
/// Represents a form input that allows selection of a command and mode.
/// </summary>
/// <typeparam name="TEnum">The type of the command enum.</typeparam>
/// <typeparam name="TSelect">The type of the selection enum.</typeparam>
public class FormSelect<TEnum, TSelect>(TEnum command, char icon, int width, bool focused = false) :
    FormInput<TEnum>(command, icon, width, focused) where TEnum : Enum where TSelect : Enum
{
    /// <summary>
    /// Gets the current selection of the enum type <typeparamref name="TSelect"/>.
    /// </summary>
    public TSelect Selection { get; private set; } = default!;

    /// <inheritdoc />
    public override string Text => $"Mode: {MedialCapitals().Replace(Selection.ToString(), " $1")}";

    /// <summary>
    /// Advances the current selection to the next enum value in <typeparamref name="TSelect"/>.
    /// Wraps around to the first value when the last value is reached.
    /// </summary>
    public void Next()
    {
        var values = (TSelect[])Enum.GetValues(typeof(TSelect));
        int currentIndex = Array.IndexOf(values, Selection);
        Selection = values[(currentIndex + 1) % values.Length];
    }
}
