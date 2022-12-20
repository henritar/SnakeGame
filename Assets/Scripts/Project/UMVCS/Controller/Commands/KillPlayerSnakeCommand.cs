using Commands;

namespace Project.UMVCS.Controller.Commands
{
    public class KillPlayerSnakeCommand : Command
    {
        private SnakePlayerController _playerSnake;

        public SnakePlayerController PlayerSnake { get => _playerSnake; set => _playerSnake = value; }

        public KillPlayerSnakeCommand(SnakePlayerController snake) 
        {
            _playerSnake = snake;
        }
    }
}