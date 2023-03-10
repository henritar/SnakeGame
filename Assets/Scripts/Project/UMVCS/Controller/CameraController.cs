using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;

namespace Project.Snake.UMVCS.Controller
{
    public class CameraController : BaseController<CameraModel, CameraView, NullService>
    {
        public CameraModel CameraModel { get => BaseModel as CameraModel; }
        public CameraView CameraView { get => BaseView as CameraView; }

    }
}