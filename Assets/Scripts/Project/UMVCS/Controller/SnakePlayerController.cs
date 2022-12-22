using Project.Snake.UMVCS.Controller;
using Project.Snake.UMVCS.Model;
using Project.UMVCS.Controller.Commands;
using Unity.VisualScripting;
using UnityEngine;

public class SnakePlayerController : SnakeController
{
    public SnakePlayerView SnakePlayerView { get => BaseView as SnakePlayerView; }
    public SnakePlayerModel SnakePlayerModel { get => BaseModel as SnakePlayerModel; }

    private void Awake()
    {
        SnakePlayerModel.MainModelRef = Context.ModelLocator.GetModel<MainModel>();
    }

    protected override void Start()
    {
        base.Start();

        
    }

    protected override void Update()
    {
        base.Update();

        if (SnakePlayerView.transform.position == SnakePlayerModel.Target.Value)
        {
            SetBodyTarget();
            if (SnakePlayerView.transform.position.x > SnakePlayerModel.MainModelRef.MainConfigData.BlockSpawnBounderiesX[SnakePlayerModel.Index].y)
            {
                SnakePlayerView.transform.position = new Vector3(SnakePlayerModel.MainModelRef.MainConfigData.BlockSpawnBounderiesX[SnakePlayerModel.Index].x, SnakePlayerView.transform.position.y);
                SnakePlayerModel.Target.Value = SnakePlayerView.transform.position + SnakePlayerModel.Direction.Value;
            }
            else if(SnakePlayerView.transform.position.x < SnakePlayerModel.MainModelRef.MainConfigData.BlockSpawnBounderiesX[SnakePlayerModel.Index].x)
            {
                SnakePlayerView.transform.position = new Vector3(SnakePlayerModel.MainModelRef.MainConfigData.BlockSpawnBounderiesX[SnakePlayerModel.Index].y, SnakePlayerView.transform.position.y);
                SnakePlayerModel.Target.Value = SnakePlayerView.transform.position + SnakePlayerModel.Direction.Value;
            }
            if (SnakePlayerView.transform.position.y > SnakePlayerModel.MainModelRef.MainConfigData.BlockSpawnBounderiesY[SnakePlayerModel.Index].y)
            {
                SnakePlayerView.transform.position = new Vector3(SnakePlayerView.transform.position.x, SnakePlayerModel.MainModelRef.MainConfigData.BlockSpawnBounderiesY[SnakePlayerModel.Index].x);
                SnakePlayerModel.Target.Value = SnakePlayerView.transform.position + SnakePlayerModel.Direction.Value;
            }
            else if (SnakePlayerView.transform.position.y < SnakePlayerModel.MainModelRef.MainConfigData.BlockSpawnBounderiesY[SnakePlayerModel.Index].x)
            {
                SnakePlayerView.transform.position = new Vector3(SnakePlayerView.transform.position.x, SnakePlayerModel.MainModelRef.MainConfigData.BlockSpawnBounderiesY[SnakePlayerModel.Index].y);
                SnakePlayerModel.Target.Value = SnakePlayerView.transform.position + SnakePlayerModel.Direction.Value;
            }
            else
            {
                SnakePlayerModel.Target.Value += SnakePlayerModel.Direction.Value;
            }

            LookAtTarget();
        }
    }

    protected override void AddListenersCallbacks()
    {
        base.AddListenersCallbacks();
        SnakePlayerView.OnSnakeHitSnake.AddListener(SnakePlayerView_OnSnakeHitSnake);
        Context.CommandManager.AddCommandListener<ChangeSnakeDirectionCommand>(CommandManager_OnChangeSnakeDirection);
    }

    protected override void RemoveListenersCallbacks()
    {
        base.RemoveListenersCallbacks();
        SnakePlayerView.OnSnakeHitSnake.RemoveListener(SnakePlayerView_OnSnakeHitSnake);
        Context.CommandManager.RemoveCommandListener<ChangeSnakeDirectionCommand>(CommandManager_OnChangeSnakeDirection);
    }

    private void ValidateDirectionChange(Vector3 newDir)
    {
        Vector3 nextTarget = newDir + SnakePlayerModel.Target.Value;
        if (nextTarget == SnakePlayerModel.Target.PreviousValue)
        {
            return;
        }
        SnakePlayerModel.Direction.Value = newDir;
    }

    protected override void SnakeView_OnBlockPicked(BlockController block)
    {
        Context.CommandManager.InvokeCommand(new SpawnAISnakeCommand(SnakePlayerModel.Index));
        base.SnakeView_OnBlockPicked(block);
    }

    private void SnakePlayerView_OnSnakeHitSnake(SnakeController otherSnake)
    {

        if (SnakePlayerModel.BodySize.Value > otherSnake.SnakeModel.BodySize.Value)
        {
            if (otherSnake.SnakeModel.TimeTravelCount.Value > 0)
            {
                otherSnake.ChangeTimeTravelCount(-1);
            }
            else
            {
                if (otherSnake is SnakeAIController)
                {
                    Context.CommandManager.InvokeCommand(new SpawnAISnakeCommand(otherSnake.SnakeModel.Index, otherSnake as SnakeAIController, true));
                }
                else
                {
                    Context.CommandManager.InvokeCommand(new KillPlayerSnakeCommand(otherSnake as SnakePlayerController));
                }
            }
        }
        else
        {
            if (SnakeModel.TimeTravelCount.Value > 0)
            {
                ChangeTimeTravelCount(-1);
            }
            else
            {
                Context.CommandManager.InvokeCommand(new KillPlayerSnakeCommand(this));
            }
        }
    }

    private void CommandManager_OnChangeSnakeDirection(ChangeSnakeDirectionCommand e)
    {
        if (e.Index == SnakePlayerModel.Index)
        {
            var dir = e.Direction;
            if (dir.x != 0)
            {
                ValidateDirectionChange(Vector3.right * dir.x);
            }
            else if (dir.y != 0)
            {
                ValidateDirectionChange(Vector3.up * dir.y);
            }
        }
    }
}
