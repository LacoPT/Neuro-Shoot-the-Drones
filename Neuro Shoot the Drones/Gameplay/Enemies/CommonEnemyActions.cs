using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Timeline;
using Neuro_Shoot_the_Drones.Tweens;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies
{
    //NOTE: It's better to use relative methods (ByX, ByY), because some enemies may have similar behaviour but sligthly different, e.g. start and final pos 
    internal static class CommonEnemyActionsExtensions
    {
        public static void MoveByX(this Enemy enemy, float by, double startOn, float duration, EasingType easing)
        {
            var transform = enemy.GetComponent<TransformComponent>();
            float destination = transform.Position.X + by;
            enemy.MoveToX(destination, startOn, duration, easing);
        }

        public static void MoveToX(this Enemy enemy, float to, double startOn, float duration, EasingType easing)
        {
            var timeLine = enemy.GetComponent<TimeLineComponent>();

            timeLine.AddElement(startOn, () =>
            {
                var transform = enemy.GetComponent<TransformComponent>();    
                var tween = new Tween(transform.Position.X, to, duration, easing);
                tween.OnUpdate += () => transform.Position.X = (tween.Value);
                enemy.AddTween(tween);
                tween.Start();
            });

        }

        public static void MoveByY(this Enemy enemy, float by, double startOn, float duration, EasingType easing)
        {
            var transform = enemy.GetComponent<TransformComponent>();
            float destination = transform.Position.Y + by;
            enemy.MoveToY(destination, startOn, duration, easing);
        }
        public static void MoveToY(this Enemy enemy, float to, double startOn, float duration, EasingType easing)
        {
            var timeLine = enemy.GetComponent<TimeLineComponent>();
            timeLine.AddElement(startOn, () =>
            {
                var transform = enemy.GetComponent<TransformComponent>();
                var tween = new Tween(transform.Position.Y, to, duration, easing);
                var oldPosition = transform.Position;
                tween.OnUpdate += () => transform.Position.Y = (tween.Value);
                enemy.AddTween(tween);
                tween.Start();
            });
        }

        public static void Shoot(this  Enemy enemy, double time, IBulletHellPattern pattern)
        {
            var timeLine = enemy.GetComponent<TimeLineComponent>();

            timeLine.AddElement(time, () =>
            {
                var transform = enemy.GetComponent<TransformComponent>();
                pattern.UpdatePosition(transform.Position);
                enemy.ShotPattern(pattern.Generate());
            });
        }
    }
}
