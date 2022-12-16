using Assets.Utils.Runtime.Managers;
using Data.Types;
using Project.Snake;
using Project.Snake.UMVCS.Controller;
using System;
using System.Collections;

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
            _snakeController.ChangeSnakeVelocity(-SnakeAppConstants.SnakeVelocityModifier);
        }

        public override void ExitState()
        {
            CoroutinerManager.Start(ExitStateCoroutine());
        }

        public override void InitializeState()
        {
            
        }

        public override Type UpdateState()
        {
            return typeof(MovingState);
        }

        private IEnumerator ExitStateCoroutine()
        {
            
            yield return CoroutinerManager.WaitOneSecond;
            _snakeController.ChangeSnakeVelocity(SnakeAppConstants.SnakeVelocityModifier);
        }
    }
}