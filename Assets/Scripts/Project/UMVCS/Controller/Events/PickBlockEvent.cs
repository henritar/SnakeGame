using Events;
using Project.Snake.UMVCS.Controller;
using Project.Snake.UMVCS.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Project.UMVCS.Controller.Events
{
    public class PickBlockEvent : UnityEvent<BlockController>, IEvent
    {
    }
}
