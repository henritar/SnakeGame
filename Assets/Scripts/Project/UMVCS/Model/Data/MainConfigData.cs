using Architectures.UMVCS.Model.Data;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
	[CreateAssetMenu(fileName = "MainConfigData", 
		menuName = SnakeAppConstants.CreateAssetMenuPath + "MainConfigData", 
		order = SnakeAppConstants.CreateAssetMenuOrder)]

	public class MainConfigData : BaseConfigData
	{
        [SerializeField]
        private Vector3 _initialSnakePosition = new Vector3(0, 0, 0);

		[SerializeField]
        private Vector2Int _blockSpawnBounderiesX = new Vector2Int(0, 0);
		[SerializeField]
		private Vector2Int _blockSpawnBounderiesY = new Vector2Int(0, 0);

        public Vector3 InitialSnakePosition { get => _initialSnakePosition;}
        public Vector2Int BlockSpawnBounderiesX { get => _blockSpawnBounderiesX; }
        public Vector2Int BlockSpawnBounderiesY { get => _blockSpawnBounderiesY; }
    }
}