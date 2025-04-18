﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    //TODO: Rewrite this shit
    internal class Tween<T> where T : System.Numerics.IMultiplyOperators<T, double, T>, System.Numerics.IAdditionOperators<T, T, T>, System.Numerics.ISubtractionOperators<T, T, T>
    { 
        public bool IsFinished = false;
        public bool IsStarted = false;
        public bool IsPaused = false;
        public delegate void FinishHandler();
        public delegate void UpdateHandler();
        public delegate void StartHandler();
        public event StartHandler OnStart;
        public event FinishHandler OnFinish;
        public event UpdateHandler OnUpdate;

        public EasingType EasingType;
        T StartValue { get; set; }
        T EndValue { get; set; }
        public T Value { get; private set; }
        public double Timer = 0;
        double EndTime = 0;

        public Tween(T startValue, T endValue, double endTime = 1, EasingType easingType = EasingType.Linear)
        {
            StartValue = startValue;
            EndValue = endValue;
            EndTime = endTime;
            EasingType = easingType;
            Reset();
        }
        public void Update(GameTime gameTime)
        {
            if(IsFinished || IsPaused || !IsStarted) return;
            Timer += gameTime.ElapsedGameTime.TotalSeconds;
            if(Timer >= EndTime)
            {
                IsFinished = true;
                OnFinish();
                Timer = EndTime;
            }
            Value = StartValue + (EndValue - StartValue) * Easings.Interpolate((float)(Timer / EndTime), EasingType);
            OnUpdate();
        }
        public void Reset()
        {
            Value = StartValue;
            Timer = 0;
            IsStarted = false;
            IsFinished = false;
        }
        public void Start()
        {
            IsStarted = true;
            OnStart?.Invoke();
        }
        
        public void Interrupt()
        {
            IsFinished = true;
            OnFinish();
        }
        
        public void Pause()
        {
            if (!IsPaused)
                IsPaused = true;
        }
        
        public void Resume()
        {
            if (IsPaused)
                IsPaused = false;
        }

        public void Skip()
        {
            Value = EndValue;
            OnUpdate();
            IsFinished = true;
            OnFinish();
        }
    }
}
