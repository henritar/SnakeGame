using Architectures.UMVCS;
using Assets.Scripts.Project.UMVCS.Controller.Commands;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using System.Collections;
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
            
            MainModel mainModel = Context.ModelLocator.GetModel<MainModel>();
            Context.CommandManager.InvokeCommand(new SnakeAIDestinationCommand(mainModel.BlockController.BlockModel.Position));

            Context.ModelLocator.AddModel(SnakeAIModel);
        }

        protected override void Update()
        {
            base.Update();

            if (SnakeAIView.transform.position == SnakeAIModel.Target.Value)
            {
                SetBodyTarget();
                SetTargetDirection();
                SnakeAIModel.Target.Value += SnakeAIModel.Direction.Value;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            RemoveListenersCallbacks();

            Context.ModelLocator.RemoveModel(SnakeAIModel);
        }

        protected override void AddListenersCallbacks()
        {
            base.AddListenersCallbacks();

            Context.CommandManager.AddCommandListener<SnakeAIDestinationCommand>(CommandManager_OnSnakeAIDestination);

        }

        protected override void RemoveListenersCallbacks()
        {
            base.RemoveListenersCallbacks();

            Context.CommandManager.RemoveCommandListener<SnakeAIDestinationCommand>(CommandManager_OnSnakeAIDestination);
        }

        public void ShuffleLocation(bool resetSize = false)
        {
            var boundariesX = SnakeAIModel.MainConfigData.BlockSpawnBounderiesX;
            var boundariesY = SnakeAIModel.MainConfigData.BlockSpawnBounderiesY;
            Vector3 position = new Vector3(Random.Range(boundariesX.x, boundariesX.y), Random.Range(boundariesY.x, boundariesY.y), 0);
            SnakeAIView.transform.position = position;
            SnakeAIModel.Target.Value= position;
            var bodyList = SnakeAIModel.BodyList;
            for (int i = 0; i < SnakeAIModel.BodyList.Count; i++)
            {
                bodyList[i].SnakeBodyView.transform.position = position;
                bodyList[i].SnakeBodyModel.Target.Value = position;
                bodyList[i].SnakeBodyModel.WaitUps.Value = i;
            }
        }

        private void CommandManager_OnSnakeAIDestination(SnakeAIDestinationCommand e)
        {
            SnakeAIModel.BlockPosition.Value = e.Destination;
        }

        private void SetTargetDirection()
        {
            var dirX = SnakeAIModel.BlockPosition.Value.x - SnakeAIView.transform.position.x;
            var dirY = SnakeAIModel.BlockPosition.Value.y - SnakeAIView.transform.position.y;

            bool xCloser = System.Math.Abs(dirX) < System.Math.Abs(dirY);

            
            if(xCloser)
            {
                SnakeAIModel.Direction.Value = dirX < 0 ? Vector3.left : dirX > 0 ? Vector3.right : dirY < 0 ? Vector3.down : Vector3.up;
            }
            else
            {
                SnakeAIModel.Direction.Value = dirY < 0 ? Vector3.down : dirY > 0 ? Vector3.up : dirX < 0 ? Vector3.left : Vector3.right;
            }
        }

        
    }
}