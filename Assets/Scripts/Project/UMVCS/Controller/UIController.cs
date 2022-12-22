using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Assets.Scripts.Project.UMVCS.Controller.Commands;
using Assets.Scripts.Project.UMVCS.View;
using Project.Data.Types;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using static Project.Snake.UMVCS.Model.UIModel;

namespace Project.Snake.UMVCS.Controller
{
    public class UIController : BaseController<UIModel, UIView, NullService>
    {
        public UIModel UIModel { get => BaseModel as UIModel; }
        public UIView UIView { get => BaseView as UIView; }

        private void Start()
        {
            UIModel.PlayerSnakeUIList = new List<UIModel.PlayerSnakeUI>();
            UIModel.KeyCodes = new HashSet<KeyCode>();
            Context.CommandManager.AddCommandListener<AddNewPlayerCommand>(CommandManager_OnAddNewPlayer);
            Context.CommandManager.AddCommandListener<ToggleStartMenuCommand>(CommandManager_OnToggleStartMenu);
            Context.CommandManager.AddCommandListener<ChangeSnakeTypeCommand>(CommandManager_OnChangeSnakeType);
        }

        private void OnDestroy()
        {
            Context.CommandManager.RemoveCommandListener<ToggleStartMenuCommand>(CommandManager_OnToggleStartMenu);
        }

        private void CommandManager_OnToggleStartMenu(ToggleStartMenuCommand e)
        {
            UIModel.StartMenuCanvas.gameObject.SetActive(e.Toggle);
            UIModel.UICamera.gameObject.SetActive(e.Toggle);
            Context.CommandManager.RemoveCommandListener<AddNewPlayerCommand>(CommandManager_OnAddNewPlayer);
            Context.CommandManager.RemoveCommandListener<ChangeSnakeTypeCommand>(CommandManager_OnChangeSnakeType);
        }

        private void CommandManager_OnAddNewPlayer(AddNewPlayerCommand e) 
        {
            e.KeyCode.ExceptWith(UIModel.KeyCodes);

            if (e.KeyCode.Count < 2 || UIModel.NumberOfPlayers == 4)
            {
                return;
            }

            UIModel.KeyCodes.UnionWith(e.KeyCode);

            List<KeyCode> keys = e.KeyCode.ToList();
            Context.CommandManager.InvokeCommand(new ChangeNumberPlayer(++UIModel.NumberOfPlayers));

            NewPlayerUIView newPlayer = Instantiate(UIModel.NewPlayerUIPrefab, UIModel.PlayerPanel.transform);
            NewPlayerUIModel newPlayerUIModel = newPlayer.GetComponentInChildren<NewPlayerUIModel>();
            newPlayerUIModel.PlayerTag.text = "Player " + UIModel.NumberOfPlayers;
            newPlayerUIModel.LeftKey.text = keys[0].ToString();
            newPlayerUIModel.RightKey.text = keys[1].ToString();
            UIModel.PlayerSnakeUI playerUI = new UIModel.PlayerSnakeUI()
            {
                leftKey = newPlayerUIModel.LeftKey,
                rightKey = newPlayerUIModel.RightKey,
                playerTag = newPlayerUIModel.PlayerTag,
                snakeSpriteIndex = 0,
                uiRef = newPlayerUIModel
            };
            UIModel.PlayerSnakeUIList.Add(playerUI);
        }

        private void CommandManager_OnChangeSnakeType(ChangeSnakeTypeCommand e)
        {
            for (int index = 0; index < UIModel.PlayerSnakeUIList.Count; index++)
            {
                PlayerSnakeUI snake = UIModel.PlayerSnakeUIList[index];
                if (snake.leftKey.text == e.KeyCode.ToString())
                {
                    snake.snakeSpriteIndex = Mathf.Max(0, snake.snakeSpriteIndex - 1);
                    snake.uiRef.Sprite.texture = UIModel.SnakesSpritesList[snake.snakeSpriteIndex];
                    UIModel.PlayerSnakeUIList[index] = snake;

                }
                else if (snake.rightKey.text == e.KeyCode.ToString())
                {
                    snake.snakeSpriteIndex = Mathf.Min(UIModel.SnakesSpritesList.Count - 1, snake.snakeSpriteIndex + 1);
                    snake.uiRef.Sprite.texture = UIModel.SnakesSpritesList[snake.snakeSpriteIndex];
                    UIModel.PlayerSnakeUIList[index] = snake;
                }
                UIModel.PlayersSnake[index] = (SelectionSnakeMenuEnum) UIModel.PlayerSnakeUIList[index].snakeSpriteIndex;
            }
            Context.CommandManager.InvokeCommand(new UpdatePlayersSpriteCommand(UIModel.PlayersSnake));
        }
    }
}