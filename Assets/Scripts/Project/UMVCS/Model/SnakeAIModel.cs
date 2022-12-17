using Architectures.UMVCS.Model;
using Attributes;
using Data.Types;
using System.Collections;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    public class SnakeAIModel : BaseModel
    {
        public MainConfigData AIConfigData { get => ConfigData as MainConfigData; }

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableFloat Velocity = new ObservableFloat();

        [Observable(IsEditable = false)]
        [SerializeField]
        public ObservableInt BodySize = new ObservableInt();
    }
}