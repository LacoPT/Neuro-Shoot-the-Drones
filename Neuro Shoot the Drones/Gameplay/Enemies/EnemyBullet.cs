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
/*    internal class EnemyBullet : GameEntity
    {
        public float BasicSpeed { get; private set; }
        public Vector2 Velocity { get; private set; }
        public Vector2 Acceleration { get; private set; }
        public float RotationSpeed { get; private set; } = 0;
        public float RotationAcceleration { get; private set; } = 0;

        public EnemyBullet(Rectangle textureSourceRect,
            Vector2 textureScale,
            Vector2 position,
            float speed = 500,
            int hitCircleSize = 10,
            float rotation = 0,
            float rotationSpeed = 0,
            float rotationAcceleration = 0)
        {
            //TODO: Solve texture problem
            DrawableComponent = new(Resources.BulletTextureAtlas, textureSourceRect, textureScale, textureSourceRect.GetRelativeCenter());
            HitCircleSize = hitCircleSize;
            Position = position;
            BasicSpeed = speed;
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
            Rotation = rotation;
            RotationSpeed = rotationSpeed;
            RotationAcceleration = rotationAcceleration;
            //TODO: Right now EnemyBullets does not collide with anything, because CollisionLayers is set to None
            CollisionComponent = new(hitCircleSize, CollisionLayers.None, CollisionLayers.None, new CollisionData(0));
        }

        public EnemyBullet(Rectangle textureSourceRect,
            Vector2 textureScale,
            Vector2 position,
            Vector2 velocity,
            Vector2 acceleration,
            float speed = 500,
            int hitCircleSize = 10,
            float rotation = 0,
            float rotationSpeed = 0,
            float rotationAcceleration = 0) : this(textureSourceRect, textureScale, position, speed,
                                                    hitCircleSize, rotation, rotationSpeed, rotationAcceleration)
        {
            Velocity = velocity;
            Acceleration = acceleration;
        }
        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {
            Velocity = new Vector2(0, BasicSpeed).Rotated(Rotation) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity;

            base.Update(gameTime);
        }
    }*/

    internal class EnemyBullet : BaseBullet
    {
        public float RotationSpeed;
        public float RotationAcceleration;

        public EnemyBullet
            (
                Texture2D texture,
                Rectangle textureSourceRect,
                Vector2 textureScale,
                Vector2 position,
                float baseSpeed,
                float acceleration,
                float rotationSpeed,
                float rotationAcceleration,
                int hitCircleSize
            ): base(texture, textureSourceRect, textureScale, position, baseSpeed, acceleration)
        {
            RotationSpeed = rotationSpeed;
            RotationAcceleration = rotationAcceleration;
            CollisionComponent = new(hitCircleSize, CollisionLayers.Player, CollisionLayers.EnemyBullet, new CollisionData(0));
        }

        public override void Update(GameTime gameTime)
        {
            RotationSpeed += RotationAcceleration;
            Rotation += RotationSpeed;

            base.Update(gameTime);
        }
    }

}
