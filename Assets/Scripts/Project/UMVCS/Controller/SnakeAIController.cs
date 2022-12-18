﻿using Architectures.UMVCS;
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
            
            AddListenersCallbacks();

            SnakeAIModel.StartPosition.Value = SnakeView.transform.position;

            MainModel mainModel = Context.ModelLocator.GetModel<MainModel>();
            Context.CommandManager.InvokeCommand(new SnakeAIDestinationCommand(mainModel.BlockController.BlockModel.Position));

            Context.ModelLocator.AddModel(SnakeAIModel);
        }

        protected override void Update()
        {
            base.Update();
            SetBodyTarget();
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
            SnakeAIModel.StartPosition.Value = position;
            SnakeAIView.transform.position = position;
        }

        private void CommandManager_OnSnakeAIDestination(SnakeAIDestinationCommand e)
        {
            StartCoroutine(MovementCoroutine(e.Destination));
        }

        private IEnumerator MovementCoroutine(Vector3 position)
        {
            bool xCloser = System.Math.Abs(position.x - SnakeAIView.transform.position.x) < System.Math.Abs(position.y - position.y - SnakeAIView.transform.position.y);
            bool yPreviousPosCloser = System.Math.Abs(position.x - SnakeAIModel.StartPosition.PreviousValue.x) > System.Math.Abs(position.y - SnakeAIModel.StartPosition.PreviousValue.y);

            if (xCloser && yPreviousPosCloser)
            {
                SnakeAIModel.Target.Value = new Vector3(position.x, SnakeAIView.transform.position.y, SnakeAIView.transform.position.z);
                yield return new WaitWhile(() => SnakeAIView.transform.position.x != position.x);
                SnakeAIModel.Target.Value = new Vector3(SnakeAIView.transform.position.x, position.y, SnakeAIView.transform.position.z);
            }
            else
            {
                SnakeAIModel.Target.Value = new Vector3(SnakeAIView.transform.position.x, position.y, SnakeAIView.transform.position.z);
                yield return new WaitWhile(() => SnakeAIView.transform.position.y != position.y);
                SnakeAIModel.Target.Value = new Vector3(position.x, SnakeAIView.transform.position.y, SnakeAIView.transform.position.z);
            }

            SnakeAIModel.StartPosition.Value = position;
        }
    }
}