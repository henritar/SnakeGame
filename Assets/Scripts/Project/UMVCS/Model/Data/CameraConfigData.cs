using Architectures.UMVCS.Model.Data;
using System.Collections.Generic;
using TMPro;
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

        [SerializeField] private List<string> _inputKeysList;

        

        public Vector2 CameraOffset { get => _cameraOffset; }
        public Vector2 CameraViewPort { get => _cameraViewPort; }
        public List<string> InputKeys { get => _inputKeysList; set => _inputKeysList = value; }
        

        public void Init(Vector2 offset, Vector2 viewPort)
        {

            _cameraOffset = offset;
            _cameraViewPort = viewPort;
            
        }
    }
}