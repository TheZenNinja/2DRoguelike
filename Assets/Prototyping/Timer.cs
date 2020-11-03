using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZenUtil
{
    public class TimerMonoHook : MonoBehaviour
    {
        public static TimerMonoHook Create(Action update, GameObject obj = null)
        {
            if (obj == null)
            {
                obj = new GameObject
                {
                    name = "Timer Hook"
                };
            }
            TimerMonoHook hook;
            if (obj.GetComponent<TimerMonoHook>())
            {
                hook = obj.GetComponent<TimerMonoHook>();
                hook.onUpdate += update;
            }
            else
            {
                hook = obj.AddComponent<TimerMonoHook>();
                hook.onUpdate = update;
            }
            return hook;
        }
        public Action onUpdate;

        private void Update() => onUpdate?.Invoke();

        public void Destroy(Action action)
        {
            if (!gameObject || !this)
                return;
            if (onUpdate.GetInvocationList().Length <= 1)
                Destroy(gameObject);
            else
            {
                onUpdate -= action;
            }
        }
    }
    public class OneTimeTimer
    {
        public static OneTimeTimer StartTimer(float length, Action action)
        {
            OneTimeTimer timer = new OneTimeTimer(length, action);
            GameObject g = new GameObject();
            TimerMonoHook hook = g.AddComponent<TimerMonoHook>();
            hook.onUpdate = timer.Update;
            timer.hook = hook;
            return timer;
        }

        public OneTimeTimer(float timerLength, Action onTimeEnd)
        {
            this.timerLength = timerLength;
            this.onTimeEnd = onTimeEnd;
            currentTime = 0;
        }

        public Action onTimeEnd;
        public float timerLength;
        private float currentTime;
        private TimerMonoHook hook;

        public float percent { get { return currentTime / timerLength; } }

        public void Update()
        {
            timerLength += Time.deltaTime;
            if (currentTime <= timerLength)
                Complete();
        }
        public void Complete() => hook.Destroy(onTimeEnd);
    }
    [Serializable]
    public class Timer
    {
        public float timerLength;
        private float currentTime;

        public bool fixedTime = false;
        public bool testing = false;
        public float percent { get { return currentTime / timerLength; } }
        public bool finished { get { return !testing; } }
        public Action onTimeEnd;
        private bool madeHook = false;
        private TimerMonoHook hook;

        public Timer(float length)
        {
            timerLength = length;
        }
        public Timer(float length, Action timeEndAction)
        {
            timerLength = length;
            onTimeEnd = timeEndAction;
        }
        ~Timer()
        {
            DestroyHook();
        }
        public void AttachHookToObj(GameObject obj)
        {
            madeHook = true;
            hook = TimerMonoHook.Create(Update, obj);
        }

        public void Restart()
        {
            if (!madeHook)
                CreateHook();

            testing = true;
            currentTime = timerLength;
        }
        public void Stop()
        {
            currentTime = 0;
            testing = false;
        }
        public void Pause() => testing = false;
        public void Resume() => testing = true;

        public void Update()
        {
            if (testing)
            {
                if (fixedTime)
                    currentTime -= Time.unscaledDeltaTime;
                else
                    currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    currentTime = 0;
                    testing = false;
                    onTimeEnd?.Invoke();
                }
            }
        }

        public void CreateHook()
        {
            madeHook = true;
            hook = TimerMonoHook.Create(Update);
        }
        public void DestroyHook()
        {
            madeHook = false;
            if (hook)
            {
                hook?.Destroy(onTimeEnd);
            }
            hook = null;
        }
        public static implicit operator float(Timer t) => t.currentTime;
    }
}
