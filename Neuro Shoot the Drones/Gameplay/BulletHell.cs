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
    //Note: This might be callet BulletSystem or BulletHellSystem also consider using EventBus, to get rid of staticity
    //BUG: This class is SO SHIT, i need to rewrite EVERYTHING
    internal static class BulletHell
    {
        static public Vector2 PlayerPosition = Vector2.Zero;
        static List<PlayerBullet> PlayerBullets = new List<PlayerBullet>();
        static List<PlayerBullet> PlayerBulletsToRemove = new();
        static List<EnemyBullet> EnemyBulletsToRemove = new();
        static List<EnemyBullet> EnemyBullets = new();

        //TODO: Rewrite this shit, because scene should work as event bus, this class shouldn't be static in the first place
        //rn it is what it is 
        public delegate void BulletAddedEventHandler(GameEntity bullet);
        public delegate void BulletRemovedEventHandler(GameEntity bullet);

        public static event BulletAddedEventHandler OnBulletAdded;
        public static event BulletRemovedEventHandler OnBulletRemoved;

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
                OnBulletRemoved(b);
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
            //NOTE: We are not removing EnemyBullets any way right now
            b.OnDestroy += () => EnemyBulletsToRemove.Add(b);
            OnBulletAdded(bullet);
        }
        
        public static void CreatePlayerBullet(Vector2 position)
        {
            var b = PlayerBulletFactory.CreateStandartPlayerBullet(position);
            PlayerBullets.Add(b);
            b.OnDestroy += () => PlayerBulletsToRemove.Add(b);

            //NOTE: Temporaly decision since there will be more bullet types later
            b.OnHit += () => b.Destroy();
            b.Initialize();
            OnBulletAdded(b);
        }

    }
}
