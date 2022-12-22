using Data.Types;
using Project.Snake;
using Project.Snake.UMVCS.Controller;
using Project.UMVCS.Controller.Commands;
using System;

namespace Project.Data.Types
{
    public class AddingBodyPartState : BaseState
    {
        private SnakeController _snakeController;

        private Type _nextState = null;

        private int _blocksToConsume = 0;
        
        public AddingBodyPartState(SnakeController sc)
        {
            _snakeController = sc;
            _blocksToConsume = 0;
         }

        public override void DestroyState()
        {
            base.DestroyState();
        }

        public override void EnterState()
        {
            _nextState = null;
            if (_blocksToConsume == 0 ) {
                _snakeController.Context.CommandManager.AddCommandListener<LoadBlockCommand>(CommandManager_OnLoadBlockCommand);
                _snakeController.ChangeSnakeVelocity(-SnakeAppConstants.SnakeVelocityDebuffModifier);
            }
            _blocksToConsume++;
        }

        public override void ExitState()
        {
            _snakeController.Context.CommandManager.RemoveCommandListener<LoadBlockCommand>(CommandManager_OnLoadBlockCommand);
            _snakeController.RestoreSnakeVelocity();
        }

        public override void InitializeState()
        {
            base.InitializeState();
        }

        public override Type UpdateState()
        {
            return _nextState;
        }

        private void CommandManager_OnLoadBlockCommand(LoadBlockCommand e)
        {
            _blocksToConsume--;
            if(_blocksToConsume == 0 )
            {
                _snakeController.ChangeSnakeVelocity(SnakeAppConstants.SnakeVelocityDebuffModifier);
                _nextState = typeof(MovingState);
            }
        }

    }
}