using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Timeline;
using Neuro_Shoot_the_Drones.Tweens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories
{
    internal class DemoDrone4_L : EnemyFactory
    {
        const float radius = 250f;
        const int loops = 3;
        const float loopDuration = 3.4f;
        public override Enemy Create(Vector2 initialPosition)
        {
            var area = ResolutionData.VisibleGameplayArea;
            var center = area.Center.ToVector2() + new Vector2(0, -150);

            var enemy = new Enemy(
                texture: Resources.Drone,
                textureSourceRect: Resources.Drone.Bounds,
                textureScale: Vector2.One / 1.6f,
                initialPosition: initialPosition,
                health: 15,
                hitCircleSize: 20
            );

            var transform = enemy.GetComponent<TransformComponent>();
            var timeline = enemy.GetComponent<TimeLineComponent>();

            enemy.MoveToY(center.Y, 0, 1.4f, EasingType.CubicEaseOut);
            enemy.MoveToX(center.X - radius, 0, 1.4f, EasingType.CubicEaseOut);

            var tween = new Tween(float.Pi, -float.Pi - float.Pi * 2 * (loops - 1) - float.Pi, loopDuration * loops);

            tween.OnUpdate += () =>
            {
                var angle = tween.Value;
                transform.Position.X = center.X + MathF.Cos(angle) * radius;
                transform.Position.Y = center.Y + MathF.Sin(angle) * radius;

            };

            timeline.AddElement(1.4f, () =>
            {
                enemy.AddTween(tween);
                tween.Start();
            });

            enemy.MoveByY(-100, 1.4f + loopDuration * loops, 1.4f, EasingType.CubicEaseOut);
            enemy.MoveByX(200, 1.4f + loopDuration * loops, 1.4f, EasingType.CubicEaseOut);

            enemy.Exit(1.4f + loopDuration * loops + 0.5f);

            return enemy;
        }
    }
}
