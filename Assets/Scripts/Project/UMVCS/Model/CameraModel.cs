using Architectures.UMVCS.Model;
using Project.Snake.UMVCS.Model;
using UnityEngine;

namespace Assets.Scripts.Project.UMVCS.Model
{
    public class CameraModel : BaseModel
    {
        public CameraConfigData CameraConfigData { get => ConfigData as CameraConfigData; set => ConfigData = value; }

        [SerializeField] private Camera _camera;
        Camera Camera { get => _camera; }

        public void InitCamera()
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }
            _camera.rect = new Rect(CameraConfigData.CameraViewPort, CameraConfigData.CameraOffset);
        }
    }
}