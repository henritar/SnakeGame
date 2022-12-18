using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Data.Types;
using Interfaces;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class SnakeBodyController : BaseController<SnakeBodyModel, SnakeBodyView, NullService>
    {
        public SnakeBodyModel SnakeBodyModel { get => BaseModel as SnakeBodyModel; }
        public SnakeBodyView SnakeBodyView { get => BaseView as SnakeBodyView; }

        public void InitializeBodyPart(ISnake snake)
        {
            SnakeBodyModel.Snake = snake;
            SnakeBodyModel.Snake.BodyVelocity.OnChanged.AddListener(SnakeModel_OnVelocityChanged);
            SnakeBodyModel.Velocity.Value = snake.BodyVelocity.Value;
            SnakeBodyModel.WaitUps.Value = snake.BodyList.Count;
            SnakeBodyModel.Target.Value = SnakeBodyView.transform.position;
        }
        
        protected void Update()
        {
            SnakeBodyView.MoveBodyPart(Vector3.MoveTowards(SnakeBodyView.transform.position, SnakeBodyModel.Target.Value, SnakeBodyModel.Velocity.Value * Time.deltaTime));
        }

        private void SnakeModel_OnVelocityChanged(Observable obs)
        {
            ObservableFloat observable = obs as ObservableFloat;
            SnakeBodyModel.Velocity.Value = observable.Value;
        }

        public void SetBodyBlockType(BlockConfigData blockType)
        {
            SnakeBodyModel.BodyBlockType = blockType;
            SnakeBodyView.GetComponent<Renderer>().material = blockType.MaterialRef;
        }

        public void SetTarget(Vector3 pos)
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
            }
            SnakeBodyModel.Target.Value = pos;
        }
    }
}