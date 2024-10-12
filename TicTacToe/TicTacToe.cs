using System.Text;
using TicTacToe.Games;
using TicTacToe.Models;
using TicTacToe.Views;

namespace TicTacToe;

/// <summary>
/// Represents the main logic for the Tic Tac Toe application.
/// </summary>
public class TicTacToe
{
    private readonly IList<Player> _players = [];
    private readonly MainMenu _mainMenu = new();
    private readonly GameMode _gameMode = new();
    private readonly ViewPlayers _viewPlayers;
    private BaseGame? _game;

    public TicTacToe()
    {
        _players.Add(new Player { Name = "PLAYER 1" });
        _players.Add(new Player { Name = "PLAYER 2" });
        _viewPlayers = new(_players);
        Console.OutputEncoding = Encoding.UTF8;
    }

    /// <summary>
    /// Plays the game, either starting a new game or continuing an existing one.
    /// </summary>
    /// <param name="newGame">Indicates whether to start a new game.</param>
    private void PlayGame(bool newGame = false)
    {
        if (newGame)
        {
            while (_gameMode.Selection == null)
                _gameMode.Render();
            _game = BaseGame.CreateGame(_gameMode.Selection.Value, [.. _players]);
            _gameMode.Reset();
        }
        else if (_game == null)
        {
            throw new InvalidOperationException("Game has not been started.");
        }

        var gameBoard = new GameBoard(_game);
        while (gameBoard.Visible)
            gameBoard.Render();

        _mainMenu.SetContinueButton(!_game.Board.IsFull && _game.Winner == null);
    }

    /// <summary>
    /// Displays the list of players and handles user interactions for viewing player details.
    /// </summary>
    private void ViewPlayers()
    {
        while (_viewPlayers.Visible)
            _viewPlayers.Render();
        _viewPlayers.Reset();
    }

    /// <summary>
    /// Displays the main menu and handles user selections.
    /// </summary>
    private void MainMenu()
    {
        while (_mainMenu.Visible)
        {
            _mainMenu.Render();

            switch (_mainMenu.Selection)
            {
                case Menu.NewGame:
                    PlayGame(true);
                    break;
                case Menu.ContinueGame:
                    PlayGame();
                    break;
                case Menu.ViewPlayers:
                    ViewPlayers();
                    break;
                default:
                    break;
            }

            if (_mainMenu.Selection != null)
                _mainMenu.Reset();
        }
    }

    /// <summary>
    /// The entry point for the Tic Tac Toe application.
    /// </summary>
    static void Main()
    {
        new TicTacToe().MainMenu();
    }
}
