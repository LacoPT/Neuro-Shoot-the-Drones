using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal class Enemy : IGameObject
    {
        public int Health { get; private set; }
        private int HitCircleSize = 15;
        public Vector2 Position { get; private set; }
        private Texture2D Texture;
        private Rectangle TextureSourceRect;
        private Vector2 TextureScale;
        private List<Tween<double>> tweens = new();


        public delegate void OnHitEventHandler();
        public delegate void OnDeathEventHandler();
        public delegate void OnUpdateEventHandler(GameTime gameTime);
        public event OnUpdateEventHandler OnUpdate;
        public event OnHitEventHandler OnHit;
        public event OnDeathEventHandler OnDeath;
        public TimeLine TimeLine { get; private set; } = new TimeLine();

        public Enemy(int health, int hitCircleSize, Vector2 initialPosition, Texture2D texture, Rectangle textureSourceRect, Vector2 textureScale)
        {
            Health = health;
            HitCircleSize = hitCircleSize;
            Position = initialPosition;
            Texture = texture;
            TextureSourceRect = textureSourceRect;
            TextureScale = textureScale;
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(
                    texture: Texture,
                    position: Position,
                    sourceRectangle: TextureSourceRect,
                    color: Color.White,
                    rotation: 0f,
                    origin: TextureSourceRect.GetRelativeCenter(),
                    scale: TextureScale,
                    effects: SpriteEffects.None,
                    layerDepth: 0f);
        }

        public void Initialize()
        {
            TimeLine.Start();
        }

        public void Update(GameTime gameTime)
        {
            OnUpdate?.Invoke(gameTime);
            TimeLine.Update(gameTime);
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        public void AddTween(Tween<double> tween)
        {
            tweens.Add(tween);
            OnUpdate += (gameTime) => tween.Update(gameTime);
            tween.OnFinish += () => tweens.Remove(tween);
        }
    }
}
