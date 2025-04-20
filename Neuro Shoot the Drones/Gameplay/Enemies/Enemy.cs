using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal class Enemy : GameEntity
    {
        public int Health { get; private set; }

        public delegate void OnHitEventHandler();
        public delegate void OnDeathEventHandler();
        public delegate void OnUpdateEventHandler(GameTime gameTime);
        public event OnUpdateEventHandler OnUpdate;
        public event OnHitEventHandler OnHit;
        public event OnDeathEventHandler OnDeath;
        public TimeLineComponent TimeLine { get; private set; } = new TimeLineComponent();

        public Enemy(int health, int hitCircleSize, Vector2 initialPosition, Texture2D texture, Rectangle textureSourceRect, Vector2 textureScale)
        {
            Health = health;
            HitCircleSize = hitCircleSize;
            Position = initialPosition;
            DrawableComponent = new(texture, textureSourceRect, textureScale, textureSourceRect.GetRelativeCenter());
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }


        public override void Initialize()
        {
            TimeLine.Start();
        }

        public override void Update(GameTime gameTime)
        {
            OnUpdate?.Invoke(gameTime);
            TimeLine.Update(gameTime);
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        public void AddTween(Tween tween)
        {
            OnUpdate += (gameTime) => tween.Update(gameTime);
        }
    }
}
