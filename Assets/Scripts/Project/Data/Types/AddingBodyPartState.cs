using Data.Types;
using Interfaces;
using Project.Snake;
using Project.Snake.UMVCS.Controller;
using Project.UMVCS.Controller.Commands;
using System;
using UnityEngine;

namespace Project.Data.Types
{
    public class AddingBodyPartState : BaseState
    {
        private ISnake _snakeController;

        private Type _nextState = null;

        private int _blocksToConsume = 0;
        
        public AddingBodyPartState(ISnake sc)
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
                _snakeController.IContext.CommandManager.AddCommandListener<LoadBlockCommand>(CommandManager_OnLoadBlockCommand);
                _snakeController.ChangeSnakeVelocity(-SnakeAppConstants.SnakeVelocityModifier);
            }
            _blocksToConsume++;
        }

        public override void ExitState()
        {
            _snakeController.IContext.CommandManager.RemoveCommandListener<LoadBlockCommand>(CommandManager_OnLoadBlockCommand);
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
                _snakeController.ChangeSnakeVelocity(SnakeAppConstants.SnakeVelocityModifier);
                _nextState = typeof(MovingState);
            }
        }

    }
}