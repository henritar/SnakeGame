using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Architectures.UMVCS.View;
using Assets.Scripts.Project.UMVCS.Controller.Commands;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class MainController : BaseController<MainModel, MainView, NullService>
    {
        private MainModel _mainModel { get { return BaseModel as MainModel; } }
        private MainView _mainView { get { return BaseView as MainView; } }

        protected void Start()
        {
            Context.CommandManager.AddCommandListener<RestartApplicationCommand>(
                CommandManager_OnRestartApplication);

            Context.CommandManager.AddCommandListener<SpawnBlockCommand>(CommandManager_OnSpawnBlock);

            Context.CommandManager.AddCommandListener<AddBodyPartCommand>(CommandManager_OnAddBodyPart);

            Context.ModelLocator.AddModel(_mainModel);

            RestartApplication();
        }

        protected void OnDestroy()
        {
            Context.CommandManager.RemoveCommandListener<RestartApplicationCommand>(
                CommandManager_OnRestartApplication);

            Context.CommandManager.RemoveCommandListener<SpawnBlockCommand>(CommandManager_OnSpawnBlock);

            Context.CommandManager.RemoveCommandListener<AddBodyPartCommand>(CommandManager_OnAddBodyPart);

            Context.ModelLocator.RemoveModel(_mainModel);
        }

        private void RestartApplication()
        {
            if(_mainModel.SnakeView != null)
            {
                Destroy(_mainModel.SnakeView.gameObject);
                Destroy(Context.ModelLocator.GetModel<BlockModel>()?.transform.parent.gameObject);
            }

            _mainModel.SnakeView = Instantiate(_mainView.SnakeViewPrefab) as SnakeView;
            _mainModel.SnakeView.transform.SetParent(_mainView.MainParent);
            _mainModel.SnakeView.MoveSnake(_mainModel.MainConfigData.InitialSnakePosition);

            Context.CommandManager.InvokeCommand(new SpawnBlockCommand());

        }

        private void CommandManager_OnRestartApplication(RestartApplicationCommand e)
        {
            RestartApplication();
        }

        private void CommandManager_OnSpawnBlock(SpawnBlockCommand e)
        {
            Destroy(Context.ModelLocator.GetModel<BlockModel>()?.transform.parent.gameObject);

            var boundariesX = _mainModel.MainConfigData.BlockSpawnBounderiesX;
            var boundariesY = _mainModel.MainConfigData.BlockSpawnBounderiesY;
            Vector3 position = new Vector3(Random.Range(boundariesX.x, boundariesX.y), Random.Range(boundariesY.x, boundariesY.y), 0);
            BlockView newBlock = Instantiate(_mainView.BlockViewPrefab, position, Quaternion.identity) as BlockView;
            newBlock.transform.SetParent(_mainView.MainParent);
            _mainModel.BlockView = newBlock;
        }

        private void CommandManager_OnAddBodyPart(AddBodyPartCommand e)
        {
            SnakeBodyView newBody = Instantiate(_mainView.SnakeBodyViewPrefab, e.BlockPicked.BlockView.transform.position, Quaternion.identity) as SnakeBodyView;
            newBody.transform.SetParent(_mainModel.SnakeView.transform);
            e.PickSnake.SnakeModel.BodyList.Add(newBody.GetComponentInChildren<SnakeBodyController>());
        }
    }
}
