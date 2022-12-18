using Architectures.UMVCS;
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
        public virtual SnakeView SnakeView { get => BaseView as SnakeView; }
        public virtual SnakeModel SnakeModel { get => BaseModel as SnakeModel; }

        protected virtual void Start()
        {
            CreateHeadBlockInstance();

            StartModelValues();

            AddListenersCallbacks();

            InitializeStateMachine();

        }

        protected virtual void OnDestroy()
        {
            RemoveListenersCallbacks();

            Context.ModelLocator.RemoveModel(SnakeModel);
        }

        protected virtual void Update()
        {
            SnakeModel.StateMachine.UpdateStates();

            SnakeView.MoveSnake(Vector3.MoveTowards(SnakeView.transform.position, SnakeModel.Target.Value, SnakeModel.Velocity.Value * Time.deltaTime));

            if (SnakeView.transform.position == SnakeModel.Target.Value)
            {
                SetBodyTarget();

                SnakeModel.Target.Value += SnakeModel.Direction.Value;

            }
        }

        protected virtual void InitializeStateMachine()
        {
            SnakeModel.InitializeStateMachine(this);
            SnakeModel.StateMachine.CurrentStateType = typeof(MovingState);
        }

        protected virtual void AddListenersCallbacks()
        {

            SnakeView.OnPickBlock.AddListener(SnakeModel_OnBlockPicked);
        }

        protected virtual void RemoveListenersCallbacks()
        {
            SnakeView.OnPickBlock.RemoveListener(SnakeModel_OnBlockPicked);
        }

        protected virtual void StartModelValues()
        {
            SnakeModel.Target.Value = SnakeView.transform.position;
            SnakeModel.Direction.Value = Vector3.up;
            SnakeModel.Velocity.Value = SnakeAppConstants.SnakeVelocity;
            SnakeModel.BodySize.Value = 0;
        }

        protected virtual void CreateHeadBlockInstance()
        {
            SnakeModel.HeadBlockType = ScriptableObject.CreateInstance<BlockConfigData>();
            SnakeModel.HeadBlockType.MaterialRef = SnakeView.GetComponent<Renderer>().material;
            SnakeModel.HeadBlockType.BlockType = BlockTypeEnum.Head;
        }

        protected virtual void SnakeModel_OnBlockPicked(BlockController block)
        {
            SnakeModel.StateMachine.CurrentStateType = typeof(PickingState);
            Context.CommandManager.InvokeCommand(new SpawnBlockCommand());
            Context.CommandManager.InvokeCommand(new SpawnAISnakeCommand());
            Context.CommandManager.InvokeCommand(new AddBodyPartCommand(this, block));
        }

        protected void SetBodyTarget()
        {
            var bodyList = SnakeModel.BodyList;
            if (bodyList.Count > 0)
            {
                bodyList[0].SetTarget(transform.position);

                for (int i = bodyList.Count - 1; i > 0; i--)
                {
                    Vector3 pos = new Vector3(Mathf.RoundToInt(bodyList[i - 1].transform.position.x), Mathf.RoundToInt(bodyList[i - 1].transform.position.y), 0);
                    bodyList[i].SetTarget(pos);
                }
            }
        }

        public void ChangeSnakeVelocity(float modifier)
        {
            SnakeModel.Velocity.Value += modifier;
            foreach (var bodyPart in SnakeModel.BodyList)
            {
                bodyPart.BaseModel.Velocity.Value = SnakeModel.Velocity.Value;
            }
        }

        public void SetHeadBlockType(BlockConfigData newType)
        {
            SnakeModel.HeadBlockType = newType;
            SnakeView.GetComponent<Renderer>().material = newType.MaterialRef;
        }

        public void AddBodyPart(SnakeBodyController bodyPart)
        {
            SnakeModel.BodyList.Add(bodyPart);
            SnakeModel.BodySize.Value += 1;
        }
    }

}
