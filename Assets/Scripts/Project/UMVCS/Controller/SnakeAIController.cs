using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Project.Snake.UMVCS.Model;
using Project.Snake.UMVCS.View;

namespace Project.Snake.UMVCS.Controller
{
    public class SnakeAIController : BaseController<SnakeAIModel, SnakeAIView, NullService>
    {
        public SnakeAIView BaseAIView { get => BaseView as SnakeAIView; }
        public SnakeAIModel BaseAIModel { get => BaseModel as SnakeAIModel; }

    }
}