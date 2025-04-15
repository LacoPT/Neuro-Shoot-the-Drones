using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal class TimeLineElement
    {
        public readonly double Time;

        public TimeLineElement(double time)
        {
            Time = time;
        }

        public event Action OnInvoke;

        public void Invoke() 
        {
            OnInvoke?.Invoke();
        }
    }
}
