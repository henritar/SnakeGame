using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Assets.Scripts.Project.UMVCS.Controller.Commands;
using Interfaces;
using Project.Data.Types;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class SnakeController : BaseController<SnakeModel, SnakeView, NullService>, ISnake
    {
        public SnakeView SnakeView { get => BaseView as SnakeView; }
        public SnakeModel SnakeModel { get => BaseModel as SnakeModel; }

        public List<SnakeBodyController> BodyList { get => _bodyList; set => _bodyList = value; }
        public BlockConfigData HeadBlockType { get => _headBlockType; set => _headBlockType = value; }
        public float BodyVelocity { get => SnakeModel.Velocity.Value; set => SnakeModel.Velocity.Value = value; }

        [SerializeField] private List<SnakeBodyController> _bodyList;

        [SerializeField] private BlockConfigData _headBlockType;

        private void Start()
        {
            HeadBlockType = new BlockConfigData(BlockTypeEnum.Head, SnakeView.GetComponent<Renderer>().material);

            SnakeModel.Target.Value = transform.position;
            SnakeModel.Direction.Value = Vector3.up;
            SnakeModel.Velocity.Value = SnakeAppConstants.SnakeVelocity;
            SnakeModel.BodySize.Value = 0;

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
            foreach (var bodyPart in BodyList)
            {
                bodyPart.BaseModel.Velocity.Value = SnakeModel.Velocity.Value;
            }
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
            Context.CommandManager.InvokeCommand(new SpawnAISnakeCommand());
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
            if (BodyList.Count > 0)
            {
                BodyList[0].SetTarget(transform.position);

                for (int i = BodyList.Count - 1; i > 0; i--)
                {
                    Vector3 pos = new Vector3(Mathf.RoundToInt(BodyList[i - 1].transform.position.x), Mathf.RoundToInt(BodyList[i - 1].transform.position.y), 0);
                    BodyList[i].SetTarget(pos);
                }
            }
        }

        public void SetHeadBlockType(BlockConfigData newType)
        {
            HeadBlockType = newType;
            SnakeView.GetComponent<Renderer>().material = newType.MaterialRef;
        }

        public void AddBodyPart(SnakeBodyController bodyPart)
        {
            BodyList.Add(bodyPart);
            SnakeModel.BodySize.Value += 1;
        }
    }

}
