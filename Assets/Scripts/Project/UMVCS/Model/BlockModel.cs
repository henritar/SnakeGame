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

        [SerializeField]
        private Vector3 _position;

        public List<BlockConfigData> BlockTypeList { get => BlockTypeList; }
        public Vector3 Position { get => _position; }
        public BlockConfigData BlockType { get => _blockType; set => _blockType = value; }

        public void InitializeBlock(BlockView view)
        {
            var randIndex = Random.Range(0, _blockTypeList.Count - 1);
            BlockConfigData blockType = _blockTypeList[randIndex];

            _blockType = blockType;
            _position = view.transform.position;

            view.GetComponent<Renderer>().material = blockType.MaterialRef;
        }
    }
}