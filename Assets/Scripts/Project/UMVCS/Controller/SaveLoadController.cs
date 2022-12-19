using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Architectures.UMVCS.View;
using Project.Snake.UMVCS.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class SaveLoadController : BaseController<SaveLoadModel, NullView, NullService>
    {
        public SaveLoadModel SaveLoadModel { get => BaseModel as SaveLoadModel; }
    }
}