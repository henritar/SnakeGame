using Architectures.UMVCS.Model.Data;
using Assets.Utils.Runtime.Managers;
using Project.Snake.UMVCS.Controller;
using Project.UMVCS.Controller.Commands;
using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{

    [System.Serializable]
    public enum BlockTypeEnum
    {
        EnginePower = 0,
        BatteringRam = 1,
        TimeTravel = 2,
        Head = 3
    }

    [CreateAssetMenu(fileName = "BlockType",
        menuName = SnakeAppConstants.CreateAssetMenuPath + "BlockType",
        order = SnakeAppConstants.CreateAssetMenuOrder)]

    
    public class BlockConfigData : BaseConfigData
    {
        [SerializeField]
        private BlockTypeEnum _blockType;

        [SerializeField]
        private Material _materialRef;

        [SerializeField]
        private string _TimeTravelPersistedData;

        public BlockTypeEnum BlockType { get => _blockType; set => _blockType = value; }
        public Material MaterialRef { get => _materialRef; set => _materialRef = value; }
        public string TimeTravelPersistedData { get => _TimeTravelPersistedData; set => _TimeTravelPersistedData = value; }

        public static BlockConfigData CreateNewBlockType(BlockTypeEnum type)
        {
            BlockConfigData blockData = Resources.Load<BlockConfigData>(type.ToString() + "BlockData");

            BlockConfigData newBlockType = CreateInstance<BlockConfigData>();

            newBlockType.BlockType = blockData.BlockType;
            newBlockType.MaterialRef = blockData.MaterialRef;
            newBlockType.TimeTravelPersistedData = "";
            return newBlockType;
        }

        public void ApplyPowerUp(SnakeController snakeController)
        {
            switch (_blockType)
            {
                case BlockTypeEnum.EnginePower:

                    snakeController.ChangeSnakeVelocity(SnakeAppConstants.SnakeVelocityEnginePowerModifier);
                    break;
                case BlockTypeEnum.BatteringRam:

                    snakeController.ChangeBatteringRamCount(1);
                    break;

                case BlockTypeEnum.TimeTravel:
                    snakeController.ChangeTimeTravelCount(1);
                    snakeController.SnakeModel.TriggeredTimeTravel = true;
                    CoroutinerManager.Start(TimeTravelPowerUpCoroutine(snakeController));
                    break;

                default: break;
            }
        }

        private IEnumerator TimeTravelPowerUpCoroutine(SnakeController snakeController)
        {
            yield return new WaitForSeconds(0.2f);
            snakeController.Context.CommandManager.InvokeCommand(new PersistDataCommand(this));
            
        }
    }
}