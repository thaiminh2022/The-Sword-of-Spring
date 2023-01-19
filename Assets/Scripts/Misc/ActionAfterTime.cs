using UnityEngine;
using System;

namespace TheSwordOfSpring.Misc
{
    public class ActionAfterTime : MonoBehaviour
    {
        float waitTime;
        Action action;

        private void Init()
        {
            Invoke(nameof(DoAction), waitTime);
        }

        private void DoAction()
        {
            action?.Invoke();
            Destroy(gameObject, .1f);
        }

        public static void Create(float waitTime, Action action)
        {

            GameObject go = new GameObject("Timer la la");
            ActionAfterTime actionAfterTime = go.AddComponent<ActionAfterTime>();
            actionAfterTime.waitTime = waitTime;
            actionAfterTime.action = action;

            actionAfterTime.Init();
        }
        public static void AddToObject(GameObject obj, float waitTime, Action action)
        {
            ActionAfterTime actionAfterTime = obj.AddComponent<ActionAfterTime>();
            actionAfterTime.waitTime = waitTime;
            actionAfterTime.action = action;

            actionAfterTime.Init();
        }
    }
}

