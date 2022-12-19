using Project.Snake.UMVCS.Model;
using UnityEngine;

namespace Project.Data.Types
{
    public class SnakeBodyPersistence
    {
        public BlockConfigData BlockType;
        public Transform Position;
        public Transform Target;
        public float Velocity;
        public int WaitUps;
    }
}