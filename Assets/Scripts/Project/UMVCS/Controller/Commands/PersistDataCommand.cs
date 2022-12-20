using Commands;
using Project.Snake.UMVCS.Model;

namespace Project.UMVCS.Controller.Commands
{
    public class PersistDataCommand: Command
    {
        public BlockConfigData Block { get => _block; }

        private BlockConfigData _block;
        public PersistDataCommand(BlockConfigData block) 
        {
            _block = block;
        }

    }
}