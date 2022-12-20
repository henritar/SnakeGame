using Architectures.UMVCS.Model;
using Project.Data.Types;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    [Serializable]
    public class SaveLoadModel : BaseModel
    {
        public List<SnakePersistence> SnakePersistenceList;

        public List<BlockPersistence> BlockPersistenceList;
    }
}