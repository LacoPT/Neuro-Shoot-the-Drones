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
        static ConcurrentBag<PlayerBullet> PlayerBullets;
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
            for (int i = 0; i < 200; i++)
                PlayerBullets.Add(PlayerBulletFactory.CreateStandartPlayerBullet(Vector2.Zero));
        }

        public static void Update(GameTime gameTime)
        {
            Parallel.ForEach(PlayerBullets, (PlayerBullet b) => 
            {
                if (b.IsActive)
                    b.Update(gameTime);
            });
        }

        public static void CreateEnemyBullet(EnemyBullet bullet)
        {
        }
        
        public static void CreatePlayerBullet(Vector2 position)
        {
            var bullet = PlayerBullets.First((b) => !b.IsActive);
            bullet.Initialize();
            bullet.UpdatePosition(position);
        }

        //public void CreatePattern(IBulletHellPattern pattern);
    }
}
