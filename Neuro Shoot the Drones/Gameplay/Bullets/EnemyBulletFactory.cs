using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Bullets
{
    //TODO: Rewrite with EnemyBulletID or so
    internal static class EnemyBulletFactory
    {
        public static EnemyBullet CreateStandart(Vector2 position)
        {
            return new EnemyBullet(texture: Resources.BulletTextureAtlas,
                                   textureSourceRect: new(1, 49, 16, 16),
                                   textureScale: Vector2.One,
                                   position: position,
                                   baseSpeed: 500,
                                   acceleration: 0,
                                   rotationSpeed: 0,
                                   rotationAcceleration: 0,
                                   hitCircleSize: 8
                                   );
        }
    }
}
