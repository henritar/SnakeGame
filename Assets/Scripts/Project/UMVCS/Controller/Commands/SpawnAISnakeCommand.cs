using Commands;
using Project.Snake.UMVCS.Controller;

namespace Project.UMVCS.Controller.Commands
{
    public class SpawnAISnakeCommand : Command
    {
        public bool ResetSnake { get => _resetSnake; }
        public SnakeAIController SnakeAI { get => _snakeAI; }
        public int Index { get => _index; }

        private bool _resetSnake;

        private int _index;

        private SnakeAIController _snakeAI;
        public SpawnAISnakeCommand(int index, SnakeAIController snake = null, bool resetSnake = false) 
        {
            _index= index;
            _snakeAI = snake;
            _resetSnake = resetSnake;
        }

    }
}