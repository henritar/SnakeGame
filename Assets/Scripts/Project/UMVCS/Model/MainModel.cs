using Architectures.UMVCS.Model;
using Attributes;
using Project.Snake.UMVCS.Controller;
using Project.Snake.UMVCS.View;
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
        public Transform MainParent { get => _mainParent; }
        public SnakePlayerView SnakePlayerViewPrefab { get => _snakePlayerViewPrefab; }
        public SnakeBodyView SnakeBodyViewPrefab { get => _snakeBodyViewPrefab; }
        public BlockView BlockViewPrefab { get => _blockViewPrefab; }
        public SnakeAIView SnakeAIViewPrefab { get => _snakeAIViewPrefab; }

        [SerializeField]
        private Transform _mainParent = null;

        [SerializeField]
        private SnakePlayerView _snakePlayerViewPrefab = null;

        [SerializeField]
        private SnakeAIView _snakeAIViewPrefab = null;

        [SerializeField]
        private SnakeBodyView _snakeBodyViewPrefab = null;

        [SerializeField]
        private BlockView _blockViewPrefab = null;

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
        
    }
}
