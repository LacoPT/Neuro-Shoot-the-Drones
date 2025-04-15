using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal class EnemyBullet : IGameObject
    {
        bool IsActive;
        Texture2D Texture;
        Rectangle TextureSourceRect;
        Vector2 TextureScale;
        public readonly int HitCircleSize = 10;

        

        public Vector2 Position { get; private set; }
        public float BasicSpeed { get; private set; }
        public Vector2 Velocity { get; private set; }
        public Vector2 Acceleration { get; private set; } = Vector2.Zero;
        public float Rotation; 
        public float RotationSpeed { get; private set; } = 0;
        public float RotationAcceleration { get; private set; } = 0;

        public delegate void OnDestroyHandler();
        public event OnDestroyHandler OnDestroy;

        //1, 49, 17, 65
        public EnemyBullet(Rectangle textureSourceRect,
            Vector2 textureScale,
            Vector2 position,
            float speed = 500,
            int hitCircleSize = 10,
            float rotation = 0,
            float rotationSpeed = 0,
            float rotationAcceleration = 0)
        {
            TextureSourceRect = textureSourceRect;
            TextureScale = textureScale;
            HitCircleSize = hitCircleSize;
            Position = position;
            BasicSpeed = speed;
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
            Rotation = rotation;
            RotationSpeed = rotationSpeed;
            RotationAcceleration = rotationAcceleration;
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
            float rotationAcceleration = 0)
        {
            TextureSourceRect = textureSourceRect;
            TextureScale = textureScale;
            HitCircleSize = hitCircleSize;
            HitCircleSize = hitCircleSize;
            Position = position;
            BasicSpeed = speed;
            Acceleration = acceleration;
            Rotation = rotation;
            RotationSpeed = rotationSpeed;
            RotationAcceleration = rotationAcceleration;
        }
        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(texture: Texture,
                position: Position,
                sourceRectangle: TextureSourceRect,
                color: Color.White,
                rotation: Rotation,
                origin: TextureSourceRect.GetRelativeCenter(),
                scale: TextureScale,
                effects: SpriteEffects.None,
                layerDepth: 0f);
        }

        public void Initialize()
        {
            Texture = Resources.BulletTextureAtlas;
        }

        public void Update(GameTime gameTime)
        {
            Velocity = new Vector2(0, BasicSpeed).Rotated(Rotation) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity;
        }
    }
}
