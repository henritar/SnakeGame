using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class BlockController : BaseController<BlockModel, BlockView, NullService>
    {
        public BlockView BlockView { get => BaseView as BlockView; }
        public BlockModel BlockModel { get => BaseModel as BlockModel;  }

        protected virtual void Start()
        {
            BlockModel.InitializeBlock(BlockView);

        }

        public void ShuffleLocation()
        {
            int index = BlockModel.Index;
            MainModel mainModel = Context.ModelLocator.GetModel<MainModel>();
            var boundariesX = mainModel.MainConfigData.BlockSpawnBounderiesX[index];
            var boundariesY = mainModel.MainConfigData.BlockSpawnBounderiesY[index];
            Vector3 position = new Vector3(Random.Range(boundariesX.x, boundariesX.y), Random.Range(boundariesY.x, boundariesY.y), 0);
            BlockView.transform.position = position;
            BlockModel.InitializeBlock(BlockView);
        }
    }
}