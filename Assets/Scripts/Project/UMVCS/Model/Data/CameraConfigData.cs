using Architectures.UMVCS.Model.Data;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    [CreateAssetMenu(fileName = "CameraConfigData",
        menuName = SnakeAppConstants.CreateAssetMenuPath + "CameraConfigData",
        order = SnakeAppConstants.CreateAssetMenuOrder)]
    public class CameraConfigData : BaseConfigData
    {
        [SerializeField] private Vector2 _cameraOffset = new Vector2(0,0);

        [SerializeField] private Vector2 _cameraViewPort = new Vector2(1,1);

        public Vector2 CameraOffset { get => _cameraOffset; }
        public Vector2 CameraViewPort { get => _cameraViewPort; }

        public void Init(Vector2 offset, Vector2 viewPort)
        {
            _cameraOffset = offset;
            _cameraViewPort = viewPort;
        }
    }
}