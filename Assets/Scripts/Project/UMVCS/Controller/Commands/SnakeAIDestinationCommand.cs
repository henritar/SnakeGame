using Commands;
using System.Collections;
using UnityEngine;

namespace Project.UMVCS.Controller.Commands
{
    public class SnakeAIDestinationCommand : Command
    {
        public int Index { get => _index; }
        public Vector3 Destination { get => _destination; }

        private Vector3 _destination;

        private int _index;

        public SnakeAIDestinationCommand(Vector3 destination, int index)
        {
            _destination = destination;
            _index = index;
        }
    }
}