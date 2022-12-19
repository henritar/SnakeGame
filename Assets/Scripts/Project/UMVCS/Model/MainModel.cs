using Architectures.UMVCS.Model;
using Attributes;
using Project.Snake.UMVCS.Controller;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    public class MainModel : BaseModel
    {
        public MainConfigData MainConfigData { get => ConfigData as MainConfigData; }
        public SnakePlayerController SnakePlayerController { get => _snakePlayerController; set => _snakePlayerController = value; }
        public SnakeAIController SnakeAIController { get => _snakeAIController; set => _snakeAIController = value; }
        public SnakeBodyController SnakeBodyController { get => _snakeBodyController; set => _snakeBodyController = value; }
        public BlockController BlockController { get => _blockController; set => _blockController = value; }

        [ReadOnly]
        [SerializeField]
        private SnakePlayerController _snakePlayerController = null;

        [ReadOnly]
        [SerializeField]
        private SnakeAIController _snakeAIController = null;

        [ReadOnly]
        [SerializeField]
        private SnakeBodyController _snakeBodyController = null;

        [ReadOnly]
        [SerializeField]
        private BlockController _blockController = null;
        

        public override void Initialize()
        {
            if (!IsInitialized)
            {

            }
            base.Initialize();
        }
    }
}
