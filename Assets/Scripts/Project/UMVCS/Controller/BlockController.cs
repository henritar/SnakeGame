﻿using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using System.Diagnostics;

namespace Project.Snake.UMVCS.Controller
{
    public class BlockController : BaseController<BlockModel, BlockView, NullService>
    {
        public BlockView BlockView { get => BaseView as BlockView; }
        public BlockModel BlockModel { get => BaseModel as BlockModel;  }

        protected virtual void Start()
        {
            BlockModel.InitializeBlock(BlockView);

            Context.ModelLocator.AddModel(BlockModel);
        }
        
        protected virtual void OnDestroy()
        {
            Context.ModelLocator.RemoveModel(BlockModel);
        }

    }
}