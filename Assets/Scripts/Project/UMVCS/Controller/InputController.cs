using Architectures.UMVCS.Controller;
using Architectures.UMVCS.Service;
using Architectures.UMVCS.View;
using Assets.Utils.Runtime.Managers;
using Project.Snake.UMVCS.Model;
using Project.UMVCS.Controller.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Snake.UMVCS.Controller
{
    public class InputController : BaseController<InputModel, NullView, NullService>
    {
        public InputModel InputModel { get => BaseModel as InputModel; }

        private void Awake()
        {
            InputModel.KeyCodes = new HashSet<KeyCode>();
            
        }

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
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Context.CommandManager.InvokeCommand(new StartApplicationCommand());
                
            }
            
        }
        private void OnGUI()
        {
            Event e = Event.current;

            //Check the type of the current event, making sure to take in only the KeyDown of the keystroke.
            //char.IsLetter to filter out all other KeyCodes besides alphabetical.
            if (e.type == EventType.KeyDown &&
                 e.keyCode.ToString().Length == 1 &&
                    char.IsLetter(e.keyCode.ToString()[0]))
            {
                if (InputModel.KeyCodes.Count < 2)
                {
                    InputModel.KeyCodes.Add(e.keyCode);
                    //Debug.Log("InputController: Added key code: " + e.keyCode);
                }
                else if (InputModel.KeyCodes.Count == 2)
                {

                    CoroutinerManager.Start(AddKeysCoroutine());
                    //Debug.Log("InputController: AddEventTrigger");

                }
            }
            if (e.type == EventType.KeyUp &&
                 e.keyCode.ToString().Length == 1 &&
                    char.IsLetter(e.keyCode.ToString()[0]))
            {
                
                //Debug.Log("InputController: Removed key code: " + e.keyCode);
                if (InputModel.KeyCodes.Count < 2)
                {
                    //Debug.Log("InputController: RemoveEventTrigger");
                    //REMOVE EVENT
                    if (InputModel.IsCoroutineRunning)
                    {
                        CoroutinerManager.Stop(AddKeysCoroutine());
                        InputModel.IsCoroutineRunning = true;
                    }
                }
                InputModel.KeyCodes.Remove(e.keyCode);
            }
        }


        private IEnumerator AddKeysCoroutine()
        {
            InputModel.IsCoroutineRunning = false;
            yield return CoroutinerManager.WaitOneSecond;
            Context.CommandManager.InvokeCommand(new AddNewPlayerCommand(InputModel.KeyCodes));
            InputModel.IsCoroutineRunning = true;
        }
    }
}