using Project.Snake.UMVCS.Model;
using UnityEngine;

namespace Project.Data.Types
{
    public class SnakeAIPersistence : SnakePersistence
    {
        public Vector3 BlockPosition;

        public SnakeAIPersistence(SnakeAIModel sm) : base(sm)
        {
            BlockPosition = sm.BlockPosition.Value;
        }
    }
}