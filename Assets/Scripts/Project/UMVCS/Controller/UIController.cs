using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class UIController : BaseController<UIModel, UIView, NullService>
    {
        public UIModel UIModel { get => BaseModel as UIModel; }
        public UIView UIView { get => BaseView as UIView; }
    }
}