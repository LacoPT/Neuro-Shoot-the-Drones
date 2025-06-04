using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.CommonComponents
{
    internal class MoveComponent : Component
    {
        public Vector2 Velocity = Vector2.Zero;
        public Vector2 Acceleration = Vector2.Zero;
        public float AngularSpeed = 0;
        public float AngularAcceleration = 0;
        public TransformComponent Transform;

        public MoveComponent(BaseEntity entity) : base(entity)
        {
            Transform = entity.GetComponent<TransformComponent>();
        }
    }
}
