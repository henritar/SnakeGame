using Commands;

namespace Project.UMVCS.Controller.Commands
{
    public class ChangeNumberPlayer : Command
    {
        private int _numberOfPlayers;

        public int NumberOfPlayers { get => _numberOfPlayers; }

        public ChangeNumberPlayer(int number) 
        {
            _numberOfPlayers = number;    
        }
    }
}