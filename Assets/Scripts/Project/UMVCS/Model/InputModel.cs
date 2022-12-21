using Architectures.UMVCS.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Snake.UMVCS.Model
{
    public class InputModel : BaseModel
    {
        public HashSet<KeyCode> KeyCodes = new HashSet<KeyCode>();

        public bool IsCoroutineRunning;

    }
}