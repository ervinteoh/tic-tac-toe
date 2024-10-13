using TicTacToe.Views.Components;

namespace TicTacToe.Views;

/// <summary>
/// Represents the main menu screen with navigation using arrow keys and selection with Enter.
/// </summary>
public class MainMenu : BaseScreen
{
    /// <summary>
    /// Menu options as form inputs.
    /// </summary>
    private readonly IList<FormButton<Menu>> _inputs = [
        new(Menu.NewGame, '→', ContentWidth * 80 / 100, true), // Focused by default
        new(Menu.ViewPlayers, '↗', ContentWidth * 80 / 100),
        new(Menu.ExitGame, '←', ContentWidth * 80 / 100),
    ];

    /// <summary>
    /// Continue button.
    /// </summary>
    private readonly FormButton<Menu> _continueButton = new(Menu.ContinueGame, '→', ContentWidth * 80 / 100);

    /// <summary>
    /// Selected menu option.
    /// </summary>
    public Menu? Selection { get; private set; }

    /// <summary>
    /// Sets the visibility of the continue button.
    /// </summary>
    /// <param name="isVisible">Button is visible.</param>
    public void SetContinueButton(bool isVisible)
    {
        if (_inputs.Contains(_continueButton))
            return;

        if (isVisible)
            _inputs.Insert(0, _continueButton);
        else
            _inputs.Remove(_continueButton);
    }

    /// <inheritdoc />
    public override void Draw()
    {
        DrawHeader();
        DrawPadding(_inputs.Count * FormButton<Menu>.Height);
        _inputs.ToList().ForEach(input => input.ToString().Split('\n').ToList().ForEach(x => DrawLine(x)));
        DrawPadding(_inputs.Count * FormButton<Menu>.Height);
        DrawFooter("Use arrow keys to move, press Enter to select.");
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
            case ConsoleKey.Enter:
                Selection = input.Command; // Selects the focused input
                break;
            default:
                break;
        }
        Visible = Selection != Menu.ExitGame;
    }

    /// <inheritdoc />
    public override void Reset()
    {
        Selection = null;
        _inputs.ToList().ForEach(x => x.Focused = false);
        _inputs.First().Focused = true;
    }
}

/// <summary>
/// Represents the options available in the main menu.
/// </summary>
public enum Menu
{
    /// <summary>
    /// Starts a new game.
    /// </summary>
    NewGame,

    /// <summary>
    /// Continues an existing game.
    /// </summary>
    ContinueGame,

    /// <summary>
    /// Views the list of players.
    /// </summary>
    ViewPlayers,

    /// <summary>
    /// Exits the game.
    /// </summary>
    ExitGame
}
