using Project.Snake;
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
        public int BlocksToConsume;
        public int BatteringRamCount;
        public string TimeTravelPersistedData;
        public int TimeTravelCount;
        public Vector3 BlockPosition;
        public List<SnakeBodyPersistence> BodyList;

        public SnakePersistence(SnakeModel sm)
        {
            Type = sm is SnakePlayerModel ? SnakeTypeEnum.Player : SnakeTypeEnum.AI;
            Name = sm.name;
            HeadBlockType = sm.TriggeredTimeTravel ? sm.BodyList[0].BaseModel.BodyBlockType.BlockType : sm.HeadBlockType.BlockType;
            Position = sm.transform.parent.transform.position;
            Target = sm.Target.Value;
            Direction = sm.Direction.Value;
            TimeTravelPersistedData = sm.TriggeredTimeTravel ? sm.BodyList[0].BaseModel.BodyBlockType.TimeTravelPersistedData :  sm.HeadBlockType.TimeTravelPersistedData;
            Velocity = sm.TriggeredTimeTravel ? sm.Velocity.Value + SnakeAppConstants.SnakeVelocityDebuffModifier : sm.Velocity.Value;
            BatteringRamCount = sm.BatteringRamCount.Value;
            TimeTravelCount = sm.TimeTravelCount.Value;
            BodyList = new List<SnakeBodyPersistence>();

            if (Type == SnakeTypeEnum.AI) 
            {
                BlockPosition = (sm as SnakeAIModel).BlockPosition.Value;
            }

            if (sm.TriggeredTimeTravel)
            {
                for (int i = 0; i < sm.BodyList.Count - 1; i++)
                {
                    SnakeBodyPersistence bodyPart = new SnakeBodyPersistence
                    {
                        Position = sm.BodyList[i].SnakeBodyView.transform.position,
                        Target = sm.BodyList[i].SnakeBodyModel.Target.Value,
                        Velocity = sm.BodyList[i].SnakeBodyModel.Velocity.Value + SnakeAppConstants.SnakeVelocityDebuffModifier,
                        WaitUps = sm.BodyList[i].SnakeBodyModel.WaitUps.Value,
                        BlockType = sm.BodyList[i + 1].SnakeBodyModel.BodyBlockType.BlockType,
                        TimeTravelPersistedData = sm.BodyList[i + 1].SnakeBodyModel.BodyBlockType.TimeTravelPersistedData
                    };

                    BodyList.Add(bodyPart);
                }
                
            }
            else
            {
                foreach (var bodyPart in sm.BodyList)
                {
                    BodyList.Add(new SnakeBodyPersistence(bodyPart.SnakeBodyModel));
                }
            }
            
        }

    }
}