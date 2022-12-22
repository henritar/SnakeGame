using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Assets.Scripts.Project.UMVCS.Controller.Commands;
using Project.Data.Types;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class MainController : BaseController<MainModel, MainView, NullService>
    {
        private MainModel MainModel { get { return BaseModel as MainModel; } }
        private MainView MainView { get { return BaseView as MainView; } }


        private void Awake()
        {
            InitializeModelProperties();

            InitializePlayersOffSet();
        }

        protected void Start()
        {
            AddCommandManagerListeners();
            
        }

        protected virtual void OnDestroy()
        {
            RemoveCommandManagerListeners();

        }

        private void InitializeModelProperties()
        {
            MainModel.BlockController = new List<BlockController>();
            MainModel.SnakeAIController = new List<SnakeAIController>();
            MainModel.SnakePlayerController = new List<SnakePlayerController>();
            MainModel.SnakeBodyController = new List<SnakeBodyController>();
            MainModel.MainParent = new List<Transform>();
        }

        private void AddCommandManagerListeners()
        {
            Context.CommandManager.AddCommandListener<StartApplicationCommand>(CommandManager_OnStartApplication);

            Context.CommandManager.AddCommandListener<RestartApplicationCommand>(
                            CommandManager_OnRestartApplication);

            Context.CommandManager.AddCommandListener<SpawnAISnakeCommand>(CommandManager_OnSpawnAISnake);

            Context.CommandManager.AddCommandListener<KillPlayerSnakeCommand>(CommandManager_OnKillPlayerSnake);

            Context.CommandManager.AddCommandListener<SpawnBlockCommand>(CommandManager_OnSpawnBlock);

            Context.CommandManager.AddCommandListener<AddBodyPartCommand>(CommandManager_OnAddBodyPart);

            Context.CommandManager.AddCommandListener<ChangeNumberPlayer>(CommandManager_OnChangeNumberPlayers);

            Context.CommandManager.AddCommandListener<UpdatePlayersSpriteCommand>(CommandManager_OnUpdatePlayersSprite);


        }

        private void RemoveCommandManagerListeners()
        {
            Context.CommandManager.AddCommandListener<UpdatePlayersSpriteCommand>(CommandManager_OnUpdatePlayersSprite);

            Context.CommandManager.RemoveCommandListener<StartApplicationCommand>(CommandManager_OnStartApplication);

            Context.CommandManager.RemoveCommandListener<ChangeNumberPlayer>(CommandManager_OnChangeNumberPlayers);

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
            if (MainModel.SnakePlayerController.Count == 0) 
            {
                RestartAllApplication();
            }

        }

        private void InitializePlayersOffSet()
        {
            for (int i = 1; i < MainModel.NumberOfPlayers; i++)
            {
                
                MainModel.MainConfigData.InitialSnakePosition.Add(MainModel.MainConfigData.InitialSnakePosition[0] + 100 * i * Vector3.one);
                MainModel.MainConfigData.BlockSpawnBounderiesX.Add(MainModel.MainConfigData.BlockSpawnBounderiesX[0] + 100 * i * Vector2Int.one);
                MainModel.MainConfigData.BlockSpawnBounderiesY.Add(MainModel.MainConfigData.BlockSpawnBounderiesY[0] + 100 * i * Vector2Int.one);
            }
        }

        private void CreateParentsAndCamera()
        {
            int ratio = 1, i = 0;

            do
            {
                ratio = (int) Mathf.Pow(2, i++);
            } while (MainModel.NumberOfPlayers > ratio);

            float offSet = 1f / ratio;


            float power = Mathf.Pow(2, i - 2);
            Vector2 viewPort = i == 1 ? Vector2.one : i % 2 == 0 ? new Vector2(offSet * power, offSet) : new Vector2(offSet * power, offSet * power);

            for (int index = 0; index < MainModel.NumberOfPlayers; index++)
            {
                GameObject mainParent = new GameObject(SnakeAppConstants.MainParent + index);
                mainParent.transform.SetParent(MainView.transform);
                MainModel.MainParent.Add(mainParent.transform);
                
                
                CameraConfigData cameraConfigData = ScriptableObject.CreateInstance<CameraConfigData>();

                switch (index % 4)
                {
                    case 0:
                        if (index == 0)
                        {
                            cameraConfigData.Init(viewPort, Vector2.zero);
                            break;
                        }
                        cameraConfigData.Init(viewPort, Vector2.one * viewPort);
                        break;
                    case 1:
                        cameraConfigData.Init(viewPort, Vector2.right * viewPort);
                        break;
                    case 2:
                        cameraConfigData.Init(viewPort, Vector2.up * viewPort);
                        break;
                    case 3:
                        cameraConfigData.Init(viewPort, Vector2.one * viewPort);
                        break;
                }
                cameraConfigData.InputKeys = MainModel.MainConfigData.CameraInputControllers;
                CreateCamera(index, cameraConfigData);
            }
        }

        private void CreateCamera(int index, CameraConfigData cameraConfigData)
        {
            UIModel uIModel = Context.ModelLocator.GetModel<UIModel>();
            HUDCameraModel HUDCamera = Instantiate(uIModel.HUDCameraPrefab, MainModel.MainParent[index]) as HUDCameraModel;
            Vector3 cameraPosition = new Vector3(MainModel.MainConfigData.InitialSnakePosition[index].x, MainModel.MainConfigData.InitialSnakePosition[index].y, -10);
            CameraView cameraView = Instantiate(MainModel.CameraViewPrefab, cameraPosition, Quaternion.identity) as CameraView;
            HUDCamera.GetComponent<Canvas>().worldCamera = cameraView.GetComponent<Camera>();
            CameraController cameraController = cameraView.GetComponentInChildren<CameraController>();
            HUDCamera.InputKey.text = MainModel.MainConfigData.CameraInputControllers[index];
            HUDCamera.PlayerName.text = "Player " + (index + 1);
            cameraController.CameraModel.Index = index;
            cameraController.CameraModel.CameraConfigData = cameraConfigData;
            cameraController.CameraModel.InitCamera(index, cameraConfigData);
            MainModel.CameraController.Add(cameraController);
            cameraView.transform.SetParent(MainModel.MainParent[index]);
        }


        private void RestartAllApplication()
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
                SnakePlayerView snakePlayerView = Instantiate(MainModel.SnakePlayerViewPrefab, MainModel.MainConfigData.InitialSnakePosition[i], Quaternion.identity) as SnakePlayerView;
                snakePlayerView.transform.SetParent(MainModel.MainParent[i].transform);
                SnakePlayerController snakePlayerController = snakePlayerView.GetComponentInChildren<SnakePlayerController>();
                snakePlayerController.SnakeModel.Index = i;
                MainModel.SnakePlayerController.Add(snakePlayerController); 
                snakePlayerView.MoveSnake(MainModel.MainConfigData.InitialSnakePosition[i]);
                
                switch (MainModel.SnakeTypeSelected[i])
                {
                    case SelectionSnakeMenuEnum.defaultSnake:
                        InitializeNewBodyPartOfType(i, BlockTypeEnum.Head, MainModel.MainConfigData.InitialSnakePosition[i] + Vector3.down);
                        InitializeNewBodyPartOfType(i, BlockTypeEnum.Head, MainModel.MainConfigData.InitialSnakePosition[i] + Vector3.down);
                        break;
                    case SelectionSnakeMenuEnum.imortalSnake:
                        MainModel.SnakePlayerController[i].SetHeadBlockType(BlockConfigData.CreateNewBlockType(BlockTypeEnum.BatteringRam));
                        InitializeNewBodyPartOfType(i, BlockTypeEnum.BatteringRam, MainModel.MainConfigData.InitialSnakePosition[i] + Vector3.down);
                        InitializeNewBodyPartOfType(i, BlockTypeEnum.BatteringRam, MainModel.MainConfigData.InitialSnakePosition[i] + Vector3.down);
                        break;
                    case SelectionSnakeMenuEnum.speedySnake:
                        InitializeNewBodyPartOfType(i, BlockTypeEnum.EnginePower, MainModel.MainConfigData.InitialSnakePosition[i] + Vector3.down);
                        InitializeNewBodyPartOfType(i, BlockTypeEnum.EnginePower, MainModel.MainConfigData.InitialSnakePosition[i] + Vector3.down);
                        break;
                }

                Context.CommandManager.InvokeCommand(new SpawnBlockCommand(i));
                Context.CommandManager.InvokeCommand(new SpawnAISnakeCommand(i));

            }
        }

        private SnakeBodyView InitializeNewBodyPartOfType(int i, BlockTypeEnum type, Vector3 position)
        {
            SnakeBodyView bodyView = Instantiate(MainModel.SnakeBodyViewPrefab, position, Quaternion.identity, MainModel.MainParent[i].transform) as SnakeBodyView;
            SnakeBodyController bodyController = bodyView.GetComponentInChildren<SnakeBodyController>();
            bodyController.InitializeBodyPart(MainModel.SnakePlayerController[i]);
            bodyController.SetBodyBlockType(BlockConfigData.CreateNewBlockType(type));
            MainModel.SnakePlayerController[i].AddBodyPart(bodyController);
            return bodyView;
        }

        private void CommandManager_OnUpdatePlayersSprite(UpdatePlayersSpriteCommand e)
        {
            MainModel.SnakeTypeSelected = e.PlayersSnake;
        }
        private void CommandManager_OnStartApplication(StartApplicationCommand e)
        {
            if (MainModel.NumberOfPlayers == 0)
                return;

            Context.CommandManager.InvokeCommand(new ToggleStartMenuCommand(false));

            CreateParentsAndCamera();

            RestartAllApplication();
        }

        private void CommandManager_OnRestartApplication(RestartApplicationCommand e)
        {
            RestartAllApplication();
        }

        private void CommandManager_OnChangeNumberPlayers(ChangeNumberPlayer e)
        {
            MainModel.NumberOfPlayers = e.NumberOfPlayers;
        }

        private void CommandManager_OnSpawnAISnake(SpawnAISnakeCommand e)
        {
            if (e.Index < MainModel.SnakeAIController.Count)
            {
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
            
            Context.CommandManager.InvokeCommand(new SnakeAIDestinationCommand(newBlock.transform.position, e.Index));
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
