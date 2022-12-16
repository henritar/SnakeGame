using Architectures.UMVCS.View;
using UnityEngine;

namespace Project.Snake.UMVCS.View
{
    public class SnakeBodyView : BaseView
    {
        public void MoveBodyPart(Vector3 position)
        {
            transform.position = position;
        }
    }
}