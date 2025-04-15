using Microsoft.Xna.Framework;
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

        public TargetedShotgunPattern(Vector2 position, Vector2 target,  int count = 5, float rate = 0.1f)
        {
            this.Count = count;
            this.Rate = rate;
            this.Target = target;
            this.Position = position;

            Spread = MathF.PI * (1 - MathF.Exp(-Rate * (Count - 1)));
            StartAngle = -Spread / 2;
            Step = Spread / (Count - 1);
            TargetAngle = Position.LookAt(Target);
        }
        public TargetedShotgunPattern(Vector2 position, int count = 5, float rate = 0.1f)
        {
            this.Count = count;
            this.Rate = rate;
            this.Target = BulletHell.PlayerPosition;
            this.Position = position;

            Spread = MathF.PI * (1 - MathF.Exp(-Rate * (Count - 1)));
            StartAngle = -Spread / 2;
            Step = Spread / (Count - 1);
            TargetAngle = Position.LookAt(Target);
        }

        public void Generate()
        {
            for(int i  = 0; i < Count; i++)
            {
                var angle = TargetAngle + StartAngle + Step * i;
                var bullet = EnemyBulletFactory.CreateStandart(Position);
                bullet.Rotation = angle - MathF.PI/2;
                BulletHell.CreateEnemyBullet(bullet);
            }
        }
    }
}
