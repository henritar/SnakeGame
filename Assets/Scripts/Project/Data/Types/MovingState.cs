using Data.Types;
using Interfaces;
using Project.Snake.UMVCS.Controller;
using System;

namespace Project.Data.Types
{
    public class MovingState : BaseState
    {
        private ISnake _snakeController;
        public MovingState(ISnake sc)
        {
            _snakeController = sc;
        }

        public override void DestroyState()
        {
            base.DestroyState();
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void InitializeState()
        {
            base.InitializeState();
        }

        public override Type UpdateState()
        {
            return base.UpdateState();
        }
    }
}