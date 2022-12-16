using Architectures.UMVCS.Model;
using Attributes;
using Project.Snake.UMVCS.View;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    public class MainModel : BaseModel
    {
        public MainConfigData MainConfigData { get => ConfigData as MainConfigData; }
        public SnakeView SnakeView { get => _snakeView; set => _snakeView = value; }
        public SnakeBodyView SnakeBodyView { get => _snakeBodyView; }
        public BlockView BlockView { get => _blockView; set => _blockView = value; }

        [ReadOnly]
        [SerializeField]
        private SnakeView _snakeView = null;

        [ReadOnly]
        [SerializeField]
        private SnakeBodyView _snakeBodyView = null;

        [ReadOnly]
        [SerializeField]
        private BlockView _blockView = null;
        

        public override void Initialize()
        {
            if (!IsInitialized)
            {

            }
            base.Initialize();
        }
    }
}
