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

        }
    }
    protected override void AddListenersCallbacks()
    {
        base.AddListenersCallbacks();
        Context.CommandManager.AddCommandListener<ChangeSnakeDirectionCommand>(CommandManager_OnChangeSnakeDirection);
    }

    protected override void RemoveListenersCallbacks()
    {
        base.RemoveListenersCallbacks();
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
        Context.CommandManager.InvokeCommand(new SpawnAISnakeCommand());
        base.SnakeView_OnBlockPicked(block);
    }

    private void CommandManager_OnChangeSnakeDirection(ChangeSnakeDirectionCommand e)
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
