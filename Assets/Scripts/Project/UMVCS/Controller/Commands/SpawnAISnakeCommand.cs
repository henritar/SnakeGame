using Commands;
using Project.Snake.UMVCS.Controller;

namespace Project.UMVCS.Controller.Commands
{
    public class SpawnAISnakeCommand : Command
    {
        public bool ResetSnake { get => _resetSnake; }
        public SnakeAIController SnakeAI { get => _snakeAI; }

        private bool _resetSnake;

        private SnakeAIController _snakeAI;
        public SpawnAISnakeCommand(SnakeAIController snake = null, bool resetSnake = false) 
        {
            _snakeAI = snake;
            _resetSnake = resetSnake;
        }

    }
}