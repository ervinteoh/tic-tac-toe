namespace TicTacToe.Views.Components;

/// <summary>
/// Represents a form input that allows enum selection.
/// </summary>
/// <param name="text">Input text.</param>
/// <param name="icon">Optional icon.</param>
/// <param name="width">Input width.</param>
/// <param name="focused">Focus state (default: false).</param>
/// <typeparam name="TSelect">The type of the selection enum.</typeparam>
public class FormSelect<TSelect>(string text, char icon, int width, bool focused = false) :
    FormInput(text, icon, width, focused) where TSelect : Enum
{
    /// <summary>
    /// Gets the current selection of the enum type <typeparamref name="TSelect"/>.
    /// </summary>
    public TSelect Selection { get; private set; } = default!;

    /// <inheritdoc />
    public override string Text => $"{base.Text}: {MedialCapitals().Replace(Selection.ToString(), " $1")}";

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
