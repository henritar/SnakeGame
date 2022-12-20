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
        private MainModel MainModel { get { return BaseModel as MainModel; } }
        private MainView MainView { get { return BaseView as MainView; } }



        protected void Start()
        {
            Context.CommandManager.AddCommandListener<RestartApplicationCommand>(
                CommandManager_OnRestartApplication);

            Context.CommandManager.AddCommandListener<SpawnAISnakeCommand>(CommandManager_OnSpawnAISnake);

            Context.CommandManager.AddCommandListener<SpawnBlockCommand>(CommandManager_OnSpawnBlock);

            Context.CommandManager.AddCommandListener<AddBodyPartCommand>(CommandManager_OnAddBodyPart);

            RestartApplication();
        }

        protected virtual void OnDestroy()
        {
            Context.CommandManager.RemoveCommandListener<RestartApplicationCommand>(
                CommandManager_OnRestartApplication);

            Context.CommandManager.RemoveCommandListener<AddBodyPartCommand>(CommandManager_OnAddBodyPart);

            Context.CommandManager.RemoveCommandListener<SpawnBlockCommand>(CommandManager_OnSpawnBlock);

            Context.CommandManager.RemoveCommandListener<SpawnAISnakeCommand>(CommandManager_OnSpawnAISnake);

        }

        private void RestartApplication()
        {
            if(MainModel.SnakePlayerController != null)
            {
                foreach (SnakeBodyController bodyPart in MainModel.SnakePlayerController.SnakeModel.BodyList)
                {
                    Destroy(bodyPart.SnakeBodyView.gameObject);
                }
                Destroy(MainModel.SnakePlayerController.SnakeView.gameObject);
                MainModel.SnakePlayerController = null;
            }
            if (MainModel.SnakeAIController != null)
            {
                foreach (SnakeBodyController bodyPart in MainModel.SnakeAIController.SnakeModel.BodyList)
                {
                    Destroy(bodyPart.SnakeBodyView.gameObject);
                }
                Destroy(MainModel.SnakeAIController.SnakeView.gameObject);
                MainModel.SnakeAIController = null;
            }
            List<BlockModel> blocks = Context.ModelLocator.GetModels<BlockModel>();
            foreach (var block in blocks)
            {
                Context.ModelLocator.RemoveModel(block);
                Destroy(block.transform.parent.gameObject);
            }

            SnakePlayerView snakePlayerView = Instantiate(MainModel.SnakePlayerViewPrefab) as SnakePlayerView;
            MainModel.SnakePlayerController = snakePlayerView.GetComponentInChildren<SnakePlayerController>();
            snakePlayerView.transform.SetParent(MainModel.MainParent);
            snakePlayerView.MoveSnake(MainModel.MainConfigData.InitialSnakePosition);

            Context.CommandManager.InvokeCommand(new SpawnBlockCommand());
            Context.CommandManager.InvokeCommand(new SpawnAISnakeCommand());

        }

        private void CommandManager_OnRestartApplication(RestartApplicationCommand e)
        {
            RestartApplication();
        }

        private void CommandManager_OnSpawnAISnake(SpawnAISnakeCommand e)
        {
            if (MainModel.SnakeAIController != null)
            {
                MainModel.SnakeAIController.ShuffleLocation(e.ResetSnake);
            }
            else
            {
                SnakeAIView newSnakeAI = Instantiate(MainModel.SnakeAIViewPrefab, GetRandomPosition(), Quaternion.identity) as SnakeAIView;
                newSnakeAI.transform.SetParent(MainModel.MainParent);
                MainModel.SnakeAIController = newSnakeAI.GetComponentInChildren<SnakeAIController>();
            }
        }

        private void CommandManager_OnSpawnBlock(SpawnBlockCommand e)
        {
            BlockModel blockModel = Context.ModelLocator.GetModel<BlockModel>();
            if (blockModel != null)
            {
                Destroy(blockModel.transform.parent.gameObject);
            }

            BlockView newBlock = Instantiate(MainModel.BlockViewPrefab, GetRandomPosition(), Quaternion.identity) as BlockView;
            newBlock.transform.SetParent(MainModel.MainParent);
            MainModel.BlockController = newBlock.GetComponentInChildren<BlockController>();
            Context.CommandManager.InvokeCommand(new SnakeAIDestinationCommand(newBlock.transform.position));
        }

        private Vector3 GetRandomPosition()
        {
            var boundariesX = MainModel.MainConfigData.BlockSpawnBounderiesX;
            var boundariesY = MainModel.MainConfigData.BlockSpawnBounderiesY;
            Vector3 position = new Vector3(Random.Range(boundariesX.x, boundariesX.y), Random.Range(boundariesY.x, boundariesY.y), 0);
            return position;
        }

        private void CommandManager_OnAddBodyPart(AddBodyPartCommand e)
        {
            SnakeBodyView bodyView = Instantiate(MainModel.SnakeBodyViewPrefab, e.BlockPicked.BlockView.transform.position, Quaternion.identity) as SnakeBodyView;
            bodyView.transform.SetParent(MainModel.MainParent);
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
