using Project.Snake.UMVCS.Controller;
using Project.Snake.UMVCS.Model;
using Project.UMVCS.Controller.Commands;
using UnityEngine;

public class SnakePlayerController : SnakeController
{
    public SnakePlayerView SnakePlayerView { get => BaseView as SnakePlayerView; }
    public SnakePlayerModel SnakePlayerModel { get => BaseModel as SnakePlayerModel; }

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

        if (SnakeModel.Direction.Value.x == -newDir.x || SnakeModel.Direction.Value.y == -newDir.y)
        {
            return;
        }
        SnakeModel.Direction.Value = newDir;
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
}
