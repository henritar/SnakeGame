using Commands;
using UnityEngine;

namespace Project.UMVCS.Controller.Commands
{
    public class ChangeSnakeTypeCommand : Command
    {
        private KeyCode _keyCode;

        public KeyCode KeyCode { get => _keyCode; }

        public ChangeSnakeTypeCommand(KeyCode keyCode)
        {
            _keyCode = keyCode;
        }
    }
}