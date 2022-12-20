using Commands;

namespace Assets.Scripts.Project.UMVCS.Controller.Commands
{
    public class SpawnBlockCommand : Command
    {
        private int _index;

        public int Index { get => _index; }

        public SpawnBlockCommand(int index) { _index = index; }
    }
}