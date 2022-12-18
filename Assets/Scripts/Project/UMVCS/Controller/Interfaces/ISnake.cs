
using Architectures.UMVCS;
using Data.Types;
using Project.Snake.UMVCS.Controller;
using Project.Snake.UMVCS.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// This is a marker interface for all Snakes.
    /// </summary>
    public interface ISnake
    {
        public Context IContext { get; }
        public List<SnakeBodyController> BodyList { get; }
        public BlockConfigData HeadBlockType { get; set; }
        public ObservableFloat BodyVelocity { get; set; }

        public void SetHeadBlockType(BlockConfigData newType);

        public void AddBodyPart(SnakeBodyController bodyPart);

        public void ChangeSnakeVelocity(float modifier);
    }
}

