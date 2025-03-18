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
        Texture2D texture;
        public float Speed;
        public Vector2 Veclocity;
        public Vector2 Acceleration;
        public Point Position;
        public float Rotation;
        public float RotationSpeed;
        public float RotationAcceleration;

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
