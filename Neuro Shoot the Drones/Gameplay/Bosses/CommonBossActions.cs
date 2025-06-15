using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Gameplay.Enemies;
using Neuro_Shoot_the_Drones.Timeline;
using Neuro_Shoot_the_Drones.Tweens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Bosses
{
    internal static class CommonBossActions
    {
        public static void MoveByX(this Boss boss, float by, double startOn, float duration, EasingType easing, TimeLineComponent timeLine)
        {
            var transform = boss.GetComponent<TransformComponent>();
            float destination = transform.Position.X + by;
            boss.MoveToX(destination, startOn, duration, easing, timeLine);
        }

        public static void MoveToX(this Boss boss, float to, double startOn, float duration, EasingType easing, TimeLineComponent timeLine)
        {
            timeLine.AddElement(startOn, () =>
            {
                var transform = boss.GetComponent<TransformComponent>();
                var tween = new Tween(transform.Position.X, to, duration, easing);
                tween.OnUpdate += () => transform.Position.X = (tween.Value);
                boss.AddTween(tween);
                tween.Start();
            });

        }

        public static void MoveByY(this Boss boss, float by, double startOn, float duration, EasingType easing, TimeLineComponent timeLine)
        {
            var transform = boss.GetComponent<TransformComponent>();
            float destination = transform.Position.Y + by;
            boss.MoveToY(destination, startOn, duration, easing, timeLine);
        }

        public static void MoveToY(this Boss boss, float to, double startOn, float duration, EasingType easing, TimeLineComponent timeLine)
        {
            timeLine.AddElement(startOn, () =>
            {
                var transform = boss.GetComponent<TransformComponent>();
                var tween = new Tween(transform.Position.Y, to, duration, easing);
                var oldPosition = transform.Position;
                tween.OnUpdate += () => transform.Position.Y = (tween.Value);
                boss.AddTween(tween);
                tween.Start();
            });
        }

        public static void Shoot(this Boss boss, double time, IBulletHellPattern pattern)
        {
            var timeLine = boss.GetComponent<TimeLineComponent>();

            timeLine.AddElement(time, () =>
            {
                var transform = boss.GetComponent<TransformComponent>();
                pattern.UpdatePosition(transform.Position);
                boss.ShotPattern(pattern);
            });
        }
    }
}
