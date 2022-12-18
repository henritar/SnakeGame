using Data.Types;
using Project.Snake.UMVCS.Controller;
using Project.Snake.UMVCS.Model;
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
            _snakeController.SnakeModel.HeadBlockType.ApplyPowerUp(_snakeController);
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