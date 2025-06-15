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
    internal class DemoDrone2TargetedPattern : IBulletHellPattern
    {
        const int count = 3;
        const float speedBonus = 100;
        Vector2 Position = Vector2.Zero;

        public List<EnemyBullet> Generate()
        {
            var result = new List<EnemyBullet>();
            var rotation = Position.LookAt(PlayerPosAccess.Get());

            for(int i = 0; i < count; i++)
            {
                var bullet = EnemyBulletFactory.CreateStandart(Position);
                var transform = bullet.GetComponent<TransformComponent>();
                var move = bullet.GetComponent <MoveComponent>();

                transform.Rotation = rotation + float.Pi / 2;
                move.Velocity.Y -= speedBonus * i;

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
