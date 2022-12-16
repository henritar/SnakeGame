using Data.Types;
using Project.Snake.UMVCS.Controller;
using System;
using UnityEngine;

namespace Project.Data.Types
{
    public class PickingState : BaseState
    {

        private SnakeController _snakeController;

        public PickingState(SnakeController sc)
        {
            _snakeController = sc;
        }

        public override void DestroyState()
        {
        }

        public override void EnterState()
        {
            Debug.Log("Entered PickingState");
        }

        public override void ExitState()
        {
            Debug.Log("Exiting PickingState");
        }

        public override void InitializeState()
        {
            
        }

        public override Type UpdateState()
        {
            return typeof(AddingBodyPartState);
        }

        
    }
}