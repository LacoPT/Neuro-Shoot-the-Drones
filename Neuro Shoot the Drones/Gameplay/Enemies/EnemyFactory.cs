using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.Patterns;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies
{
    internal static class EnemyFactory
    {
        public static Enemy CreateSimpleDrone()
        {
            Vector2 targetPosition = GlobalVariables.VisibleGameplayArea.Center.ToVector2();
            Vector2 targetPosition2 = targetPosition.WithX(1300);
            Vector2 initialPosition = new(targetPosition.X, -150);
            Enemy drone = new Enemy(100, 15, initialPosition, Resources.Drone, Resources.Drone.Bounds, Vector2.One);
            drone.TimeLine.AddElement(3, () =>
            {
                //moves enemy from top to the center of screen
                Tween<double> tween = new(initialPosition.Y, targetPosition.Y, 2, EasingType.CubicEaseOut);
                var oldPos = drone.Position;
                tween.OnUpdate += () => drone.UpdatePosition(oldPos.WithY((float)tween.Value));
                drone.AddTween(tween);
                tween.Start();
            });

            drone.TimeLine.AddElement(6.1, () =>
            {
                var pattern = new TargetedShotgunPattern(drone.Position, 3, 0.1f);
                pattern.Generate();
            });

            drone.TimeLine.AddElement(6.4, () =>
            {
                var pattern = new TargetedShotgunPattern(drone.Position, 4, 0.1f);
                pattern.Generate();
            });
            
            drone.TimeLine.AddElement(6.7, () =>
            {
                var pattern = new TargetedShotgunPattern(drone.Position, 5, 0.1f);
                pattern.Generate();
            });
            
            drone.TimeLine.AddElement(7, () =>
            {
                var pattern = new TargetedShotgunPattern(drone.Position, 6, 0.1f);
                pattern.Generate();
            });

            
            drone.TimeLine.AddElement(5.7, () =>
            {
                Tween<double> tween = new(drone.Position.X, targetPosition2.X, 3, EasingType.QuarticEaseInOut);
                var oldPos = drone.Position;
                tween.OnUpdate += () => drone.UpdatePosition(oldPos.WithX((float)tween.Value));
                drone.AddTween(tween);
                tween.Start();
            });
            return drone;
        }
    }
}
