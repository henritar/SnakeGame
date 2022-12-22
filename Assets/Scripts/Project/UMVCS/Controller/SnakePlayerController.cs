using Project.Snake.UMVCS.Controller;
using Project.UMVCS.Controller.Commands;
using UnityEngine;

public class SnakePlayerController : SnakeController
{
    public SnakePlayerView SnakePlayerView { get => BaseView as SnakePlayerView; }
    public SnakePlayerModel SnakePlayerModel { get => BaseModel as SnakePlayerModel; }

    protected override void Update()
    {
        base.Update();

        if (SnakePlayerView.transform.position == SnakePlayerModel.Target.Value)
        {
            SetBodyTarget();
            
            SnakePlayerModel.Target.Value += SnakePlayerModel.Direction.Value;
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
        Context.CommandManager.InvokeCommand(new SpawnAISnakeCommand(SnakeModel.Index));
        base.SnakeView_OnBlockPicked(block);
    }

    private void SnakePlayerView_OnSnakeHitSnake(SnakeController otherSnake)
    {

        if (SnakeModel.BodySize.Value > otherSnake.SnakeModel.BodySize.Value)
        {
            Debug.Log("BodySize > OtherBody Size");
            if (otherSnake.SnakeModel.TimeTravelCount.Value > 0)
            {
                Debug.Log("TimeTravelUsed: " + otherSnake.name);
                otherSnake.ChangeTimeTravelCount(-1);
            }
            else
            {
                if (otherSnake is SnakeAIController)
                {
                    Debug.Log("AIKilled: " + otherSnake.name);
                    Context.CommandManager.InvokeCommand(new SpawnAISnakeCommand(otherSnake.SnakeModel.Index, otherSnake as SnakeAIController, true));
                }
                else
                {
                    Debug.Log("SnakeKilled: " + name);
                    Context.CommandManager.InvokeCommand(new KillPlayerSnakeCommand(otherSnake as SnakePlayerController));
                }
            }
        }
        else
        {
            Debug.Log("BodySize < OtherBody Size");
            if (SnakeModel.TimeTravelCount.Value > 0)
            {
                Debug.Log("TimeTravelUsed: " + name);
                ChangeTimeTravelCount(-1);
            }
            else
            {
                Debug.Log("SnakeKilled: " + name);
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
