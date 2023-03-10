using Architectures.UMVCS.Model;
using Data.Types;
using Attributes;
using Project.Data.Types;
using Project.Snake.UMVCS.Controller;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    public class SnakeModel : BaseModel
    {
        public MainConfigData MainConfigData { get { return ConfigData as MainConfigData; } }
        public StateMachine StateMachine { get { return _stateMachine; } }

        public BlockConfigData HeadBlockType { get => _headBlockType; set => _headBlockType = value; }
        public List<SnakeBodyController> BodyList { get => _bodyList; set => _bodyList = value; }
        public bool TriggeredTimeTravel { get => triggeredTimeTravel; set => triggeredTimeTravel = value; }

        [SerializeField] private BlockConfigData _headBlockType;

        [SerializeField] private List<SnakeBodyController> _bodyList;

        [SerializeField] private bool triggeredTimeTravel;

        private StateMachine _stateMachine = new StateMachine();

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableInt BatteringRamCount = new ObservableInt();

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableInt TimeTravelCount = new ObservableInt();

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableFloat Velocity = new ObservableFloat();

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableInt BodySize = new ObservableInt();

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableVector3 Target = new ObservableVector3();

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableVector3 Direction = new ObservableVector3();

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
