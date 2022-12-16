using Commands;
using Project.Snake.UMVCS.Model;

namespace Project.UMVCS.Controller.Commands
{
    public class AddBodyPartCommand : Command
    {

        public BlockConfigData BlockType { get => _blockType; }

        private BlockConfigData _blockType;

        public AddBodyPartCommand(BlockConfigData type)
        {
            _blockType = type;
        }
    }
}