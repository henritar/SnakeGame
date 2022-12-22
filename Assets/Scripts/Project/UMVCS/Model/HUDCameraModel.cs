using Architectures.UMVCS.Model;
using TMPro;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    public class HUDCameraModel : BaseModel
    {
        [SerializeField]
        private TextMeshProUGUI _playerName;
        [SerializeField]
        private TextMeshProUGUI _InputKey;

        public TextMeshProUGUI PlayerName { get => _playerName; set => _playerName = value; }
        public TextMeshProUGUI InputKey { get => _InputKey; set => _InputKey = value; }
    }
}