using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Bullets
{
    internal class BulletSystem
    {
        //So GC won't touch it
        List<BaseBullet> Bullets = new();
        List<BaseBullet> BulletsToDestroy = new();
        TransformComponent PlayerTranslation;
        public readonly Rectangle BulletAllowedArea = ResolutionData.VisibleGameplayArea.Grow(500);

        public BulletSystem(TransformComponent playerTranslation)
        {
            PlayerTranslation = playerTranslation;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var bullet in BulletsToDestroy)
            {
                Bullets.Remove(bullet);
            }
            BulletsToDestroy.Clear();

            foreach(var bullet in Bullets)
            {
                var position = bullet.GetComponent<TransformComponent>().Position;
                bullet.Time += gameTime.ElapsedGameTime.TotalSeconds;
                if (!BulletAllowedArea.InBounds(position) || bullet.Time >= bullet.LifeTime)
                    bullet.Destroy();
            }    
        }

        public void AddBullet(BaseBullet b)
        {
            Bullets.Add(b);
            b.Initialize();
            b.OnDestroy += () => BulletsToDestroy.Add(b);
        }

        public void DestoryBullet(BaseBullet b)
        {
            BulletsToDestroy.Add(b);
        }
    }
}
