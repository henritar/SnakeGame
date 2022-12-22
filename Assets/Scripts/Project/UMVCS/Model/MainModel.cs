using Architectures.UMVCS.Model;
using Attributes;
using Project.Data.Types;
using Project.Snake.UMVCS.Controller;
using Project.Snake.UMVCS.View;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    public class MainModel : BaseModel
    {
        public MainConfigData MainConfigData { get => ConfigData as MainConfigData; }

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
        public List<SelectionSnakeMenuEnum> SnakeTypeSelected { get => _snakeTypeSelected; set => _snakeTypeSelected = value; }

        [SerializeField]
        private int _numberOfPlayers = 1;

        [SerializeField]
        private List<Transform> _mainParent = new List<Transform>();

        [SerializeField]
        private List<SelectionSnakeMenuEnum> _snakeTypeSelected = new List<SelectionSnakeMenuEnum>();

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
    }
}
