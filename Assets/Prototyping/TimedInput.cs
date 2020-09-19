using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZenUtil
{
    [Serializable]
    public class TimedInput
    {
        public KeyCode key;
        public float timeWindow = 0.2f;

        private bool testing;
        private float timer;

        public Action tapAction, holdAction, prereleaseAction;

        public void SetupTapOnly(Action tap)
        {
            tapAction = tap;
            TimerMonoHook.Create(Update);
        }
        public void Setup(Action tap, Action hold)
        {
            tapAction = tap;
            holdAction = hold;
            TimerMonoHook.Create(Update);
        }
        public void Setup(Action tap, Action hold, Action release)
        {
            tapAction = tap;
            holdAction = hold;
            prereleaseAction = release;
            TimerMonoHook.Create(Update);
        }

        public void Update()
        {
            if (testing)
                timer += Time.deltaTime;

            if (testing && timer >= timeWindow)
            {
                holdAction?.Invoke();
                testing = false;
            }
            else if (Input.GetKeyUp(key))
            {
                if (testing)
                    tapAction?.Invoke();
                else
                    prereleaseAction?.Invoke();
                testing = false;
            }
            else if (Input.GetKeyDown(key))
            {
                testing = true;
                timer = 0;
            }
        }
    }

}
