using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.Patterns;
using Neuro_Shoot_the_Drones.Tweens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories
{
    internal class DemoMinawan5_L : EnemyFactory
    {
        public override Enemy Create(Vector2 initialPosition)
        {
            var targetPosX = ResolutionData.VisibleGameplayArea.Right + 100;
            var initialPositionY = initialPosition.Y;
            var amplitude = 75;
            var duration = 3.5f;
            var jumps = 5;

            var enemy = new Enemy(Resources.Minawan, Resources.Minawan.Bounds, Vector2.One / 5, initialPosition, health: 6, hitCircleSize: 20);
            var transform = enemy.GetComponent<TransformComponent>();

            enemy.MoveToX(targetPosX, 0, duration, EasingType.Linear);
            var tween = new Tween(0, float.Pi * jumps, duration, EasingType.Linear);

            tween.OnUpdate += () => transform.Position.Y = initialPosition.Y - amplitude * MathF.Abs(MathF.Sin(tween.Value));

            for (int i = 0; i < 5; i++)
            {
                var pattern = new SimpleCircularPattern(7, initialPosition, MathF.PI * 2f / 5f * (float)i);

                enemy.Shoot((float)i * duration / jumps, pattern);
            }

            enemy.OnInitialized += () =>
            {
                enemy.AddTween(tween);
                tween.Start();
            };

            enemy.Exit(duration + 1.5f);

            return enemy;
        }
    }
}
