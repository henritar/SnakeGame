using Project.Snake.UMVCS.Model;
using System;
using UnityEngine;

namespace Project.Data.Types
{
    [Serializable]
    public class BlockPersistence
    {
        public BlockTypeEnum BlockType;
        public Vector3 Position;

        public BlockPersistence(BlockModel block)
        {
            BlockType = block.BlockType.BlockType;
            Position = block.Position;
        }
    }
}