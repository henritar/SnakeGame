using Architectures.UMVCS.Model;
using Attributes;
using Data.Types;
using Interfaces;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    public class SnakeBodyModel : BaseModel
    {

        private ISnake _snake = null;

        [SerializeField]
        private BlockConfigData _bodyBlockType = null;

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

        public ISnake Snake { get => _snake; set => _snake = value; }
    }
}