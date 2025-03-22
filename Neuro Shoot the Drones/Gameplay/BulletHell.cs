using Microsoft.Extensions.ObjectPool;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal static class BulletHell
    {
        static List<PlayerBullet> PlayerBullets = new List<PlayerBullet>();
        static List<PlayerBullet> BulletsToRemove = new();
        static List<EnemyBullet> EnemyBullets = new();

        public static void Draw(GameTime gameTime, SpriteBatch sb)
        {
            Parallel.ForEach(PlayerBullets, (PlayerBullet b) =>
            {
                if(b.IsActive)
                    b.Draw(gameTime, sb);
            });
        }

        public static void Initialize()
        {
            PlayerBullets.Clear();
        }

        public static void Update(GameTime gameTime)
        {
            foreach(PlayerBullet b in BulletsToRemove)
            {
                PlayerBullets.Remove(b);
            }
            BulletsToRemove.Clear();
            foreach(var bullet in PlayerBullets)
            {
                bullet.Update(gameTime);
            }
        }

        public static void CreateEnemyBullet(EnemyBullet bullet)
        {
        }
        
        public static void CreatePlayerBullet(Vector2 position)
        {
            var b = PlayerBulletFactory.CreateStandartPlayerBullet(position);
            PlayerBullets.Add(b);
            b.Initialize();
            b.UpdatePosition(position);
            b.Destroy += () => BulletsToRemove.Add(b);
        }

        //public void CreatePattern(IBulletHellPattern pattern);
    }
}
