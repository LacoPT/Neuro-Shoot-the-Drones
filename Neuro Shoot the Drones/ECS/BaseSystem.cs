using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.ECS
{
    internal abstract class BaseSystem
    {
        protected readonly Type ComponentSystemType;
        protected readonly List<Component> Components = new();
        protected readonly List<Component> ComponentsToRemove = new();
        protected readonly List<Component> ComponentsToAdd = new();

        protected BaseSystem(Type componentSystemType)
        {
            ComponentSystemType = componentSystemType;
        }

        //Template method
        public void Update(GameTime gameTime)
        {
            foreach (var component in ComponentsToRemove)
            {
                Components.Remove(component);
            }
            ComponentsToRemove.Clear();

            OnUpdate(gameTime);

            foreach (var component in ComponentsToAdd)
            {
                Components.Add(component);
                component.OnDestroy += () => RemoveComponent(component);

                OnAddComponent(component);
            }
            ComponentsToAdd.Clear();
        }

        public abstract void OnUpdate(GameTime gameTime);

        //TemplateMethod
        public void AddComponent(Component component)
        {
            if (component.GetType() != ComponentSystemType)
                throw new ArgumentException($"The component must be of type {ComponentSystemType.ToString()}");
            if(!Components.Contains(component))
                ComponentsToAdd.Add(component);
        }
        public virtual void OnAddComponent(Component component)
        {
        }

        //NOTE: Not recommended to use manually
        //Template method
        public void RemoveComponent(Component component)
        {
            if (!Components.Contains(component))
                return;
            ComponentsToRemove.Add(component);

            OnRemoveComponent(component);
        }

        public virtual void OnRemoveComponent(Component component)
        {
        }
    }
}
