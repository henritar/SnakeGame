using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Model;
using Architectures.UMVCS.Service;
using Architectures.UMVCS.View;
using Project.UMVCS.Controller.Commands;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class InputController : BaseController<NullModel, NullView, NullService>
    {
        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Context.CommandManager.InvokeCommand(new RestartApplicationCommand());
            }

            Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            if (!dir.Equals(Vector3.zero))
            {
                Context.CommandManager.InvokeCommand(new ChangeSnakeDirectionCommand(dir));
            }
            
        }
    }
}