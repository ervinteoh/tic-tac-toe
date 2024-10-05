using TicTacToe.Models;
using TicTacToe.Views.Components;

namespace TicTacToe.Views;

public class ViewPlayers : BaseScreen
{
    private readonly IList<Player> _players;

    private readonly IList<FormInput> _inputs = [];

    public bool ReturnToMainMenu { get; private set; } = false;

    public ViewPlayers(IList<Player> players)
    {
        _players = players;
        players.Select(x => new FormInput(x.Name, null, ContentWidth * 80 / 100, false)).ToList().ForEach(_inputs.Add);
        _inputs.Add(new("Back to Main Menu", '←', ContentWidth * 80 / 100));
        _inputs[0].Focused = true;
    }

    /// <inheritdoc />
    public override void Draw()
    {
        DrawHeader();
        DrawPadding(_players.Count * FormInput.Height);
        _inputs.ToList().ForEach(input => input.ToString().Split('\n').ToList().ForEach(x => DrawLine(x)));
        DrawPadding(_players.Count * FormInput.Height);
        DrawFooter("Use arrow keys to move and change player names.");
    }

    /// <inheritdoc />
    public override void Input()
    {
        var input = _inputs.FirstOrDefault(x => x.Focused, _inputs.First()); // Gets the focused input
        var index = _inputs.IndexOf(input); // Gets the index of the focused input
        var consoleKey = Console.ReadKey(intercept: true).Key;
        switch (consoleKey)
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
            case ConsoleKey.Enter:
                ReturnToMainMenu = _inputs[_inputs.Count - 1].Focused;
                if (ReturnToMainMenu)
                    _players.ToList().ForEach(x => x.Name = _inputs[_players.IndexOf(x)].Text);
                break;
            case ConsoleKey.Backspace:
                if (index == _players.Count)
                    break;
                if (_inputs[index].Text.Length > 0)
                    _inputs[index].Text = _inputs[index].Text[..^1];
                break;
            default:
                if (index == _players.Count)
                    break;

                if ((consoleKey < ConsoleKey.A || consoleKey > ConsoleKey.Z) &&
                    (consoleKey < ConsoleKey.D0 || consoleKey > ConsoleKey.D9) &&
                    consoleKey != ConsoleKey.Spacebar)
                {
                    break;
                }

                char typedChar = (char)consoleKey;
                if (char.IsLetterOrDigit(typedChar) || char.IsWhiteSpace(typedChar))
                    _inputs[index].Text += typedChar;
                break;
        }
    }
}
