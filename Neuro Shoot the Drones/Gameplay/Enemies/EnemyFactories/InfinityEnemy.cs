using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Tweens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories
{
    internal class InfinityEnemy : EnemyFactory
    {
        public override Enemy Create(Vector2 initialPosition)
        {
            var amplitude = 250f;
            var duration = 4f;
            var leanAngle = 0.3f;

            var enemy = new Enemy(
                Resources.LightDrone,
                Resources.LightDrone.Bounds,
                Vector2.One / 1.3f,
                initialPosition,
                health: 50,
                hitCircleSize: 20
            );

            var transform = enemy.GetComponent<TransformComponent>();

            var tween = new Tween(0, float.Pi * 2, duration, EasingType.Linear);
            tween.DestroyOnEnd = false;
            tween.OnFinish += () =>
            {
                tween.Reset();
                tween.Start();
            };

            enemy.OnDestroy += () =>
            {
                tween.Destroy();
            };

            tween.OnUpdate += () =>
            {
                float t = tween.Value;

                int loopCount = (int)(t / (2 * MathF.PI));
                float rotationAngle = (loopCount % 2 == 0) ? leanAngle : -leanAngle;

                float xRel = amplitude * MathF.Sin(t);
                float yRel = (amplitude / 2f) * MathF.Sin(2 * t);

                float cosA = MathF.Cos(rotationAngle);
                float sinA = MathF.Sin(rotationAngle);

                float xRot = xRel * cosA - yRel * sinA;
                float yRot = xRel * sinA + yRel * cosA;

                transform.Position = initialPosition + new Vector2(xRot, yRot);
            };

            enemy.OnInitialized += () =>
            {
                enemy.AddTween(tween);
                tween.Start();
            };

            return enemy;
        }
    }
}
