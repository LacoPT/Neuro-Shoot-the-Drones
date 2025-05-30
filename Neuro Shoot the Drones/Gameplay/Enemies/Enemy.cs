using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    //TODO: Write hit mechanic
    internal class Enemy : GameEntity
    {
        public int Health { get; private set; }
        public bool IsBoss { get; private set; } = false;

        public delegate void OnHitEventHandler();
        public delegate void OnDeathEventHandler();
        public delegate void OnUpdateEventHandler(GameTime gameTime);
        public event OnUpdateEventHandler OnUpdate;
        public event OnHitEventHandler OnHit;
        public delegate void PatternGeneratedEventHandler(List<EnemyBullet> bullets);
        public event PatternGeneratedEventHandler OnPatternGenerated;
        //NOTE: There is differences between Death and Destroy
        //Destroy may be called when Enemy exits scene naturally
        //Death however is called when enemy's health is below zero, based on that we might add score, spawn pickups or play sounds
        public event OnDeathEventHandler OnDeath;
        public TimeLineComponent TimeLine { get; private set; } = new TimeLineComponent();

        public Enemy(int health, int hitCircleSize, Vector2 initialPosition, Texture2D texture, Rectangle textureSourceRect, Vector2 textureScale)
        {
            Health = health;
            HitCircleSize = hitCircleSize;
            Position = initialPosition;
            DrawableComponent = new(texture, textureSourceRect, textureScale, textureSourceRect.GetRelativeCenter());
            CollisionComponent = new( HitCircleSize, CollisionLayers.PlayerBullet | CollisionLayers.Player, CollisionLayers.Enemy, new(0) );
            CollisionComponent.OnCollisionRegistered += (collisionData) =>
            {
                Hit(collisionData.Damage);
            };
            OnDeath += Destroy;
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }


        public override void Initialize()
        {
            TimeLine.Start();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            OnUpdate?.Invoke(gameTime);
            TimeLine.Update(gameTime);

            base.Update(gameTime);
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        public void AddTween(Tween tween)
        {
            OnUpdate += (gameTime) => tween.Update(gameTime);
        }


        //TODO: Consider rewriting to support more complex behaviour, for example knockback
        //TODO: Consider making HitableComponent
        public void Hit(int damage)
        {
            OnHit?.Invoke();
            Health -= damage;
            if (Health <= 0) OnDeath?.Invoke();
        }
        
        public void GeneratePattern(List<EnemyBullet> bullets)
        {
            OnPatternGenerated(bullets);
        }

        public void ToggleBoss()
        {
            IsBoss = !IsBoss;
        }
    }
}
