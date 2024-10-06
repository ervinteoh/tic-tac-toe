namespace TicTacToe.Views;

/// <summary>
/// Abstract base class for rendering all screen-based interfaces.
/// Requires derived screens to implement Draw and Input methods.
/// </summary>
public abstract class BaseScreen
{
    /// <summary>
    /// The title of the application, displayed in the header. 
    /// </summary>
    private const string Title = @"
 _____ _        _____            _____          
|_   _(_)      |_   _|          |_   _|         
  | |  _  ___    | | __ _  ___    | | ___   ___ 
  | | | |/ __|   | |/ _` |/ __|   | |/ _ \ / _ \
  | | | | (__    | | (_| | (__    | | (_) |  __/
  \_/ |_|\___|   \_/\__,_|\___|   \_/\___/ \___|";

    /// <summary>
    /// The width of the content area inside the screen.
    /// </summary>
    protected const int ContentWidth = 60;

    /// <summary>
    /// The height of the content area inside the screen.
    /// </summary>
    protected const int ContentHeight = 19;

    /// <summary>
    /// Gets a value indicating whether the screen is currently visible.
    /// </summary>
    public bool Visible { get; protected set; } = true;

    /// <summary>
    /// Draws a single line with optional content, centered between borders.
    /// </summary>
    /// <param name="content">Optional string content to display. If null, the line is blank.</param>
    /// <param name="leftBorder">Character for the left border. Defaults to '║'.</param>
    /// <param name="rightBorder">Character for the right border. Defaults to '║'.</param>
    /// <exception cref="ArgumentException">Thrown if the content length exceeds the ContentWidth.</exception>
    protected static void DrawLine(string? content = null, char leftBorder = '║', char rightBorder = '║')
    {
        if (content != null && content.Length > ContentWidth)
        {
            throw new ArgumentException("Content length is too long.");
        }
        else if (content != null && content.Length < ContentWidth)
        {
            var padding = (ContentWidth - content.Length) / 2;
            var offset = ContentWidth % 2 != content.Length % 2 ? 1 : 0;
            content = $"{new string(' ', padding)}{content}{new string(' ', padding + offset)}";
        }

        Console.WriteLine($"{leftBorder}{content ?? new string(' ', ContentWidth)}{rightBorder}");
    }

    /// <summary>
    /// Draws the header, including the application title and a border line above and below.
    /// </summary>
    protected static void DrawHeader()
    {
        DrawLine(new string('═', ContentWidth), '╔', '╗');
        Title.Trim('\r', '\n').Split('\n').ToList().ForEach(line => DrawLine(line.TrimEnd('\r', '\n')));
        DrawLine();
        DrawLine(new string('═', ContentWidth), '╠', '╣');
    }

    /// <summary>
    /// Draws the footer with a provided message, and border lines above and below the message.
    /// </summary>
    /// <param name="text">The message to display in the footer.</param>
    protected static void DrawFooter(string text)
    {
        DrawLine(new string('═', ContentWidth), '╠', '╣');
        DrawLine(text);
        DrawLine(new string('═', ContentWidth), '╚', '╝');
    }

    /// <summary>
    /// Adds vertical padding by drawing empty lines above and below the content area.
    /// </summary>
    /// <param name="contentAreaHeight">The height of the area in which the content will be displayed.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if contentAreaHeight is negative.</exception>
    protected static void DrawPadding(int contentAreaHeight)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(contentAreaHeight);

        var paddingHeight = (ContentHeight - contentAreaHeight) / 2;
        Enumerable.Range(0, paddingHeight).ToList().ForEach(_ => DrawLine());
    }

    /// <summary>
    /// Draw the screen's content.
    /// </summary>
    public abstract void Draw();

    /// <summary>
    /// Handle user input on the screen.
    /// </summary>
    /// <returns>Returns <c>true</c> if the screen should stay opened; otherwise, <c>false</c></returns>
    public abstract void Input();

    /// <summary>
    /// Reset the screen.
    /// </summary>
    public abstract void Reset();
}
