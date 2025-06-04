using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.ECS;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Gameplay.Drawable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay
{
    internal abstract class GameEntity : BaseEntity
    {
        public delegate void InitializedEventHalder();
        public event InitializedEventHalder OnInitialized;

        public GameEntity(Vector2 startPosition, float initialRotation = 0)
        {
            var translation = new TransformComponent(this);
            translation.Position = startPosition;
            translation.Rotation = initialRotation;
            AddComponent(translation);
        }

        //Template method
        public void Initialize()
        {
            OnInitilize();

            var translation = GetComponent<TransformComponent>();
            var collision = GetComponent<CollisionComponent>();
            var drawable = GetComponent<Drawable.DrawableComponent>();
            if (collision == null || drawable == null)
                throw new Exception("Either collision or drawable component is not set");
            collision.Transform = translation;
            drawable.Transform = translation;
            OnInitialized?.Invoke();
        }
        protected virtual void OnInitilize()
        {
        }
    }
}
