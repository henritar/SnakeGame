using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
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
        public List<SnakeBodyController> BodyList { get => _bodyList; set => _bodyList = value; }
        public BlockConfigData HeadBlockType { get => _headBlockType; set => _headBlockType = value; }
        public float BodyVelocity { get => SnakeAIModel.Velocity.Value; set => SnakeAIModel.Velocity.Value = value; }

        [SerializeField] private BlockConfigData _headBlockType;

        [SerializeField] private List<SnakeBodyController> _bodyList;

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