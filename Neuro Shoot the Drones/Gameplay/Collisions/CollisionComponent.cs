using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.ECS;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Collisions
{
    //NOTE: To use, you need to update its position from class that has this Component
    internal class CollisionComponent: Component
    {
        public int HitCircleSize { get; private set; }
        public TransformComponent Transform;
        public Vector2 Position => Transform.Position;
        public CollisionData CollisionData { get; private set; }

        /// <summary>
        /// This means what layers this component impacts
        /// </summary>
        public readonly CollisionLayers ImpactLayers;
        /// <summary>
        /// This means what layers this component is affected by, what this collider is
        /// </summary>
        public readonly CollisionLayers AffectedByLayers;

        public delegate void CollisionRegisteredEventHandler(CollisionData collisionData);
        public event CollisionRegisteredEventHandler OnCollisionRegistered;

        public CollisionComponent(int hitCircleSize, CollisionLayers impactLayers, CollisionLayers affectedLayers, CollisionData collisionData, BaseEntity entity)
        : base(entity)
        {
            HitCircleSize = hitCircleSize;
            ImpactLayers = impactLayers;
            AffectedByLayers = affectedLayers;
            CollisionData = collisionData;
            Transform = entity.GetComponent<TransformComponent>();
        }

        public void RegisterCollision(CollisionData data)
        {
            OnCollisionRegistered?.Invoke(data);
        }
    }
}
