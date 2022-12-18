using Architectures.UMVCS;
using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Data.Types;
using Interfaces;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class SnakeAIController : BaseController<SnakeAIModel, SnakeAIView, NullService>, ISnake
    {
        public SnakeAIView SnakeAIView { get => BaseView as SnakeAIView; }
        public SnakeAIModel SnakeAIModel { get => BaseModel as SnakeAIModel; }

        public Context IContext { get => Context; }
        public List<SnakeBodyController> BodyList { get => SnakeAIModel.BodyList; set => SnakeAIModel.BodyList = value; }
        public BlockConfigData HeadBlockType { get => SnakeAIModel.HeadBlockType; set => SnakeAIModel.HeadBlockType = value; }
        public ObservableFloat BodyVelocity { get => SnakeAIModel.Velocity; set => SnakeAIModel.Velocity.Value = value.Value; }

        private void Start()
        {
            SnakeAIModel.BodySize.Value = 0;

            Context.ModelLocator.AddModel(SnakeAIModel);
        }

        private void OnDestroy()
        {
            Context.ModelLocator.RemoveModel(SnakeAIModel);
        }
        public void ChangeSnakeVelocity(float modifier)
        {
            SnakeAIModel.Velocity.Value += modifier;
            foreach (var bodyPart in BodyList)
            {
                bodyPart.BaseModel.Velocity.Value = SnakeAIModel.Velocity.Value;
            }
        }

        public void ShuffleLocation(bool resetSize = false)
        {
            var boundariesX = SnakeAIModel.AIConfigData.BlockSpawnBounderiesX;
            var boundariesY = SnakeAIModel.AIConfigData.BlockSpawnBounderiesY;
            Vector3 position = new Vector3(Random.Range(boundariesX.x, boundariesX.y), Random.Range(boundariesY.x, boundariesY.y), 0);
            SnakeAIView.transform.position = position;
        }

        public void SetHeadBlockType(BlockConfigData newType)
        {
            HeadBlockType = newType;
            SnakeAIView.GetComponent<Renderer>().material = newType.MaterialRef;
        }

        public void AddBodyPart(SnakeBodyController bodyPart)
        {
            BodyList.Add(bodyPart);
            SnakeAIModel.BodySize.Value += 1;
        }
    }
}