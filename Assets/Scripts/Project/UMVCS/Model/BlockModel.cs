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
        private BlockConfigData _blockType = null;

        public List<BlockConfigData> BlockTypeList { get => BlockTypeList; }
        public BlockConfigData BlockType { get => _blockType; }

        public void InitializeBlock(BlockView view)
        {
            var randIndex = Random.Range(0, _blockTypeList.Count - 1);
            BlockConfigData blockType = _blockTypeList[randIndex];

            _blockType = blockType;

            view.GetComponent<Renderer>().material = blockType.MaterialRef;
        }
    }
}