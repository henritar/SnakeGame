using Architectures.UMVCS.Model.Data;
using System;
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

        [SerializeField]
        private Material _materialRef;

        public BlockTypeEnum BlockType { get => _blockType; }
        public Material MaterialRef { get => _materialRef; }
    }
}