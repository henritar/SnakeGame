using Commands;
using UnityEngine;

namespace Project.UMVCS.Controller.Commands
{
    public class ToggleStartMenuCommand : Command
    {
        [SerializeField]
        private bool _toggle;
        public bool Toggle { get => _toggle; }

        public ToggleStartMenuCommand(bool toggle) 
        {
            _toggle= toggle;
        }
    }
}