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
/*    internal class PlayerBullet : GameEntity
    {
        public bool IsActive { get; private set; } = false;
        public int Damage;
        public float Speed;
        public float Acceleration;

        Vector2 RelativeDestinationCenter = Vector2.Zero;
        Vector2 TextureScale = Vector2.One;

        public delegate void DestroyEventHandler();
        public event DestroyEventHandler OnDestroy;

        //TODO: ConsiderRemoving
        public delegate void HitEventHandler();
        public event HitEventHandler OnHit;

        public PlayerBullet(Texture2D texture, Rectangle sourceRect, int hitCircleSize, Vector2 position, int damage, float speed, float acceleration)
        {
            DrawableComponent = new(texture, sourceRect, Vector2.One, sourceRect.GetRelativeCenter());
            HitCircleSize = hitCircleSize;
            Position = position;
            Damage = damage;
            Speed = speed;
            Acceleration = acceleration;
            OnDestroy += () => IsActive = false;
            CollisionComponent = new(hitCircleSize, CollisionLayers.Enemy, CollisionLayers.PlayerBullet, new CollisionData(damage));
            CollisionComponent.OnCollisionRegistered += (collisionData) => Hit();
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }

        public override void Initialize()
        {
            IsActive = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (Position.Y < GlobalVariables.VisibleGameplayArea.Top)
                Destroy();
            Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed += Acceleration;

            base.Update(gameTime);
        }

        public void Hit()
        {
            OnHit?.Invoke();
        }

        public void Destroy()
        {
            OnDestroy?.Invoke();
        }
    }
*/
    internal class PlayerBullet: BaseBullet
    {
        public readonly int Damage;
        int HitLimit = 1;
        public PlayerBullet
            (
                Texture2D texture,
                Rectangle textureSourceRect,
                int hitCircleSize,
                Vector2 textureScale,
                Vector2 position,
                int damage,
                float baseSpeed,
                float acceleration,
                int hitLimit
            ) : base(texture, textureSourceRect, textureScale, position, baseSpeed, acceleration)
        {
            Damage = damage;
            HitLimit = hitLimit;
            CollisionComponent = new(hitCircleSize, CollisionLayers.Enemy, CollisionLayers.PlayerBullet, new CollisionData(damage));
            CollisionComponent.OnCollisionRegistered += (collisionData) => Hit();
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }

        public override void Update(GameTime gameTime)
        {
            if (Position.Y < GlobalVariables.VisibleGameplayArea.Y)
                Destroy();

            base.Update(gameTime);
        }

        void Hit()
        {
            HitLimit--;
            if (HitLimit <= 0)
                Destroy();
        }
        
    }
}
