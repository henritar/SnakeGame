using Commands;
using Project.Snake.UMVCS.Model;

namespace Project.UMVCS.Controller.Commands
{
    public class LoadPersistedDataCommand : Command
    {
        public string PersistedData { get => _persistedData; }

        private string _persistedData;
        public LoadPersistedDataCommand(string persistedData)
        {
            _persistedData = persistedData;
        }
    }
}