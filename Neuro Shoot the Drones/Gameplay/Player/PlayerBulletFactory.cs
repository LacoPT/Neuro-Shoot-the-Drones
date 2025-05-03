using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal static class PlayerBulletFactory
    {
        public static PlayerBullet CreateStandartPlayerBullet(Vector2 position)
        {
            return new PlayerBullet(
                texture: Resources.BulletTextureAtlas,
                textureSourceRect: new(2, 129, 14, 16),
                textureScale: Vector2.One,
                hitCircleSize: 10,
                position: position,
                damage: 1,
                hitLimit: 1,
                baseSpeed: 1200,
                acceleration: 0);
        }
    }
}
