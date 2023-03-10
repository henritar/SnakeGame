using Architectures.UMVCS;
using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Assets.Scripts.Project.UMVCS.Controller.Commands;
using Assets.Utils.Runtime.Managers;
using Project.Data.Types;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using System.Collections;
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

        }

        protected virtual void Update()
        {
            SnakeModel.StateMachine.UpdateStates();

            SnakeView.MoveSnake(Vector3.MoveTowards(SnakeView.transform.position, SnakeModel.Target.Value, SnakeModel.Velocity.Value * Time.deltaTime));

        }

        protected virtual void InitializeStateMachine()
        {
            SnakeModel.InitializeStateMachine(this);
            SnakeModel.StateMachine.CurrentStateType = typeof(MovingState);
        }

        protected virtual void AddListenersCallbacks()
        {
            SnakeView.OnSnakeHitBounds.AddListener(SnakeView_OnSnakeHitbounds);
            SnakeView.OnPickBlock.AddListener(SnakeView_OnBlockPicked);
        }

        protected virtual void RemoveListenersCallbacks()
        {
            SnakeView.OnSnakeHitBounds.RemoveListener(SnakeView_OnSnakeHitbounds);
            SnakeView.OnPickBlock.RemoveListener(SnakeView_OnBlockPicked);
        }

        protected virtual void StartModelValues()
        {
            SnakeModel.Target.Value = SnakeView.transform.position;
            SnakeModel.Direction.Value = Vector3.up;
            SnakeModel.Velocity.Value = SnakeAppConstants.SnakeVelocity;
        }

        protected virtual void CreateHeadBlockInstance()
        {
            SnakeModel.HeadBlockType = ScriptableObject.CreateInstance<BlockConfigData>();
            SnakeModel.HeadBlockType.MaterialRef = SnakeView.GetComponent<Renderer>().material;
            SnakeModel.HeadBlockType.BlockType = BlockTypeEnum.Head;
        }

        protected virtual void SnakeView_OnBlockPicked(BlockController block)
        {
            Context.CommandManager.InvokeCommand(new SpawnBlockCommand(SnakeModel.Index));
            Context.CommandManager.InvokeCommand(new AddBodyPartCommand(this, block));
            SnakeModel.StateMachine.CurrentStateType = typeof(PickingState);
        }
        private void SnakeView_OnSnakeHitbounds()
        {
            if (this is SnakePlayerController)
                Context.CommandManager.InvokeCommand(new KillPlayerSnakeCommand(this as SnakePlayerController));
            else
                Context.CommandManager.InvokeCommand(new SpawnAISnakeCommand(SnakeModel.Index));
        }

        protected void SetBodyTarget()
        {
            var bodyList = SnakeModel.BodyList;
            if (bodyList.Count > 0)
            {
                bodyList[0].SetTarget(new Vector3(SnakeView.transform.position.x, SnakeView.transform.position.y));

                for (int i = bodyList.Count - 1; i > 0; i--)
                {
                    Vector3 pos = new Vector3(bodyList[i - 1].transform.position.x, bodyList[i - 1].transform.position.y, 0);
                    bodyList[i].SetTarget(pos);
                }
            }
        }

        protected void LookAtTarget()
        {
            SnakeView.transform.rotation = SnakeModel.Direction.Value == Vector3.up ? Quaternion.Euler(0, 0, 0) : SnakeModel.Direction.Value == Vector3.down ? Quaternion.Euler(0, 0, 180)
                : SnakeModel.Direction.Value == Vector3.right ? Quaternion.Euler(0, 0, -90) : Quaternion.Euler(0, 0, 90);
        }

        public void KillSnake()
        {
            for (int i = 0; i < SnakeModel.BodyList.Count; i++)
            {
                Destroy(SnakeModel.BodyList[i].transform.parent.gameObject);
            }
            Destroy(SnakeView.gameObject);
            MainModel mainModel = Context.ModelLocator.GetModel<MainModel>();
            mainModel.SnakePlayerController.RemoveAt(SnakeModel.Index);
        }

        public void RestoreSnakeVelocity()
        {
            int enginePowerBlocks = 0;
            foreach (var bodyPart in SnakeModel.BodyList)
            {
                if (bodyPart.SnakeBodyModel.BodyBlockType.BlockType == BlockTypeEnum.EnginePower)
                {
                    enginePowerBlocks++;
                }
            }
            SnakeModel.Velocity.Value = SnakeAppConstants.SnakeVelocity + enginePowerBlocks * SnakeAppConstants.SnakeVelocityEnginePowerModifier; 
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

        public void ChangeBatteringRamCount(int v)
        {
            SnakeModel.BatteringRamCount.Value += v;
            if (SnakeModel.BatteringRamCount.Value < SnakeModel.BatteringRamCount.PreviousValue)
            {
                if (SnakeModel.HeadBlockType.BlockType == BlockTypeEnum.BatteringRam)
                {
                    SnakeModel.HeadBlockType = BlockConfigData.CreateNewBlockType(BlockTypeEnum.Head);
                    SnakeView.GetComponent<Renderer>().material = SnakeModel.HeadBlockType.MaterialRef;
                }
                else
                {
                    foreach (var bodyPart in SnakeModel.BodyList)
                    {
                        if (bodyPart.SnakeBodyModel.BodyBlockType.BlockType == BlockTypeEnum.BatteringRam)
                        {
                            bodyPart.SetBodyBlockType(BlockConfigData.CreateNewBlockType(BlockTypeEnum.Head));
                            break;
                        }
                    }
                }
                CoroutinerManager.Start(ColliderDisableCoroutine());
            }
        }

        public void ChangeTimeTravelCount(int v)
        {
            SnakeModel.TimeTravelCount.Value += v;
            string persistedData = "";
            if (SnakeModel.TimeTravelCount.Value < SnakeModel.TimeTravelCount.PreviousValue)
            {
                if (SnakeModel.HeadBlockType.BlockType == BlockTypeEnum.TimeTravel)
                {
                    persistedData = SnakeModel.HeadBlockType.TimeTravelPersistedData;
                }
                else
                {
                    foreach (var bodyPart in SnakeModel.BodyList)
                    {
                        if (bodyPart.SnakeBodyModel.BodyBlockType.BlockType == BlockTypeEnum.TimeTravel)
                        {
                            persistedData = bodyPart.SnakeBodyModel.BodyBlockType.TimeTravelPersistedData;
                            
                            break;
                        }
                    }
                }
                if (persistedData == "")
                    return;
                Context.CommandManager.InvokeCommand(new LoadPersistedDataCommand(persistedData));
                CoroutinerManager.Start(ColliderDisableCoroutine());
            }
        }

        private IEnumerator ColliderDisableCoroutine()
        {
            SnakeView.GetComponent<BoxCollider2D>().enabled = false;
            yield return CoroutinerManager.WaitOneSecond;
            if (SnakeView != null)
                SnakeView.GetComponent<BoxCollider2D>().enabled = true;
            RestoreSnakeVelocity();
        }
    }

}
