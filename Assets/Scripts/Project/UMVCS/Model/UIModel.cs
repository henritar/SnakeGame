using Architectures.UMVCS.Model;
using Assets.Scripts.Project.UMVCS.View;
using Project.Data.Types;
using Project.Snake.UMVCS.View;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Snake.UMVCS.Model
{
    public class UIModel : BaseModel
    {
        [System.Serializable]
        public struct PlayerSnakeUI
        {
            public TextMeshProUGUI playerTag;
            public TextMeshProUGUI leftKey;
            public TextMeshProUGUI rightKey;
            public int snakeSpriteIndex;
            public NewPlayerUIModel uiRef;
        }

        [SerializeField] 
        private Camera _UICamera;
        [SerializeField]
        private List<PlayerSnakeUI> _playerSnakeUIList;
        [SerializeField]
        private int _numberOfPlayers;
        [SerializeField]
        private NewPlayerUIView _newPlayerUIPrefab;
        [SerializeField]
        private HashSet<KeyCode> _keyCodes= new HashSet<KeyCode>();
        [SerializeField]
        private Image _playerPanel = null;
        [SerializeField]
        private Canvas _startMenuCanvas = null;
        [SerializeField]
        private HUDCameraModel _HUDCameraPrefab = null;
        [SerializeField]
        private List<SelectionSnakeMenuEnum> _playersSnake = new List<SelectionSnakeMenuEnum>();
        [SerializeField]
        private List<Texture2D> _snakesSpritesList = new List<Texture2D>();

        public List<PlayerSnakeUI> PlayerSnakeUIList { get => _playerSnakeUIList; set => _playerSnakeUIList = value; }
        public Camera UICamera { get => _UICamera; set => _UICamera = value; }
        public int NumberOfPlayers { get => _numberOfPlayers; set => _numberOfPlayers = value; }
        
        public HashSet<KeyCode> KeyCodes { get => _keyCodes; set => _keyCodes = value; }
        public Image PlayerPanel { get => _playerPanel; set => _playerPanel = value; }

        public NewPlayerUIView NewPlayerUIPrefab { get => _newPlayerUIPrefab; }
        public Canvas StartMenuCanvas { get => _startMenuCanvas; }
        public HUDCameraModel HUDCameraPrefab { get => _HUDCameraPrefab; set => _HUDCameraPrefab = value; }
        public List<SelectionSnakeMenuEnum> PlayersSnake { get => _playersSnake; set => _playersSnake = value; }
        public List<Texture2D> SnakesSpritesList { get => _snakesSpritesList; set => _snakesSpritesList = value; }
    }
}