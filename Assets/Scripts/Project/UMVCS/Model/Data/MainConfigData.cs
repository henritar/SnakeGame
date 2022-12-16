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
        private Vector2 _blockSpawnBounderiesX = new Vector2(0, 0);
		[SerializeField]
		private Vector2 _blockSpawnBounderiesY = new Vector2(0, 0);

        public Vector3 InitialSnakePosition { get => _initialSnakePosition;}
        public Vector2 BlockSpawnBounderiesX { get => _blockSpawnBounderiesX; }
        public Vector2 BlockSpawnBounderiesY { get => _blockSpawnBounderiesY; }
    }
}