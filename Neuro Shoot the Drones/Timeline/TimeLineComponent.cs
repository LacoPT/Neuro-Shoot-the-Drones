﻿using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.ECS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neuro_Shoot_the_Drones.Timeline
{
    public enum TimeLineState
    {
        NotStarted,
        Running,
        Paused,
        Stopped,
        Finished
    }

    //TODO: ECS MIGRATION NOTE: Make an actual Component
    internal class TimeLineComponent : Component
    {
        //NOTE: This exists so timeline could actually exist without entity, for example in levels
        static BaseEntity DUMMY = new BaseEntity();
        public double Time { get; set; } = 0;
        public Queue<TimeLineElement> TimeLine { get; private set; } = new();
        SortedList<double, TimeLineElement> elements = new();
        public TimeLineState State { get; private set; } = TimeLineState.NotStarted;


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

        public TimeLineComponent(BaseEntity entity) : base(entity)
        {
        }

        public TimeLineComponent() : base(DUMMY)
        { }

        public void Update(GameTime gameTime)
        {
            if (State != TimeLineState.Running) return;
            if (TimeLine.TryPeek(out var element))
            {
                if (Time >= element.Time)
                {
                    TimeLine.Dequeue();
                    element.Invoke();
                }
            }
            else
            {
                Finish();
            }
            Time += gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void AddElement(double time, Action action)
        {
            if (State != TimeLineState.NotStarted)
                throw new Exception("Bad timeline usage!");
            if (!elements.ContainsKey(time))
            {
                var element = new TimeLineElement(time);
                element.OnInvoke += action;
                elements.Add(time, element);
            }
            else
            {
                var element = elements[time];
                element.OnInvoke += action;
            }
        }

        public void Start()
        {
            if (State != TimeLineState.NotStarted)
                return;
            TimeLine = new(elements.Values);
            State = TimeLineState.Running;
            OnStart?.Invoke();
        }

        public void Pause()
        {
            if (State != TimeLineState.Running)
                return;
            State = TimeLineState.Paused;
            OnPause?.Invoke();
        }

        public void Resume()
        {
            if (State != TimeLineState.Paused)
                return;
            State = TimeLineState.Running;
            OnResume?.Invoke();
        }

        public void Stop()
        {
            State = TimeLineState.Stopped;
            OnStop?.Invoke();
        }

        public void Finish()
        {
            State = TimeLineState.Finished;
            OnFinish?.Invoke();
        }

        public void Restart()
        {
            Time = 0;
            TimeLine = new(elements.Values);
            State = TimeLineState.Running;
            OnStart?.Invoke();
        }
    }
}
