using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Assets.Scripts.Project.UMVCS.View;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        }

        private void OnDestroy()
        {
            Context.CommandManager.RemoveCommandListener<ToggleStartMenuCommand>(CommandManager_OnToggleStartMenu);
            Context.CommandManager.RemoveCommandListener<AddNewPlayerCommand>(CommandManager_OnAddNewPlayer);
        }

        private void CommandManager_OnToggleStartMenu(ToggleStartMenuCommand e)
        {
            UIModel.StartMenuCanvas.gameObject.SetActive(e.Toggle);
            UIModel.UICamera.gameObject.SetActive(e.Toggle);
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
                playerTag = newPlayerUIModel.PlayerTag
            };
            UIModel.PlayerSnakeUIList.Add(playerUI);
        }
    }
}