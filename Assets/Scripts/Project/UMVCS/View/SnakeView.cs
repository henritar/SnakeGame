using Architectures.UMVCS.View;
using Project.Snake.UMVCS.Controller;
using Project.Snake.UMVCS.Model;
using Project.UMVCS.Controller.Events;
using UnityEngine;

namespace Project.Snake.UMVCS.View
{
    public class SnakeView : BaseView
    {
        public PickBlockEvent OnPickBlock = new PickBlockEvent();

        public void MoveSnake(Vector3 position)
        {
            transform.position = position;
        }

       
        protected void OnTriggerEnter2D(Collider2D other)
        {
            BlockController block = other.gameObject.GetComponentInChildren<BlockController>();
            if (block)
            {
                OnPickBlock?.Invoke(block);
            }
        }
    }
}