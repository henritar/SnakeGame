using Architectures.UMVCS.Model.Data;
using Project.Snake.UMVCS.Controller;
using System;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{

    [System.Serializable]
    public enum BlockTypeEnum
    {
        EnginePower = 0,
        BatteringRam = 1,
        TimeTravel = 2,
        Head = 3
    }

    [CreateAssetMenu(fileName = "BlockType",
        menuName = SnakeAppConstants.CreateAssetMenuPath + "BlockType",
        order = SnakeAppConstants.CreateAssetMenuOrder)]

    
    public class BlockConfigData : BaseConfigData
    {
        [SerializeField]
        private BlockTypeEnum _blockType;

        [SerializeField]
        private Material _materialRef;

        public BlockTypeEnum BlockType { get => _blockType; set => _blockType = value; }
        public Material MaterialRef { get => _materialRef; set => _materialRef = value; }


        public void ApplyPowerUp(SnakeController snakeController)
        {
            switch (_blockType)
            {
                case BlockTypeEnum.EnginePower:

                    snakeController.ChangeSnakeVelocity(SnakeAppConstants.SnakeVelocityEnginePowerModifier);
                    break;
                case BlockTypeEnum.BatteringRam:

                    break;

                case BlockTypeEnum.TimeTravel:
                    break;

                default: break;
            }
        }
    }
}