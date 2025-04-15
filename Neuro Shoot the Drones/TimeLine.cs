using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neuro_Shoot_the_Drones
{
    public enum TimeLineState
    {
        NotStarted,
        Running,
        Paused,
        Stopped,
        Finished
    }

    internal class TimeLine
    {
        public double Time { get; private set;  } = 0 ;
        Queue<TimeLineElement> timeLine = new();
        SortedList<double, TimeLineElement> elements = new();
        TimeLineState state = TimeLineState.NotStarted;

        public delegate void OnStartEventHandler();
        public delegate void OnPauseEventHadler();
        public delegate void OnResumeEventHadler();
        public delegate void OnStopEventHaldler();
        public delegate void OnFinishEventHandler();

        public event OnStartEventHandler OnStart;
        public event OnPauseEventHadler OnPause;
        public event OnResumeEventHadler OnResume;
        public event OnStopEventHaldler OnStop;
        public event OnFinishEventHandler OnFinish;

        public void Update(GameTime gameTime)
        {
            if(state != TimeLineState.Running) return;
            if(timeLine.TryPeek(out var element))
            {
                if(Time >= element.Time)
                {
                    timeLine.Dequeue();
                    element.Invoke();
                }
            }
            else
            {
                Finish();
            }
            Time += gameTime.ElapsedGameTime.TotalSeconds;
        }

        public TimeLineElement AddElement(double time, Action action)
        {
            if(state != TimeLineState.NotStarted)
                throw new Exception("Bad timeline usage!");
            var element = new TimeLineElement(time);
            element.OnInvoke += action;
            elements.Add(time, element);
            return element;
        }

        public void Start()
        {
            timeLine = new(elements.Values);
            if (state != TimeLineState.NotStarted)
                return;
            state = TimeLineState.Running;
            OnStart?.Invoke();
        }

        public void Pause()
        {
            if (state != TimeLineState.Running)
                return;
            state = TimeLineState.Paused;
            OnPause?.Invoke();
        }

        public void Resume()
        {
            if (state != TimeLineState.Paused)
                return;
            state = TimeLineState.Running;
            OnResume?.Invoke();
        }
           
        public void Stop()
        {
            state = TimeLineState.Stopped;
            OnStop?.Invoke();
        }

        private void Finish()
        {
            state = TimeLineState.Finished;
            OnFinish?.Invoke();
        }
    }
}
