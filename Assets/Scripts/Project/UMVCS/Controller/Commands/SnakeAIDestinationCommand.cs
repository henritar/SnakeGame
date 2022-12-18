using Commands;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Project.UMVCS.Controller.Commands
{
    public class SnakeAIDestinationCommand : Command
    {
        public Vector3 Destination { get => _destination; }

        private Vector3 _destination;

        public SnakeAIDestinationCommand(Vector3 destination)
        {
            _destination = destination;
        }
    }
}