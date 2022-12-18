using Architectures.UMVCS;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class SnakeAIController : SnakeController
    {
        public SnakeAIView SnakeAIView { get => SnakeView as SnakeAIView; }
        public SnakeAIModel SnakeAIModel { get => SnakeModel as SnakeAIModel; }

        protected override void Start()
        {
            base.Start();

            Context.ModelLocator.AddModel(SnakeAIModel);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Context.ModelLocator.RemoveModel(SnakeAIModel);
        }

        public void ShuffleLocation(bool resetSize = false)
        {
            var boundariesX = SnakeAIModel.MainConfigData.BlockSpawnBounderiesX;
            var boundariesY = SnakeAIModel.MainConfigData.BlockSpawnBounderiesY;
            Vector3 position = new Vector3(Random.Range(boundariesX.x, boundariesX.y), Random.Range(boundariesY.x, boundariesY.y), 0);
            SnakeAIView.transform.position = position;
        }
    }
}