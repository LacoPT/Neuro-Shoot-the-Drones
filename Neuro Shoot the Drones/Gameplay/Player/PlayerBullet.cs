using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal class PlayerBullet : IGameObject
    {
        public bool IsActive { get; private set; } = false;
        public Texture2D Texture;
        public Rectangle TextureSourceRect;
        public int HitCircleSize;
        public Vector2 Position;
        public int Damage;
        public float Speed;
        public float Acceleration;

        Vector2 RelativeDestinationCenter = Vector2.Zero;
        Vector2 TextureScale = Vector2.One;

        public delegate void DestroyHandler();
        public event DestroyHandler OnDestroy;

        public PlayerBullet(Texture2D texture, Rectangle sourceRect, int hitCircleSize, Vector2 position, int damage, float speed, float acceleration)
        {
            Texture = texture;
            TextureSourceRect = sourceRect;
            HitCircleSize = hitCircleSize;
            Position = position;
            Damage = damage;
            Speed = speed;
            Acceleration = acceleration;
            OnDestroy += () => IsActive = false;
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(
                    texture: Texture,
                    position: Position,
                    sourceRectangle: TextureSourceRect,
                    color: Color.White,
                    rotation: 0f,
                    origin: RelativeDestinationCenter,
                    scale: TextureScale,
                    effects: SpriteEffects.None,
                    layerDepth: 0f);
        }

        public void Initialize()
        {
            RelativeDestinationCenter = TextureSourceRect.GetRelativeCenter();
            IsActive = true;
        }

        public void Update(GameTime gameTime)
        {
            if (Position.Y < GlobalVariables.VisibleGameplayArea.Top)
                OnDestroy();
            Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void UpdatePosition(Vector2 newPos)
        {
            Position = newPos;
        }
    }
}
