using Architectures.UMVCS.Model;
using System.Collections.Generic;
using UnityEngine;
using Project.Snake.UMVCS.View;

namespace Project.Snake.UMVCS.Model
{
    public class BlockModel : BaseModel
    {
        [SerializeField]
        private List<BlockConfigData> _blockTypeList = null;

        [SerializeField]
        private BlockTypeEnum _blockType;

        public List<BlockConfigData> BlockTypeList { get => BlockTypeList; }
        public BlockTypeEnum BlockType { get => _blockType; }

        public void InitializeBlock(BlockView view)
        {
            var randIndex = Random.Range(0, _blockTypeList.Count);
            BlockConfigData blockType = _blockTypeList[randIndex];

            _blockType = blockType.BlockType;

            view.GetComponent<Renderer>().material = blockType.MaterialRef;
        }
    }
}