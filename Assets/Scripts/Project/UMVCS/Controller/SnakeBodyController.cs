using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using System.Collections;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class SnakeBodyController : BaseController<SnakeBodyModel, SnakeBodyView, NullService>
    {
        public SnakeBodyModel SnakeBodyModel { get => BaseModel as SnakeBodyModel; }
        public SnakeBodyView SnakeBodyView { get => BaseView as SnakeBodyView; }
    }
}