using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay.Drawable;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories
{
    internal class DemoGymbag3_R : EnemyFactory
    {
        public override Enemy Create(Vector2 initialPosition)
        {
            var offset = 100f;

            var enemy = new Enemy(Resources.Gymbag, Resources.Gymbag.Bounds, Vector2.One / 2.6f, initialPosition, health: 25, hitCircleSize: 25);

            enemy.MoveByX(-offset, 0, 1, EasingType.CubicEaseOut);

            enemy.MoveByX(offset, 3, 1, EasingType.CubicEaseIn);

            var pattern = new DemoGymbag3CannonPatternR();
            enemy.Shoot(2, pattern);
                
            enemy.Exit(4.5);

            return enemy;
        }
    }
}
