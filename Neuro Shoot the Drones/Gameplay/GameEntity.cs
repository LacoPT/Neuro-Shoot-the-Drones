using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal abstract class GameEntity
    {
        public int HitCircleSize;
        public Vector2 Position = Vector2.Zero;
        public float Rotation;
        protected DrawableComponent DrawableComponent;
        public virtual void Initialize() { }
        public virtual void Update(GameTime gameTime) 
        { }
        public virtual void Draw(GameTime gameTime, SpriteBatch sb) 
        {
            DrawableComponent.Draw(gameTime, sb, Position, Rotation);
        }
    }
}
