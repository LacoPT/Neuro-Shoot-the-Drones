using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.ExtensionAndHelpers
{
    internal static class CommonEnemyActionsExtensions
    {
        public static void MoveByX(this Enemy enemy, float by, double startOn, float duration, EasingType easing)
        {
            float destination = enemy.Position.X + by;
            MoveToX(enemy, destination, startOn, duration, easing);
        }

        public static void MoveToX(this Enemy enemy, float to, double startOn, float duration, EasingType easing)
        {
            enemy.TimeLine.AddElement(startOn, () =>
            {
                var tween = new Tween(enemy.Position.X,to, duration, easing);
                var oldPosition = enemy.Position;
                tween.OnUpdate += () => enemy.UpdatePosition(oldPosition.WithX(tween.Value));
                enemy.AddTween(tween);
                tween.Start();
            });

        }

        public static void MoveByY(this Enemy enemy, float by, double startOn, float duration, EasingType easing)
        {
            float destination = enemy.Position.Y + by;
            MoveToY(enemy, destination, startOn, duration, easing);
        }
        public static void MoveToY(this Enemy enemy, float to, double startOn, float duration, EasingType easing)
        {
            enemy.TimeLine.AddElement(startOn, () =>
            {
                var tween = new Tween(enemy.Position.Y,to, duration, easing);
                var oldPosition = enemy.Position;
                tween.OnUpdate += () => enemy.UpdatePosition(oldPosition.WithY(tween.Value));
                enemy.AddTween(tween);
                tween.Start();
            });
        }

        //NOTE: !!! DO NOT USE !!!
        //TODO: Fix the bug in TimeLineComponent with being unable to add keys with the same value
        public static void MoveBy(this Enemy enemy, Vector2 by, double startOn, float duration, EasingType easing)
        {
            var destination = enemy.Position + by;
            MoveTo(enemy, destination, startOn, duration, easing);
        }

        //Do not use
        public static void MoveTo(this Enemy enemy, Vector2 to, double startOn, float duration, EasingType easing)
        {
            MoveToX(enemy, to.X, startOn, duration, easing);
            MoveToY(enemy, to.Y, startOn, duration, easing);
        }

        public static void Shoot(this Enemy enemy, double startOn, IBulletHellPattern pattern)
        {
            enemy.TimeLine.AddElement(startOn, () =>
            {
                pattern.UpdatePosition(enemy.Position);
                //TODO: Generate() should take Position as parameter
                pattern.Generate();
            });
        }

        public static void MoveByCurve()
        {
        }
    }
}
