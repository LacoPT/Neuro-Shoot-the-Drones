using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay.Bullets;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Gameplay.Drawable;
using Neuro_Shoot_the_Drones.Timeline;
using Neuro_Shoot_the_Drones.Tweens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies
{
    internal class Enemy : GameEntity
    {
        public int Health { get; private set; }

        public delegate void DeathEventHandler(EnemyDeathDataComponent deathData);
        public event DeathEventHandler OnDeath;

        public delegate void AddTweenEventHandler(Tween tween);
        public event AddTweenEventHandler OnAddTween;

        public delegate void ShootEventHandler(List<EnemyBullet> bullets);
        public event ShootEventHandler OnShoot;

        /// <summary>
        ///  Use AddComponent with TimeLine component before Initializing
        /// </summary>
        public Enemy(Texture2D texture, Rectangle textureSourceRect, Vector2 textureScale, Vector2 startPosition,
            float initialRotation = 0, float health = 1, int hitCircleSize = 1, int score = 0)
       : base(startPosition, initialRotation)
        {
            var drawable = new Drawable.DrawableComponent(this, texture, textureSourceRect, textureSourceRect.GetRelativeCenter(), textureScale, syncRotation: false);
            AddComponent(drawable);
            var collision = new CollisionComponent(hitCircleSize, CollisionLayers.Player | CollisionLayers.PlayerBullet,
                                                   CollisionLayers.Enemy, new(0), this);
            AddComponent(collision);
            var healthComponent = new HealthComponent(this, health);
            AddComponent(healthComponent);
            var timeLine = new TimeLineComponent(this);
            AddComponent(timeLine);
            var deathData = new EnemyDeathDataComponent(this, score);
            AddComponent(deathData);
            healthComponent.OnDeath += () => OnDeath(deathData);
            collision.OnCollisionRegistered += (data) => healthComponent.Hurt(data.Damage);
        }

        protected override void OnInitilize()
        {
            GetComponent<TimeLineComponent>().Start();
        }

        public void AddTween(Tween tween)
        {
            OnAddTween(tween);
        }

        public void ShotPattern(List<EnemyBullet> bullets)
        {
            OnShoot(bullets);
        }
    }
}
