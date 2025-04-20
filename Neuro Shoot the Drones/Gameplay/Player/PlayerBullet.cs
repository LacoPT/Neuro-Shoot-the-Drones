using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal class PlayerBullet : GameEntity
    {
        public bool IsActive { get; private set; } = false;
        public int Damage;
        public float Speed;
        public float Acceleration;

        Vector2 RelativeDestinationCenter = Vector2.Zero;
        Vector2 TextureScale = Vector2.One;

        public delegate void DestroyHandler();
        public event DestroyHandler OnDestroy;

        public PlayerBullet(Texture2D texture, Rectangle sourceRect, int hitCircleSize, Vector2 position, int damage, float speed, float acceleration)
        {
            DrawableComponent = new(texture, sourceRect, Vector2.One, sourceRect.GetRelativeCenter());
            HitCircleSize = hitCircleSize;
            Position = position;
            Damage = damage;
            Speed = speed;
            Acceleration = acceleration;
            OnDestroy += () => IsActive = false;
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
                OnDestroy();
            Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed += Acceleration;
        }
    }
}
