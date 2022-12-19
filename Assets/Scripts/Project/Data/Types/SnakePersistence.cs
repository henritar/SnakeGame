using Project.Snake.UMVCS.Model;
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

    public class SnakePersistence
    {
        public SnakeTypeEnum Type;
        public string Name;
        public BlockConfigData HeadType;
        public Transform Position;
        public Vector3 Target;
        public Vector3 Direction;
        public float Velocity;
        public int BatteringRamCount;
        public List<SnakeBodyPersistence> BodyParts;
    }
}