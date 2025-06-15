using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories
{
    internal class DemoDrone1 : EnemyFactory
    {
        readonly Vector2 offset = new Vector2(0, 300);
        public override Enemy Create(Vector2 initialPosition)
        {
            var drone = new Enemy(Resources.LightDrone, Resources.LightDrone.Bounds, Vector2.One / 1.3f, initialPosition, health: 10, hitCircleSize: 15);

            var targetPos = initialPosition;

            var pattern = new TargetedShotgunPattern(initialPosition + offset, 4, 0.2f);

            drone.MoveByY(offset.Y, 0, 1.7f, EasingType.CircularEaseOut);
            drone.Shoot(1.4, pattern);
            drone.MoveByY(-offset.Y, 4, 1.4f, EasingType.CircularEaseIn);
            drone.Exit(6);

            return drone;
        }
    }
}
