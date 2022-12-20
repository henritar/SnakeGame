using Architectures.UMVCS;
using Assets.Scripts.Project.UMVCS.Controller.Commands;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using System.Collections.Generic;
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

        }

        protected override void Update()
        {
            base.Update();

            if (SnakeAIView.transform.position == SnakeAIModel.Target.Value)
            {
                SetBodyTarget();
                SetTargetDirection();
                SnakeAIModel.Target.Value += SnakeAIModel.Direction.Value;
                LookAtTarget();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            RemoveListenersCallbacks();

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
            
            if (resetSize)
            {
                for (int i = 0; i < SnakeAIModel.BodyList.Count; i++)
                {
                    Destroy(SnakeAIModel.BodyList[i].SnakeBodyView.gameObject);
                }
                SnakeAIModel.BodyList = new List<SnakeBodyController>();
                SnakeAIModel.BodySize.Value = 0;
                SnakeAIModel.Velocity.Value = SnakeAppConstants.SnakeVelocity;
                SetHeadBlockType(BlockConfigData.CreateNewBlockType(BlockTypeEnum.Head));
            }
            else
            {
                var bodyList = SnakeAIModel.BodyList;
                for (int i = 0; i < SnakeAIModel.BodyList.Count; i++)
                {
                    bodyList[i].SnakeBodyView.transform.position = position;
                    bodyList[i].SnakeBodyModel.Target.Value = position;
                    bodyList[i].SnakeBodyModel.WaitUps.Value = i;
                }
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

            Vector3 nextTarget = SnakeAIModel.Direction.Value + SnakeAIModel.Target.Value;
            if (nextTarget == SnakeAIModel.Target.PreviousValue)
            {
                SnakeAIModel.Direction.Value = xCloser ? RotateVectorRight(SnakeAIModel.Direction.Value,90) : RotateVectorUp(SnakeAIModel.Direction.Value, 90);
            }
        }

        private Vector3 RotateVectorUp(Vector3 start, float angle)
        {
            start.Normalize();

            Vector3 axis = Vector3.Cross(start, Vector3.up);

            if (axis == Vector3.zero) axis = Vector3.forward;

            return Quaternion.AngleAxis(angle, axis) * start;
        }

        private Vector3 RotateVectorRight(Vector3 start, float angle)
        {
            start.Normalize();

            Vector3 axis = Vector3.Cross(start, Vector3.right);

            if (axis == Vector3.zero) axis = Vector3.forward;

            return Quaternion.AngleAxis(angle, axis) * start;
        }


    }
}