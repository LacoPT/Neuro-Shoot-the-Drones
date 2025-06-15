using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Tweens
{
    internal class Tween
    {
        public bool DestroyOnEnd = true;
        //TODO: Make with state enum
        public bool IsFinished = false;
        public bool IsStarted = false;
        public bool IsPaused = false;
        public delegate void FinishEventHandler();
        public delegate void UpdateEventHandler();
        public delegate void StartEventHandler();
        public delegate void DestroyedEventHandler();
        public event StartEventHandler OnStart;
        public event FinishEventHandler OnFinish;
        public event UpdateEventHandler OnUpdate;
        public event DestroyedEventHandler OnDestroy;

        public EasingType EasingType;
        float StartValue { get; set; }
        float EndValue { get; set; }
        public float Value { get; private set; }
        public double Timer { get; private set; } = 0;
        readonly double EndTime = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="endTime"></param>
        /// <param name="easingType"></param>
        /// <param name="destroyOnFinish">Is needed for TweenSystem, CAREFUL, if set to false, you need to manualy Destroy it</param>
        public Tween(float startValue, float endValue, double endTime = 1, EasingType easingType = EasingType.Linear, bool destroyOnFinish = true)
        {
            StartValue = startValue;
            EndValue = endValue;
            EndTime = endTime;
            EasingType = easingType;
            Reset();
        }

        public void Update(GameTime gameTime)
        {
            if (IsFinished || IsPaused || !IsStarted) return;
            Timer += gameTime.ElapsedGameTime.TotalSeconds;
            if (Timer >= EndTime)
            {
                IsFinished = true;
                OnFinish?.Invoke();
                Timer = EndTime;
            }
            Value = StartValue + (EndValue - StartValue) * Easings.Interpolate((float)(Timer / EndTime), EasingType);
            OnUpdate?.Invoke();
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
            OnFinish?.Invoke();
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
            OnFinish?.Invoke();
        }

        public void Destroy()
        {
            OnDestroy?.Invoke();
        }
    }
}
