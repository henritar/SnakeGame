using Commands;
using Project.Data.Types;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UMVCS.Controller.Commands
{
    public class UpdatePlayersSpriteCommand : Command
    {
        [SerializeField]
        private List<SelectionSnakeMenuEnum> _playersSnake = new List<SelectionSnakeMenuEnum>();

        public List<SelectionSnakeMenuEnum> PlayersSnake { get => _playersSnake;  }

        public UpdatePlayersSpriteCommand(List<SelectionSnakeMenuEnum> playersSnake)
        {
            _playersSnake = playersSnake;
        }
    }
}