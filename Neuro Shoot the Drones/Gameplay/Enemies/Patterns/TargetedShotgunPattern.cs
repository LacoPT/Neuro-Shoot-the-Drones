using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.Bullets;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies.Patterns
{
    internal class TargetedShotgunPattern: IBulletHellPattern
    {
        int Count = 5;
        float Rate = 0.1f;
        Vector2 Target;
        Vector2 Position;

        float TargetAngle;
        float Spread;
        float StartAngle;
        float Step;

        public TargetedShotgunPattern(Vector2 position, int count = 5, float rate = 0.1f)
        {
            this.Count = count;
            this.Rate = rate;
            this.Position = position;

            Spread = MathF.PI * (1 - MathF.Exp(-Rate * (Count - 1)));
            StartAngle = -Spread / 2;
            Step = Spread / (Count - 1);
            TargetAngle = Position.LookAt(Target);
        }

        public List<EnemyBullet> Generate()
        {
            var result = new List<EnemyBullet>();
            for(int i  = 0; i < Count; i++)
            {
                var angle = TargetAngle + StartAngle + Step * i;
                var b = EnemyBulletFactory.CreateStandart(Position);
                var transfrom = b.GetComponent<TransformComponent>();
                transfrom.Rotation = angle - MathF.PI / 8;
                result.Add(b);
            }
            return result;
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            Position = newPosition;
            TargetAngle = Position.LookAt(Target);
        }

        public void UpdateTargetPosition(Vector2 newPosition)
        {
            Target = newPosition;
        }
    }
}
