using Architectures.UMVCS.Model;
using Attributes;
using Data.Types;
using Project.Data.Types;
using Project.Snake.UMVCS.Controller;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    public class SnakeAIModel : BaseModel
    {

        public MainConfigData MainConfigData { get { return ConfigData as MainConfigData; } }
        public StateMachine StateMachine { get { return _stateMachine; } }

        public BlockConfigData HeadBlockType { get => _headBlockType; set => _headBlockType = value; }
        public List<SnakeBodyController> BodyList { get => _bodyList; set => _bodyList = value; }

        [SerializeField] private BlockConfigData _headBlockType;

        [SerializeField] private List<SnakeBodyController> _bodyList;

        private StateMachine _stateMachine = new StateMachine();

        public MainConfigData AIConfigData { get => ConfigData as MainConfigData; }

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableFloat Velocity = new ObservableFloat();

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableInt BodySize = new ObservableInt();

        public void InitializeStateMachine(SnakeController snakeController)
        {
            List<IState> states = new List<IState>();
            states.Add(new MovingState(snakeController));
            states.Add(new PickingState(snakeController));
            states.Add(new AddingBodyPartState(snakeController));
            _stateMachine.States = states;
        }

    }
}