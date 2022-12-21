using Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Project.UMVCS.Controller.Commands
{
    public class AddNewPlayerCommand : Command
    {
        private HashSet<KeyCode> _keyCodes;
        public HashSet<KeyCode> KeyCode { get => _keyCodes; set => _keyCodes = value; }

        public AddNewPlayerCommand(HashSet<KeyCode> keys)
        {
            _keyCodes = keys;
        }
    }
}