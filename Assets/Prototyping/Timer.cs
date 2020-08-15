using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZenClasses
{
    [Serializable]
    public class Timer
    {
        public float timerLength;
        private float currentTime;

        private bool testing = false;
        public float percent { get { return currentTime / timerLength; } }
        public bool finished { get { return !testing; } }
        public Action onTimeEnd;

        public void Start()
        {
            testing = true;
            currentTime = timerLength;
        }
        public void Restart() => Start();

        public void Stop()
        {
            currentTime = 0;
            testing = false;
        }
        public void Pause()
        {
            testing = false;
        }

        public void Update(float timeDelta)
        {
            if (testing)
            {
                currentTime -= timeDelta;
                if (currentTime <= 0)
                {
                    currentTime = 0;
                    testing = false;
                    onTimeEnd?.Invoke();
                }
            }
        }

        public static implicit operator float(Timer t) => t.currentTime;
    }
}
