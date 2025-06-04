using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.CommonComponents
{
    internal class MoveSystem : BaseSystem
    {
        public MoveSystem() : base(typeof(MoveComponent))
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (MoveComponent component in Components)
            {
                component.AngularSpeed += dt * component.AngularAcceleration;
                component.Transform.Rotation += dt * component.AngularSpeed;
                component.Velocity += dt * component.Acceleration;
                if(component.Transform.Rotation != 0)
                    component.Transform.Position += dt * component.Velocity.Rotated(component.Transform.Rotation);
                else
                    component.Transform.Position += dt * component.Velocity;
            }
        }
    }
}
