using Architectures.UMVCS.Model;
using Attributes;
using Project.Snake.UMVCS.Controller;
using Project.Snake.UMVCS.View;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    public class MainModel : BaseModel
    {
        public MainConfigData MainConfigData { get => ConfigData as MainConfigData; }

        public List<Vector3> InitialSnakePosition { get => _initialSnakePosition; set => _initialSnakePosition = value; }
        public List<Vector2Int> BlockSpawnBounderiesX { get => _blockSpawnBounderiesX; set => _blockSpawnBounderiesX = value; }
        public List<Vector2Int> BlockSpawnBounderiesY { get => _blockSpawnBounderiesY; set => _blockSpawnBounderiesY = value;  }

        public List<SnakePlayerController> SnakePlayerController { get => _snakePlayerController; set => _snakePlayerController = value; }
        public List<SnakeAIController> SnakeAIController { get => _snakeAIController; set => _snakeAIController = value; }
        public List<SnakeBodyController> SnakeBodyController { get => _snakeBodyController; set => _snakeBodyController = value; }
        public List<BlockController> BlockController { get => _blockController; set => _blockController = value; }
        public List<CameraController> CameraController { get => _cameraController; set => _cameraController = value; }
        public int NumberOfPlayers { get => _numberOfPlayers; set => _numberOfPlayers = value; }
        public List<Transform> MainParent { get => _mainParent; set => _mainParent = value; }
        public CameraView CameraViewPrefab { get => _cameraViewPrefab; }
        public SnakePlayerView SnakePlayerViewPrefab { get => _snakePlayerViewPrefab; }
        public SnakeBodyView SnakeBodyViewPrefab { get => _snakeBodyViewPrefab; }
        public BlockView BlockViewPrefab { get => _blockViewPrefab; }
        public SnakeAIView SnakeAIViewPrefab { get => _snakeAIViewPrefab; }

        [SerializeField]
        private int _numberOfPlayers = 1;

        [SerializeField]
        private List<Transform> _mainParent = new List<Transform>();

        [SerializeField]
        private CameraView _cameraViewPrefab = null;

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
        private List<SnakePlayerController> _snakePlayerController = null;

        [ReadOnly]
        [SerializeField]
        private List<SnakeAIController> _snakeAIController = null;

        [ReadOnly]
        [SerializeField]
        private List<SnakeBodyController> _snakeBodyController = null;

        [ReadOnly]
        [SerializeField]
        private List<BlockController> _blockController = null;

        [ReadOnly]
        [SerializeField]
        private List<CameraController> _cameraController = null;


        [SerializeField]
        private List<Vector3> _initialSnakePosition = new List<Vector3>();

        [SerializeField]
        private List<Vector2Int> _blockSpawnBounderiesX = new List<Vector2Int>();
        [SerializeField]
        private List<Vector2Int> _blockSpawnBounderiesY = new List<Vector2Int>();
    }
}
