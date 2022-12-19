using Architectures.UMVCS.Model;
using Project.Data.Types;
using Project.Snake.UMVCS.Controller;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    public class SaveLoadModel : BaseModel
    {
        [SerializeField]
        private List<SnakePersistence> _snakePersistenceList;

        [SerializeField]
        private List<BlockPersistence> _blockPersistenceList;
    }
}