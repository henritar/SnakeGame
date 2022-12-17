using Commands;
using Interfaces;
using Project.Snake.UMVCS.Controller;
using Project.Snake.UMVCS.Model;

namespace Project.UMVCS.Controller.Commands
{
    public class AddBodyPartCommand : Command
    {

        public BlockController BlockPicked { get => _blockPicker; }
        public ISnake PickSnake { get => _pickSnake; }

        private BlockController _blockPicker;

        private ISnake _pickSnake;
        public AddBodyPartCommand(ISnake snake, BlockController block)
        {
            _blockPicker = block;
            _pickSnake = snake;
        }
    }
}