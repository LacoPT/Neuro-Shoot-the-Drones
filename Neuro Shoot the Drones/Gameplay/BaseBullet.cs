using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay
{
    internal abstract class BaseBullet: GameEntity
    {
        protected float BaseSpeed;
        public float Velocity;
        public float Acceleration;

        public BaseBullet(Texture2D texture, Rectangle textureSourceRect, Vector2 textureScale, Vector2 position, float baseSpeed, float acceleration)
        {
            DrawableComponent = new(texture, textureSourceRect, textureScale, textureSourceRect.GetRelativeCenter());
            Position = position;
            BaseSpeed = baseSpeed;
            Velocity = baseSpeed;
            Acceleration = acceleration;
        }

        public override void Initialize()
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }

        public override void Update(GameTime gameTime)
        {
            Velocity += Acceleration;
            //Because bullets are facing upwards by default
            Position += new Vector2(0, -Velocity).Rotated(Rotation) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }
    }
}
