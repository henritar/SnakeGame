using Architectures.UMVCS.Model;
using Attributes;
using Data.Types;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    public class SnakeBodyModel : BaseModel
    {

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableFloat Velocity = new ObservableFloat();

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableVector3 Target = new ObservableVector3();

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableInt WaitUps = new ObservableInt();

    }
}