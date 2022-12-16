using Architectures.UMVCS.View;
using UnityEngine;

namespace Project.Snake.UMVCS.View
{
    public class MainView : BaseView
    {
        [SerializeField]
        private Transform _mainParent = null;

        [SerializeField]
        private SnakeView _snakeViewPrefab = null;

        [SerializeField]
        private SnakeBodyView _snakeBodyViewPrefab = null;

        [SerializeField]
        private BlockView _blockViewPrefab = null;

        public Transform MainParent { get => _mainParent; }
        public SnakeView SnakeViewPrefab { get => _snakeViewPrefab; }
        public SnakeBodyView SnakeBodyViewPrefab { get => _snakeBodyViewPrefab; }
        public BlockView BlockViewPrefab { get => _blockViewPrefab; }
    }
}
