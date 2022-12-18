using Attributes;
using Data.Types;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Snake.UMVCS.Model
{
    public class SnakeAIModel : SnakeModel
    {
        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableVector3 BlockPosition = new ObservableVector3();


    }
}