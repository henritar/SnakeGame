using Commands;
using Project.Snake.UMVCS.Model;
using UnityEngine;

namespace Project.UMVCS.Controller.Commands
{
    public class ChangeSnakeDirectionCommand : Command
    {
        public Vector3 Direction { get => _direction; }

        private Vector3 _direction;

        public ChangeSnakeDirectionCommand(Vector3 dir)
        {
            _direction = dir;   
        }
    }
}