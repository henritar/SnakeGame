using Architectures.UMVCS.Model;
using TMPro;
using UnityEngine;


namespace Project.Snake.UMVCS.Model
{
    public class CameraModel : BaseModel
    {
        public CameraConfigData CameraConfigData { get => ConfigData as CameraConfigData; set => ConfigData = value; }

        [SerializeField] private Camera _camera;
        [SerializeField] private TextMeshProUGUI _playerName;
        [SerializeField] private TextMeshProUGUI _inputKey;

        public TextMeshProUGUI InputKey { get => _inputKey; set => _inputKey = value; }
        public TextMeshProUGUI PlayerName { get => _playerName; set => _playerName = value; }
        Camera Camera { get => _camera; }

        public void InitCamera(int index, CameraConfigData cameraConfig)
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }
            _camera.rect = new Rect(CameraConfigData.CameraViewPort, CameraConfigData.CameraOffset);
        }
    }
}