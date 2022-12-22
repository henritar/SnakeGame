using DesignPatterns.Singleton;
using System.Collections;
using UnityEngine;

namespace Assets.Utils.Runtime.Managers
{
    public class CoroutinerManager : SingletonMonoBehaviour<CoroutinerManager>
    {
        public static WaitForSeconds WaitDotFiveSecond = new WaitForSeconds(0.5f);
        public static WaitForSeconds WaitOneSecond = new WaitForSeconds(1);
        public static WaitForSeconds WaitTwoSeconds = new WaitForSeconds(2);
        public static WaitForSeconds WaitFiveSeconds = new WaitForSeconds(5);

        public static Coroutine Start(IEnumerator routine)
        { return Instance.StartCoroutine(routine); }

        public static void Stop(IEnumerator routine)
        { Instance.StopCoroutine(routine); }
        public static void Stop(Coroutine routine)
        { Instance.StopCoroutine(routine); }
        public static void StopAll()
        { Instance.StopAllCoroutines(); }
    }
}