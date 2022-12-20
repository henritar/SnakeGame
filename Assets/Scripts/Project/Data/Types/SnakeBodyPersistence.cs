using Project.Snake;
using Project.Snake.UMVCS.Controller;
using Project.Snake.UMVCS.Model;
using System;
using UnityEngine;

namespace Project.Data.Types
{
    [Serializable]
    public class SnakeBodyPersistence
    {
        public BlockTypeEnum BlockType;
        public Vector3 Position;
        public Vector3 Target;
        public float Velocity;
        public int WaitUps;

        public SnakeBodyPersistence(SnakeBodyModel bodyPart)
        {
            BlockType = bodyPart.BodyBlockType.BlockType;
            Position = bodyPart.transform.parent.transform.position;
            Target = bodyPart.Target.Value;
            Velocity = bodyPart.Velocity.Value + SnakeAppConstants.SnakeVelocityDebuffModifier;
            WaitUps = bodyPart.WaitUps.Value;
        }
    }
}