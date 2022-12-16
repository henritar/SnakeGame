using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Assets.Scripts.Project.UMVCS.Controller.Commands;
using Project.Data.Types;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class SnakeController : BaseController<SnakeModel, SnakeView, NullService>
    {
        public SnakeView SnakeView { get => BaseView as SnakeView; }
        public SnakeModel SnakeModel { get => BaseModel as SnakeModel; }

        private void Start()
        {
            SnakeModel.Target.Value = transform.position;
            SnakeModel.Direction.Value = Vector3.up;
            SnakeModel.Velocity.Value = SnakeAppConstants.SnakeVelocity;

            Context.CommandManager.AddCommandListener<ChangeSnakeDirectionCommand>(CommandManager_OnChangeSnakeDirection);

            SnakeView.OnPickBlock.AddListener(SnakeModel_OnBlockPicked);

            SnakeModel.InitializeStateMachine(this);
            SnakeModel.StateMachine.CurrentStateType = typeof(MovingState);

            Context.ModelLocator.AddModel(SnakeModel);
        }

        private void OnDestroy()
        {
            Context.CommandManager.RemoveCommandListener<ChangeSnakeDirectionCommand>(CommandManager_OnChangeSnakeDirection);
            SnakeView.OnPickBlock.RemoveListener(SnakeModel_OnBlockPicked);
            Context.ModelLocator.RemoveModel(SnakeModel);
        }

        private void Update()
        {
            SnakeModel.StateMachine.UpdateStates();

            SnakeView.MoveSnake(Vector3.MoveTowards(SnakeView.transform.position, SnakeModel.Target.Value, SnakeModel.Velocity.Value * Time.deltaTime));

            if (SnakeView.transform.position == SnakeModel.Target.Value)
            {
                SetBodyTarget();
                
                SnakeModel.Target.Value += SnakeModel.Direction.Value;

            }
        }

        public void ChangeSnakeVelocity(float modifier)
        {
            SnakeModel.Velocity.Value += modifier;
        }
 
        private void CommandManager_OnChangeSnakeDirection(ChangeSnakeDirectionCommand e)
        {
            var dir = e.Direction;
            if (dir.x != 0) 
            {
                ValidateDirectionChange(Vector3.right * dir.x);
            }
            if (dir.y != 0)
            {
                ValidateDirectionChange(Vector3.up * dir.y);
            }
        }

        private void SnakeModel_OnBlockPicked(BlockController block)
        {
            SnakeModel.StateMachine.CurrentStateType = typeof(PickingState);
            Context.CommandManager.InvokeCommand(new SpawnBlockCommand());
            Context.CommandManager.InvokeCommand(new AddBodyPartCommand(this, block));
        }

        private void ValidateDirectionChange(Vector3 newDir)
        {

            if (SnakeModel.Direction.Value.x == -newDir.x || SnakeModel.Direction.Value.y == -newDir.y)
            {
                return;
            }
            SnakeModel.Direction.Value = newDir;
        }

        private void SetBodyTarget()
        {
            if (SnakeModel.BodyList.Count > 0)
            {
                SnakeModel.BodyList[0].SetTarget(transform.position);

                for (int i = SnakeModel.BodyList.Count - 1; i > 0; i--)
                {
                    Vector3 pos = new Vector3(Mathf.RoundToInt(SnakeModel.BodyList[i - 1].transform.position.x), Mathf.RoundToInt(SnakeModel.BodyList[i - 1].transform.position.y), 0);
                    SnakeModel.BodyList[i].SetTarget(pos);
                }
            }
        }
    }

}
