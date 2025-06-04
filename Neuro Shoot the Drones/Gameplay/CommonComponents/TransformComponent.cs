using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.CommonComponents
{
    internal class TransformComponent : Component
    {
        public Vector2 Position = new();
        public float Rotation = 0f;

        public TransformComponent(BaseEntity entity) : base(entity)
        {
        }
    }
}
