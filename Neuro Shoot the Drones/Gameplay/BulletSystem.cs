using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay
{
    internal class BulletSystem
    {

        List<BaseBullet> Bullets = new();
        List<BaseBullet> BulletsToRemove = new();

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            foreach (var b in Bullets)
            {
                b.Draw(gameTime, sb);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(var b in BulletsToRemove)
            {
                Bullets.Remove(b);
            }
            BulletsToRemove.Clear();
            foreach(var b in Bullets)
            {
                b.Update(gameTime);
            }
        }


        //NOTE: The reason behind why this two methods are different is because EnemyBullets are mostly created in patterns
        //while PlayerBullets are more simple, but maybe later this will be rewritten into one method,
        //because player bullets are also can be different
        public void CreateEnemyBullet(EnemyBullet bullet)
        {
            Bullets.Add(bullet);
            bullet.OnDestroy += () => BulletsToRemove.Add(bullet);
            bullet.Initialize();
        }

        public PlayerBullet CreatePlayerBullet(Vector2 Position)
        {
            var bullet = PlayerBulletFactory.CreateStandartPlayerBullet(Position);
            Bullets.Add(bullet);
            bullet.OnDestroy += () => BulletsToRemove.Add(bullet);
            bullet.Initialize();
            return bullet;
        }
    }
}
