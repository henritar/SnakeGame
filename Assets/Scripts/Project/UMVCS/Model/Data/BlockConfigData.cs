using Architectures.UMVCS.Model.Data;
using Project.Snake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    [System.Serializable]
    public enum BlockTypeEnum
    {
        EnginePower = 0,
        BatteringRam = 1,
        TimeTravel = 2
    }

    [CreateAssetMenu(fileName = "BlockConfigData",
        menuName = SnakeAppConstants.CreateAssetMenuPath + "BlockConfigData",
        order = SnakeAppConstants.CreateAssetMenuOrder)]

    
    public class BlockConfigData : BaseConfigData
    {
        [SerializeField]
        private BlockTypeEnum _blockType;

        public BlockTypeEnum BlockType { get => _blockType; }
    }
}