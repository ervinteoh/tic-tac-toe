using TicTacToe.Models;
using TicTacToe.Views.Components;

namespace TicTacToe.Views;

public class GameMode : BaseScreen
{
    /// <summary>
    /// Menu options as form inputs.
    /// </summary>
    private readonly IList<FormInput> _inputs = [
        new FormSelect<Mode>(nameof(Mode), '→', ContentWidth * 80 / 100, true),
        new FormInput("Confirm", '↗', ContentWidth * 80 / 100, false),
    ];

    /// <summary>
    /// Game mode confirmed.
    /// </summary>
    public bool Confimed { get; private set; } = false;

    /// <inheritdoc />
    public override void Draw()
    {
        DrawHeader();
        DrawPadding(FormInput.Height * 2);
        _inputs.ToList().ForEach(input => input.ToString().Split('\n').ToList().ForEach(x => DrawLine(x)));
        DrawPadding(FormInput.Height * 2);
        DrawFooter("Use right arrow key to change mode.");
    }

    /// <inheritdoc />
    public override void Input()
    {
        var input = _inputs.FirstOrDefault(x => x.Focused, _inputs.First()); // Gets the focused input
        var index = _inputs.IndexOf(input); // Gets the index of the focused input
        switch (Console.ReadKey(intercept: true).Key)
        {
            case ConsoleKey.UpArrow:
                if (index > 0)
                {
                    input.Focused = false;
                    _inputs[index - 1].Focused = true; // Moves focus up
                }
                break;
            case ConsoleKey.DownArrow:
                if (index < _inputs.Count - 1)
                {
                    input.Focused = false;
                    _inputs[index + 1].Focused = true; // Moves focus down
                }
                break;
            case ConsoleKey.RightArrow:
                if (_inputs[index] is FormSelect<Mode> selectMode)
                    selectMode.Next();
                break;
            case ConsoleKey.Enter:
                if (index == _inputs.Count - 1)
                    Confimed = true;
                break;
            default:
                break;
        }
    }
}
