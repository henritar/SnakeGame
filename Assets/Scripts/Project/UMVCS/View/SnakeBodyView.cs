using Architectures.UMVCS.View;
using Project.Snake.UMVCS.Controller;
using Project.UMVCS.Controller.Events;
using UnityEngine;

namespace Project.Snake.UMVCS.View
{
    public class SnakeBodyView : BaseView
    {
        public AIHitEvent OnAIHitEvent = new AIHitEvent();
        public PlayerHitEvent OnPlayerHitEvent = new PlayerHitEvent();

        public void MoveBodyPart(Vector3 position)
        {
            transform.position = position;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            SnakeController sc = other.gameObject.GetComponentInChildren<SnakeController>();
            if (sc == null) { return; }

            if (sc.SnakeModel.BatteringRamCount.Value > 0)
            {
                sc.ChangeBatteringRamCount(-1);
            }
            else if (sc.SnakeModel.TimeTravelCount.Value > 0)
            {
                sc.ChangeTimeTravelCount(-1);
            }
            else if (sc as SnakeAIController)
            {
                OnAIHitEvent?.Invoke(sc as SnakeAIController);
            }
            else if(sc as SnakePlayerController)
            {
                OnPlayerHitEvent?.Invoke(sc as SnakePlayerController);
            }
        }
    }
}