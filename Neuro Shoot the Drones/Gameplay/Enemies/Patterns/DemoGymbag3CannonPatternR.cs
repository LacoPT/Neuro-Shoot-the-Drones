using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.Bullets;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies.Patterns
{
    internal class DemoGymbag3CannonPatternR : IBulletHellPattern
    {
        Vector2 Position = Vector2.Zero;

        const int count = 3;
        const float initialVelocityY = -300;
        const float accelerationY = 300;
        
        public List<EnemyBullet> Generate()
        {
            var result = new List<EnemyBullet>();
            
            for(int i = 0; i < count; i++)
            {
                var bullet = EnemyBulletFactory.CreateHeavy(Position);

                var transform = bullet.GetComponent<TransformComponent>();
                var move = bullet.GetComponent<MoveComponent>();

                transform.Rotation = 0;
                move.Velocity = Vector2.Zero;
                move.Velocity.X = -((float)i * 0.5f + 0.5f) * bullet.BaseSpeed;
                move.Velocity.Y = initialVelocityY - i * 25;
                move.Acceleration.Y = accelerationY + ( (float)i * 60f);

                result.Add(bullet);
            }

            return result;
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            Position = newPosition;
        }
    }
}
