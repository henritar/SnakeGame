using Data.Types;
using Interfaces;
using Project.Snake.UMVCS.Controller;
using System;
using UnityEngine;

namespace Project.Data.Types
{
    public class PickingState : BaseState
    {

        private ISnake _snakeController;

        public PickingState(ISnake sc)
        {
            _snakeController = sc;
        }

        public override void DestroyState()
        {
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
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