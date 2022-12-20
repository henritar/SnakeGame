using Project.Snake.UMVCS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Data.Types
{
    [System.Serializable]
    public enum SnakeTypeEnum
    {
        Player = 0,
        AI = 1,
    }

    [Serializable]
    public class SnakePersistence
    {
        public SnakeTypeEnum Type;
        public string Name;
        public BlockTypeEnum HeadBlockType;
        public Vector3 Position;
        public Vector3 Target;
        public Vector3 Direction;
        public float Velocity;
        public int BatteringRamCount;
        public List<SnakeBodyPersistence> BodyList;

        public SnakePersistence(SnakeModel sm)
        {
            Type = sm is SnakePlayerModel ? SnakeTypeEnum.Player : SnakeTypeEnum.AI;
            Name = sm.name;
            HeadBlockType = sm.HeadBlockType.BlockType;
            Position = sm.transform.parent.transform.position;
            Target = sm.Target.Value;
            Direction = sm.Direction.Value;
            Velocity = sm.Velocity.Value;
            BatteringRamCount = sm.BatteringRamCount.Value;
            BodyList = new List<SnakeBodyPersistence>();

            foreach (var bodyPart in sm.BodyList)
            {
                BodyList.Add(new SnakeBodyPersistence(bodyPart.SnakeBodyModel));
            }
        }
    }
}