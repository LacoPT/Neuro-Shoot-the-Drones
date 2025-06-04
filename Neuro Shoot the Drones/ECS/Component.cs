using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.ECS
{
    //NOTE: Component can exist without entity
    internal abstract class Component
    {
        public readonly BaseEntity Entity;
        public delegate void DestroyedEventHandler();
        public event DestroyedEventHandler OnDestroy;
        public bool Skip = false;

        //NOTE: !!! ALWAYS CALL THE BASE CONTRUCTOR, NO MATTER WHAT !!!
        public Component(BaseEntity entity)
        {
            Entity = entity;
            entity.OnDestroy += Destroy;
        }

        public void Destroy()
        {
            OnDestroy?.Invoke();
        }
    }
}
