using Architectures.UMVCS.View;
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
            if (other.gameObject.GetComponent<BlockView>())
            {
                OnPickBlock?.Invoke();
            }
        }
    }
}