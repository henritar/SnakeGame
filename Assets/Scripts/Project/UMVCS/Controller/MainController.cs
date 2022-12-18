using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Architectures.UMVCS.View;
using Assets.Scripts.Project.UMVCS.Controller.Commands;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

            Context.CommandManager.AddCommandListener<SpawnAISnakeCommand>(CommandManager_OnSpawnAISnake);

            Context.CommandManager.AddCommandListener<SpawnBlockCommand>(CommandManager_OnSpawnBlock);

            Context.CommandManager.AddCommandListener<AddBodyPartCommand>(CommandManager_OnAddBodyPart);


            Context.ModelLocator.AddModel(_mainModel);

            RestartApplication();
        }

        protected void OnDestroy()
        {
            Context.CommandManager.RemoveCommandListener<RestartApplicationCommand>(
                CommandManager_OnRestartApplication);

            Context.CommandManager.RemoveCommandListener<AddBodyPartCommand>(CommandManager_OnAddBodyPart);

            Context.CommandManager.RemoveCommandListener<SpawnBlockCommand>(CommandManager_OnSpawnBlock);

            Context.CommandManager.RemoveCommandListener<SpawnAISnakeCommand>(CommandManager_OnSpawnAISnake);

            Context.ModelLocator.RemoveModel(_mainModel);
        }

        private void RestartApplication()
        {
            if(_mainModel.SnakeController != null)
            {
                foreach (SnakeBodyController bodyPart in _mainModel.SnakeController.SnakeModel.BodyList)
                {
                    Destroy(bodyPart.SnakeBodyView.gameObject);
                }
                Destroy(_mainModel.SnakeController.SnakeView.gameObject);
            }
            if (_mainModel.SnakeAIController != null)
            {
                foreach (SnakeBodyController bodyPart in _mainModel.SnakeAIController.SnakeModel.BodyList)
                {
                    Destroy(bodyPart.SnakeBodyView.gameObject);
                }
                Destroy(_mainModel.SnakeAIController.SnakeView.gameObject);
            }
            Destroy(Context.ModelLocator.GetModel<BlockModel>()?.transform.parent.gameObject);

            

            SnakeView snakeView = Instantiate(_mainView.SnakeViewPrefab) as SnakeView;
            _mainModel.SnakeController = snakeView.GetComponentInChildren<SnakeController>();
            snakeView.transform.SetParent(_mainView.MainParent);
            snakeView.MoveSnake(_mainModel.MainConfigData.InitialSnakePosition);

            Context.CommandManager.InvokeCommand(new SpawnBlockCommand());
            Context.CommandManager.InvokeCommand(new SpawnAISnakeCommand());

        }

        private void CommandManager_OnRestartApplication(RestartApplicationCommand e)
        {
            RestartApplication();
        }

        private void CommandManager_OnSpawnAISnake(SpawnAISnakeCommand e)
        {
            if (_mainModel.SnakeAIController != null)
            {
                _mainModel.SnakeAIController.ShuffleLocation();
            }
            else
            {
                SnakeAIView newSnakeAI = Instantiate(_mainView.SnakeAIViewPrefab, GetRandomPosition(), Quaternion.identity) as SnakeAIView;
                newSnakeAI.transform.SetParent(_mainView.MainParent);
                _mainModel.SnakeAIController = newSnakeAI.GetComponentInChildren<SnakeAIController>();
            }
        }

        private void CommandManager_OnSpawnBlock(SpawnBlockCommand e)
        {
            Destroy(Context.ModelLocator.GetModel<BlockModel>()?.transform.parent.gameObject);

            BlockView newBlock = Instantiate(_mainView.BlockViewPrefab, GetRandomPosition(), Quaternion.identity) as BlockView;
            newBlock.transform.SetParent(_mainView.MainParent);
            _mainModel.BlockController = newBlock.GetComponentInChildren<BlockController>();
            Debug.Log("BLOCK LOCATION: " + newBlock.transform.position);
            Context.CommandManager.InvokeCommand(new SnakeAIDestinationCommand(newBlock.transform.position));
        }

        private Vector3 GetRandomPosition()
        {
            var boundariesX = _mainModel.MainConfigData.BlockSpawnBounderiesX;
            var boundariesY = _mainModel.MainConfigData.BlockSpawnBounderiesY;
            Vector3 position = new Vector3(Random.Range(boundariesX.x, boundariesX.y), Random.Range(boundariesY.x, boundariesY.y), 0);
            return position;
        }

        private void CommandManager_OnAddBodyPart(AddBodyPartCommand e)
        {
            SnakeBodyView bodyView = Instantiate(_mainView.SnakeBodyViewPrefab, e.BlockPicked.BlockView.transform.position, Quaternion.identity) as SnakeBodyView;
            bodyView.transform.SetParent(_mainView.MainParent);
            SnakeBodyController bodyController = bodyView.GetComponentInChildren<SnakeBodyController>();
            bodyController.InitializeBodyPart(e.PickSnake);

            bodyController.SetBodyBlockType(e.BlockPicked.BlockModel.BlockType);

            List<SnakeBodyController> bodyList = e.PickSnake.SnakeModel.BodyList;
            e.PickSnake.AddBodyPart(bodyController);

            var previousType = e.PickSnake.SnakeModel.HeadBlockType;
            e.PickSnake.SetHeadBlockType(e.BlockPicked.BlockModel.BlockType);

            foreach (var bodyPart in bodyList)
            {
                var tempType = bodyPart.SnakeBodyModel.BodyBlockType;
                bodyPart.SetBodyBlockType(previousType);
                previousType = tempType;
            }
        }
    }
}
