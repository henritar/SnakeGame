using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Assets.Scripts.Project.UMVCS.Controller.Commands;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class MainController : BaseController<MainModel, MainView, NullService>
    {
        private MainModel MainModel { get { return BaseModel as MainModel; } }
        private MainView MainView { get { return BaseView as MainView; } }



        protected void Start()
        {
            MainModel.BlockController = new List<BlockController>();
            MainModel.SnakeAIController= new List<SnakeAIController>();
            MainModel.SnakePlayerController= new List<SnakePlayerController>();
            MainModel.SnakeBodyController= new List<SnakeBodyController>();
            MainModel.MainParent = new List<Transform>();

            Context.CommandManager.AddCommandListener<RestartApplicationCommand>(
                CommandManager_OnRestartApplication);

            Context.CommandManager.AddCommandListener<SpawnAISnakeCommand>(CommandManager_OnSpawnAISnake);

            Context.CommandManager.AddCommandListener<KillPlayerSnakeCommand>(CommandManager_OnKillPlayerSnake);

            Context.CommandManager.AddCommandListener<SpawnBlockCommand>(CommandManager_OnSpawnBlock);

            Context.CommandManager.AddCommandListener<AddBodyPartCommand>(CommandManager_OnAddBodyPart);

            CreateParentsAndCamera();

            RestartApplication();
        }

        private void CreateParentsAndCamera()
        {

            for (int index = 0; index < MainModel.NumberOfPlayers; index++)
            {
                GameObject mainParent = new GameObject(SnakeAppConstants.MainParent + index);
                mainParent.transform.SetParent(MainView.transform);
                MainModel.MainParent.Add(mainParent.transform);
                CreateCamera(index);
            }
        }

        private void CreateCamera(int index)
        {
                CameraView cameraView = Instantiate(MainModel.CameraViewPrefab, Vector3.back * 10, Quaternion.identity) as CameraView;
                CameraController cameraController = cameraView.GetComponentInChildren<CameraController>();
                cameraController.CameraModel.Index = index;
                MainModel.CameraController.Add(cameraController);
                cameraView.transform.SetParent(MainModel.MainParent[index]);
        }

        protected virtual void OnDestroy()
        {
            Context.CommandManager.RemoveCommandListener<AddBodyPartCommand>(CommandManager_OnAddBodyPart);

            Context.CommandManager.RemoveCommandListener<SpawnBlockCommand>(CommandManager_OnSpawnBlock);

            Context.CommandManager.RemoveCommandListener<SpawnAISnakeCommand>(CommandManager_OnSpawnAISnake);

            Context.CommandManager.RemoveCommandListener<KillPlayerSnakeCommand>(CommandManager_OnKillPlayerSnake);

            Context.CommandManager.RemoveCommandListener<RestartApplicationCommand>(
                CommandManager_OnRestartApplication);

        }

        private void CommandManager_OnKillPlayerSnake(KillPlayerSnakeCommand e)
        {
            e.PlayerSnake.KillSnake();

            List<SnakePlayerController> _remainingPlayers = Context.ModelLocator.GetModels<SnakePlayerController>();

            if (_remainingPlayers.Count == 0) 
            {
                RestartApplication();
            }

        }

        private void RestartApplication()
        {
            if(MainModel.SnakePlayerController.Count > 0)
            {
                foreach (var controller in MainModel.SnakePlayerController)
                {

                    foreach (SnakeBodyController bodyPart in controller.SnakeModel.BodyList)
                    {
                        Destroy(bodyPart.SnakeBodyView.gameObject);
                    }
                    Destroy(controller.SnakeView.gameObject);
                }
                MainModel.SnakePlayerController = new List<SnakePlayerController>();
            }
            if (MainModel.SnakeAIController.Count > 0)
            {
                foreach (var controller in MainModel.SnakeAIController)
                {
                    foreach (SnakeBodyController bodyPart in controller.SnakeModel.BodyList)
                    {
                        Destroy(bodyPart.SnakeBodyView.gameObject);
                    }
                    Destroy(controller.SnakeView.gameObject);
                }
                MainModel.SnakeAIController = new List<SnakeAIController>();
            }

            if (MainModel.BlockController.Count > 0)
            {
                foreach (var block in MainModel.BlockController)
                {
                    if (block != null)
                    {
                        Destroy(block.BlockView.gameObject);
                    }
                }
                MainModel.BlockController= new List<BlockController>();
            }


            for (int i = 0; i < MainModel.NumberOfPlayers; i++)
            {
                SnakePlayerView snakePlayerView = Instantiate(MainModel.SnakePlayerViewPrefab) as SnakePlayerView;
                snakePlayerView.transform.SetParent(MainModel.MainParent[i].transform);
                SnakePlayerController snakePlayerController = snakePlayerView.GetComponentInChildren<SnakePlayerController>();
                snakePlayerController.SnakeModel.Index = i;
                MainModel.SnakePlayerController.Add(snakePlayerController); 
                snakePlayerView.MoveSnake(MainModel.MainConfigData.InitialSnakePosition[i]);

                Context.CommandManager.InvokeCommand(new SpawnBlockCommand(i));
                Context.CommandManager.InvokeCommand(new SpawnAISnakeCommand(i));

            }
        }

        private void CommandManager_OnRestartApplication(RestartApplicationCommand e)
        {
            RestartApplication();
        }

        private void CommandManager_OnSpawnAISnake(SpawnAISnakeCommand e)
        {
            if (e.Index < MainModel.SnakeAIController.Count)
            {
                Debug.Log(MainModel.SnakeAIController.Count);
                MainModel.SnakeAIController[e.Index].ShuffleLocation(e.ResetSnake);
            }
            else
            {
                SnakeAIView newSnakeAI = Instantiate(MainModel.SnakeAIViewPrefab, GetRandomPosition(e.Index), Quaternion.identity) as SnakeAIView;
                newSnakeAI.transform.SetParent(MainModel.MainParent[e.Index].transform);
                SnakeAIController snakeController = newSnakeAI.GetComponentInChildren<SnakeAIController>();
                snakeController.SnakeAIModel.Index = e.Index;
                MainModel.SnakeAIController.Add(snakeController);
            }
        }

        private void CommandManager_OnSpawnBlock(SpawnBlockCommand e)
        {
            BlockView newBlock = Instantiate(MainModel.BlockViewPrefab, GetRandomPosition(e.Index), Quaternion.identity) as BlockView;
            newBlock.transform.SetParent(MainModel.MainParent[e.Index].transform);
            BlockController blockController = newBlock.GetComponentInChildren<BlockController>();
            if (e.Index < MainModel.BlockController.Count)
            {
                Destroy(MainModel.BlockController[e.Index].BlockView.gameObject);
                
                MainModel.BlockController[e.Index] = blockController;
            }
            else
            {
                MainModel.BlockController.Add(blockController);
            }
            
            Context.CommandManager.InvokeCommand(new SnakeAIDestinationCommand(newBlock.transform.position));
    }

        private Vector3 GetRandomPosition(int index)
        {
            var boundariesX = MainModel.MainConfigData.BlockSpawnBounderiesX[index];
            var boundariesY = MainModel.MainConfigData.BlockSpawnBounderiesY[index];
            Vector3 position = new Vector3(Random.Range(boundariesX.x, boundariesX.y), Random.Range(boundariesY.x, boundariesY.y), 0);
            return position;
        }

        private void CommandManager_OnAddBodyPart(AddBodyPartCommand e)
        {
            SnakeBodyView bodyView = Instantiate(MainModel.SnakeBodyViewPrefab, e.BlockPicked.BlockView.transform.position, Quaternion.identity) as SnakeBodyView;
            bodyView.transform.SetParent(MainModel.MainParent[e.PickSnake.SnakeModel.Index]);
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
