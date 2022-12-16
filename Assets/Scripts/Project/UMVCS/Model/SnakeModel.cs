using Architectures.UMVCS.Model;
using Data.Types;
using Project.Data.Types;
using Project.Snake.UMVCS.Controller;
using System.Collections.Generic;

namespace Project.Snake.UMVCS.Model
{
    public class SnakeModel : BaseModel
    {
        public MainConfigData MainConfigData { get { return ConfigData as MainConfigData; } }
        public StateMachine StateMachine { get { return _stateMachine; } }
        private StateMachine _stateMachine = new StateMachine();

        public void InitializeStateMachine(SnakeController snakeController)
        {
            List<IState> states = new List<IState>();
            states.Add(new MovingState(snakeController));
            states.Add(new PickingState(snakeController));
            _stateMachine.States = states;
        }
    }
}
