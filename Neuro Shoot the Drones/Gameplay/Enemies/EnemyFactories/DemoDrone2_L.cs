using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.Patterns;
using Neuro_Shoot_the_Drones.Tweens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories
{
    internal class DemoDrone2_L : EnemyFactory
    {
        public override Enemy Create(Vector2 initialPosition)
        {
            var area = ResolutionData.VisibleGameplayArea;

            var startPos = new Vector2(area.Left, area.Top - 100);

            var enemy = new Enemy(
                texture: Resources.LightDrone,
                textureSourceRect: Resources.LightDrone.Bounds,
                textureScale: Vector2.One / 1.3f,
                initialPosition: startPos,
                health: 8,
                hitCircleSize: 20
            );

            var transform = enemy.GetComponent<TransformComponent>();

            var centerX = area.Left + area.Width;
            var centerY = area.Top;
            var radius = area.Width / 1.35f;

            var duration = 4f;
            var tween = new Tween(float.Pi * 1.2f, float.Pi / 2 * 0.8f, duration, EasingType.Linear);

            tween.OnUpdate += () =>
            {
                float angle = tween.Value;
                transform.Position = new Vector2(
                    centerX + radius * MathF.Cos(angle),
                    centerY + radius * MathF.Sin(angle)
                );
            };

            var pattern = new DemoDrone2TargetedPattern();
            enemy.Shoot(duration / 2.25, pattern);

            enemy.OnInitialized += () =>
            {
                enemy.AddTween(tween);
                tween.Start();
            };

            enemy.Exit(duration + 0.5f);

            return enemy;
        }
    }
}
