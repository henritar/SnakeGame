using Architectures.UMVCS.Model;
using Attributes;
using Data.Types;
using Project.Snake.UMVCS.Controller;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    public class SnakeBodyModel : BaseModel
    {
        [SerializeField]
        private SnakeController _snake = null;

        [SerializeField]
        private MainModel _mainModelRef;

        [SerializeField]
        private BlockConfigData _bodyBlockType = null;

        [SerializeField]
        private BoxCollider2D _bodyCollider;

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableFloat Velocity = new ObservableFloat();

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableVector3 Target = new ObservableVector3();

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableInt WaitUps = new ObservableInt();

        public BlockConfigData BodyBlockType { get => _bodyBlockType; set => _bodyBlockType = value; }

        public SnakeController Snake { get => _snake; set => _snake = value; }
        public BoxCollider2D BodyCollider { get => _bodyCollider; set => _bodyCollider = value; }
        public MainModel MainModelRef { get => _mainModelRef; set => _mainModelRef = value; }
    }
}