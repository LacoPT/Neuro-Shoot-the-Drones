using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies
{
    internal static class EnemyBulletFactory
    {
        public static EnemyBullet CreateStandart(Vector2 position)
        {
            return new EnemyBullet(textureSourceRect: new(1, 49, 16, 16),
                                    textureScale: Vector2.One,
                                    position: position);
        }
    }
}
