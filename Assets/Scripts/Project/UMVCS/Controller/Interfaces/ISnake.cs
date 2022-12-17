
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
        public List<SnakeBodyController> BodyList { get; }
        public BlockConfigData HeadBlockType { get; set; }
        public float BodyVelocity { get; set; }

        public void SetHeadBlockType(BlockConfigData newType);

        public void AddBodyPart(SnakeBodyController bodyPart);
    }
}

