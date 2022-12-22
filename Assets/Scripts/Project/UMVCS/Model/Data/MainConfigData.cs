using Architectures.UMVCS.Model.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
	[CreateAssetMenu(fileName = "MainConfigData", 
		menuName = SnakeAppConstants.CreateAssetMenuPath + "MainConfigData", 
		order = SnakeAppConstants.CreateAssetMenuOrder)]

	public class MainConfigData : BaseConfigData
	{
        [SerializeField]
        private List<Vector3> _initialSnakePosition = new List<Vector3>();

		[SerializeField]
        private List<Vector2Int> _blockSpawnBounderiesX = new List<Vector2Int>();
		[SerializeField]
		private List<Vector2Int> _blockSpawnBounderiesY = new List<Vector2Int>();
		[SerializeField]
		private List<string> _cameraInputControllers = new List<string>();

        public List<Vector3> InitialSnakePosition { get => _initialSnakePosition;}
        public List<Vector2Int> BlockSpawnBounderiesX { get => _blockSpawnBounderiesX; }
        public List<Vector2Int> BlockSpawnBounderiesY { get => _blockSpawnBounderiesY; }
        public List<string> CameraInputControllers { get => _cameraInputControllers;  }
    }
}