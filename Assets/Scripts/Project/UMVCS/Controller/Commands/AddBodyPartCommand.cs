using Commands;
using Project.Snake.UMVCS.Controller;
using Project.Snake.UMVCS.Model;

namespace Project.UMVCS.Controller.Commands
{
    public class AddBodyPartCommand : Command
    {

        public BlockController BlockPicked { get => _blockPicker; }
        public SnakeController PickSnake { get => _pickSnake; }

        private BlockController _blockPicker;

        private SnakeController _pickSnake;
        public AddBodyPartCommand(SnakeController snake, BlockController block)
        {
            _blockPicker = block;
            _pickSnake = snake;
        }
    }
}