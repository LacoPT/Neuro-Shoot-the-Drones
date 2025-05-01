using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories
{
    internal class Drone0 : EnemyFactory
    {
        public override Enemy Create(Vector2 initialPosition)
        {
            var targetPosY = 500;
            var drone = new Enemy(15, 15, initialPosition, Resources.Drone, Resources.Drone.Bounds, Vector2.One / 2);
                drone.MoveByY(targetPosY, 0, 3, EasingType.CubicEaseOut);
                drone.Shoot(3.1, new SimpleCircularPattern(16, Vector2.Zero, 0));
                drone.MoveToX(GlobalVariables.VisibleGameplayArea.Center.X + GlobalVariables.VisibleGameplayArea.Width / 2 + 100, 3.2, 3, EasingType.CubicEaseInOut);
            return drone;
        }
    }
}
