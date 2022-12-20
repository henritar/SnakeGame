using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Data.Types;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using System;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class SnakeBodyController : BaseController<SnakeBodyModel, SnakeBodyView, NullService>
    {
        public SnakeBodyModel SnakeBodyModel { get => BaseModel as SnakeBodyModel; }
        public SnakeBodyView SnakeBodyView { get => BaseView as SnakeBodyView; }

        private void Start()
        {
            SnakeBodyModel.BodyCollider = SnakeBodyView.GetComponent<BoxCollider2D>();
            SnakeBodyModel.BodyCollider.enabled = false;
            SnakeBodyView.OnPlayerHitEvent.AddListener(SnakeBodyView_OnPlayerHit);
            SnakeBodyView.OnAIHitEvent.AddListener(SnakeBodyView_OnAIHit);
        }

        protected virtual void Update()
        {
            SnakeBodyView.MoveBodyPart(Vector3.MoveTowards(SnakeBodyView.transform.position, SnakeBodyModel.Target.Value, SnakeBodyModel.Velocity.Value * Time.deltaTime));
        }

        protected virtual void OnDestroy()
        {

            SnakeBodyModel.Snake?.SnakeModel.Velocity.OnChanged.RemoveListener(SnakeModel_OnVelocityChanged);
            SnakeBodyView.OnPlayerHitEvent.RemoveListener(SnakeBodyView_OnPlayerHit);
            SnakeBodyView.OnAIHitEvent.RemoveListener(SnakeBodyView_OnAIHit);
        }

        public void InitializeBodyPart(SnakeController snake)
        {
            SnakeBodyModel.Snake = snake;
            SnakeBodyModel.Velocity.Value = snake.SnakeModel.Velocity.Value;
            SnakeBodyModel.WaitUps.Value = snake.SnakeModel.BodyList.Count;
            SnakeBodyModel.Target.Value = SnakeBodyView.transform.position;

            snake.SnakeModel.Velocity.OnChanged.AddListener(SnakeModel_OnVelocityChanged);
        }

        private void SnakeModel_OnVelocityChanged(Observable obs)
        {
            ObservableFloat observable = obs as ObservableFloat;
            SnakeBodyModel.Velocity.Value = observable.Value;
        }

        private void SnakeBodyView_OnAIHit(SnakeAIController sc)
        {
            Context.CommandManager.InvokeCommand(new SpawnAISnakeCommand(SnakeBodyModel.Index, sc, true));
        }

        private void SnakeBodyView_OnPlayerHit(SnakePlayerController sc)
        {
            Context.CommandManager.InvokeCommand(new RestartApplicationCommand());
        }

        public void SetBodyBlockType(BlockConfigData blockType)
        {
            SnakeBodyModel.BodyBlockType = blockType;
            SnakeBodyView.GetComponent<Renderer>().material = blockType.MaterialRef;
        }

        public void SetTarget(Vector3 pos, SnakeController head)
        {
            if (SnakeBodyModel.WaitUps.Value > 0)
            {
                SnakeBodyModel.WaitUps.Value--;
                return;
            }
            if (SnakeBodyModel.WaitUps.Value == 0)
            {
                Context.CommandManager.InvokeCommand(new LoadBlockCommand());
                SnakeBodyModel.WaitUps.Value--;
                SnakeBodyModel.BodyCollider.enabled = true;
            }
            SnakeBodyModel.Target.Value = pos;
        }
    }
}