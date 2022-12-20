using Architectures.UMVCS.Model;
using Project.Snake.UMVCS.Model;
using UnityEngine;

namespace Assets.Scripts.Project.UMVCS.Model
{
    public class CameraModel : BaseModel
    {
        public CameraConfigData CameraConfigData { get => ConfigData as CameraConfigData; }

        [SerializeField] private Camera _camera;
        Camera Camera { get => _camera; }

        public void InitCamera(CameraConfigData cameraConfig = null)
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }
            _camera.rect = new Rect(CameraConfigData.CameraOffset, CameraConfigData.CameraViewPort);
        }
    }
}