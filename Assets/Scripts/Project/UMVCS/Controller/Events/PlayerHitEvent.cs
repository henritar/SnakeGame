using Events;
using UnityEngine.Events;
using Project.Snake.UMVCS.Controller;

namespace Project.UMVCS.Controller.Events
{
    public class PlayerHitEvent : UnityEvent<SnakePlayerController>, IEvent
    {
    }
}