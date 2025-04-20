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
        static public Vector2 PlayerPosition = Vector2.Zero;
        static List<PlayerBullet> PlayerBullets = new List<PlayerBullet>();
        static List<PlayerBullet> PlayerBulletsToRemove = new();
        static List<EnemyBullet> EnemyBulletsToRemove = new();
        static List<EnemyBullet> EnemyBullets = new();

        public static void Draw(GameTime gameTime, SpriteBatch sb)
        {
            Parallel.ForEach(PlayerBullets, (PlayerBullet b) =>
            {
                if(b.IsActive)
                    b.Draw(gameTime, sb);
            });
            foreach (var b in EnemyBullets)
            {
                b.Draw(gameTime, sb);
            }
        }

        public static void Initialize()
        {
            PlayerBullets.Clear();
            PlayerBulletsToRemove.Clear();
            EnemyBulletsToRemove.Clear();
            EnemyBullets.Clear();
        }

        public static void Update(GameTime gameTime)
        {
            foreach(PlayerBullet b in PlayerBulletsToRemove)
            {
                PlayerBullets.Remove(b);
            }
            PlayerBulletsToRemove.Clear();
            foreach(var b in PlayerBullets)
            {
                b.Update(gameTime);
            }

            foreach(EnemyBullet b in EnemyBullets)
            {
                b.Update(gameTime);
            }
        }

        public static void CreateEnemyBullet(EnemyBullet bullet)
        {
            var b = bullet;
            EnemyBullets.Add(b);
            b.Initialize();
            b.OnDestroy += () => EnemyBulletsToRemove.Add(b);
        }
        
        public static void CreatePlayerBullet(Vector2 position)
        {
            var b = PlayerBulletFactory.CreateStandartPlayerBullet(position);
            PlayerBullets.Add(b);
            b.OnDestroy += () => PlayerBulletsToRemove.Add(b);
            b.Initialize();
        }
    }
}
