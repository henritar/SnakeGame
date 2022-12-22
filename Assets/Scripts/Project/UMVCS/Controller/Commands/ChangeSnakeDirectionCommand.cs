using Commands;
using Project.Snake.UMVCS.Model;
using UnityEngine;

namespace Project.UMVCS.Controller.Commands
{
    public class ChangeSnakeDirectionCommand : Command
    {
        public Vector3 Direction { get => _direction; }
        public int Index { get => _index; set => _index = value; }

        private Vector3 _direction;

        private int _index;

        public ChangeSnakeDirectionCommand(Vector3 dir, int index)
        {
            _direction = dir;   
            _index = index;
        }
    }
}