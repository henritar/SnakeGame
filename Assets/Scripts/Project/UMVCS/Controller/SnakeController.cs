using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Assets.Scripts.Project.UMVCS.Controller.Commands;
using Project.Data.Types;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class SnakeController : BaseController<SnakeModel, SnakeView, NullService>
    {
        public SnakeView SnakeView { get => BaseView as SnakeView; }
        public SnakeModel SnakeModel { get => BaseModel as SnakeModel; }

        [SerializeField] private Vector3 target;
        [SerializeField] private Vector3 newDir;
        [SerializeField] private float velocity;

        [SerializeField] private List<Transform> bodyList;

        private void Start()
        {
            target = transform.position;
            newDir = Vector3.up;
            velocity = SnakeAppConstants.SnakeVelocity;

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

            SnakeView.MoveSnake(Vector3.MoveTowards(SnakeView.transform.position, target, velocity * Time.deltaTime));

            if (SnakeView.transform.position == target)
            {
                target += newDir;
            }
        }

        public void ChangeSnakeVelocity(float modifier)
        {
            velocity += modifier;
        }
 
        private void CommandManager_OnChangeSnakeDirection(ChangeSnakeDirectionCommand e)
        {
            var dir = e.Direction;
            if (dir.x != 0) 
            {
                newDir = Vector3.right * dir.x;
            }
            if (dir.y != 0)
            {
                newDir = Vector3.up * dir.y;
            }
        }

        private void SnakeModel_OnBlockPicked()
        {
            SnakeModel.StateMachine.CurrentStateType = typeof(PickingState);
            Context.CommandManager.InvokeCommand(new SpawnBlockCommand());
        }
    }

}
