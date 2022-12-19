using Events;
using Project.Snake.UMVCS.Controller;
using UnityEngine.Events;

namespace Project.UMVCS.Controller.Events
{
    public class SnakeHitSnakeEvent : UnityEvent<SnakeController>, IEvent
    {
    }
}