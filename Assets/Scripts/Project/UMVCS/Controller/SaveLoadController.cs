using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Architectures.UMVCS.View;
using Project.Data.Types;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Snake.UMVCS.Controller
{
    public class SaveLoadController : BaseController<SaveLoadModel, NullView, NullService>
    {
        public SaveLoadModel SaveLoadModel { get => BaseModel as SaveLoadModel; }

        private void Start()
        {
            SaveLoadModel.PersistentData = new SaveLoadModel.PersistData();
            SaveLoadModel.PersistentData.SnakePersistenceList = new List<SnakePersistence>();
            SaveLoadModel.PersistentData.BlockPersistenceList = new List<BlockPersistence>();

            Context.CommandManager.AddCommandListener<PersistDataCommand>(CommandManager_OnPersistData);
            Context.CommandManager.AddCommandListener<LoadPersistedDataCommand>(CommandManager_OnLoadPersistedData);
        }

        private void OnDestroy()
        {
            Context.CommandManager.RemoveCommandListener<PersistDataCommand>(CommandManager_OnPersistData);
            Context.CommandManager.RemoveCommandListener<LoadPersistedDataCommand>(CommandManager_OnLoadPersistedData);
        }

        private void CommandManager_OnPersistData(PersistDataCommand e)
        {
            SaveLoadModel.PersistentData.SnakePersistenceList.Clear();
            SaveLoadModel.PersistentData.BlockPersistenceList.Clear();

            List<SnakeModel> snakeModelList = Context.ModelLocator.GetModels<SnakeModel>();
            foreach (var snakeModel in snakeModelList)
            {
                
                SnakePersistence snake = new SnakePersistence(snakeModel);

                SaveLoadModel.PersistentData.SnakePersistenceList.Add(snake);
            }

            List<BlockModel> blockModelList = Context.ModelLocator.GetModels<BlockModel>();
            foreach (var blockModel in blockModelList)
            {
                BlockPersistence block = new BlockPersistence(blockModel);

                SaveLoadModel.PersistentData.BlockPersistenceList.Add(block);
            }

                e.Block.TimeTravelPersistedData = JsonUtility.ToJson(SaveLoadModel.PersistentData);
        }

        private void CommandManager_OnLoadPersistedData(LoadPersistedDataCommand e)
        {
            string jsonPersistence = e.PersistedData;

            SaveLoadModel.PersistData _saveLoadModel = JsonUtility.FromJson<SaveLoadModel.PersistData>(jsonPersistence);
            SaveLoadModel.PersistentData.SnakePersistenceList = _saveLoadModel.SnakePersistenceList;
            SaveLoadModel.PersistentData.BlockPersistenceList = _saveLoadModel.BlockPersistenceList;
            
            
            List<SnakeModel> snakeModelList = Context.ModelLocator.GetModels<SnakeModel>();
            for (int i = 0; i < snakeModelList.Count; i++)
            {
                var snakeModel = snakeModelList[i];
                var snakePersistence = SaveLoadModel.PersistentData.SnakePersistenceList[i];

                SnakeController snakeController = snakeModel.transform.parent.GetComponentInChildren<SnakeController>();

                snakeController.SetHeadBlockType(BlockConfigData.CreateNewBlockType(snakePersistence.HeadBlockType));
                snakeModel.transform.parent.transform.position = snakePersistence.Position;
                snakeModel.Target.Value = snakePersistence.Target;
                snakeModel.Velocity.Value = snakePersistence.Velocity;
                snakeModel.Direction.Value = snakePersistence.Direction;
                snakeModel.BatteringRamCount.Value = snakePersistence.BatteringRamCount;
                snakeModel.BodySize.Value = snakePersistence.BodyList.Count;
                snakeModel.HeadBlockType.TimeTravelPersistedData = snakePersistence.TimeTravelPersistedData;

                if (snakeModel.BodyList.Count > snakePersistence.BodyList.Count)
                {
                    while(snakeModel.BodyList.Count != snakePersistence.BodyList.Count)
                    {
                        var bodyPart = snakeModel.BodyList[snakeModel.BodyList.Count - 1];
                        snakeModel.BodyList.RemoveAt(snakeModel.BodyList.Count - 1);
                        Destroy(bodyPart.transform.parent.gameObject);
                    }
                }
                else if ( snakeModel.BodyList.Count < snakePersistence.BodyList.Count)
                {
                    while (snakeModel.BodyList.Count != snakePersistence.BodyList.Count)
                    {
                        MainModel mainModel = Context.ModelLocator.GetModel<MainModel>();
                        SnakeBodyView bodyView = Instantiate(mainModel.SnakeBodyViewPrefab);
                        snakeController.AddBodyPart(bodyView.GetComponentInChildren<SnakeBodyController>());
                    }
                }

                SnakeAIModel snakeAIModel = snakeModelList[i] as SnakeAIModel;
                if (snakeAIModel != null)
                {
                    snakeAIModel.BlockPosition.Value = snakePersistence.BlockPosition;
                }

                for (int j = 0; j < snakePersistence.BodyList.Count; j++)
                {
                    var snakeBodyModel = snakeModel.BodyList[j].SnakeBodyModel;
                    var snakeBodyPersistence = snakePersistence.BodyList[j];

                    snakeModel.BodyList[j].SnakeBodyView.transform.position = snakeBodyPersistence.Position;
                    snakeBodyModel.Target.Value = snakeBodyPersistence.Target;
                    snakeBodyModel.Velocity.Value = snakeBodyPersistence.Velocity;
                    snakeBodyModel.WaitUps.Value = snakeBodyPersistence.WaitUps;
                    snakeModel.BodyList[j].SetBodyBlockType(BlockConfigData.CreateNewBlockType(snakeBodyPersistence.BlockType));
                    snakeBodyModel.BodyBlockType.TimeTravelPersistedData = snakeBodyPersistence.TimeTravelPersistedData;
                }
            }

            List<BlockModel> blockModelList = Context.ModelLocator.GetModels<BlockModel>();
            for (int i = 0; i < blockModelList.Count; i++)
            {
                var blockModel = blockModelList[i];
                var blockPersistence = SaveLoadModel.PersistentData.BlockPersistenceList[i];

                blockModel.transform.parent.transform.position = blockPersistence.Position;
                var blockType = BlockConfigData.CreateNewBlockType(blockPersistence.BlockType);
                blockModel.BlockType = blockType;
                blockModel.transform.parent.GetComponent<Renderer>().material = blockType.MaterialRef;
            }
        }
    }
}