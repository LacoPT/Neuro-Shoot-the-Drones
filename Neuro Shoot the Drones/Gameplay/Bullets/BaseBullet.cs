using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Gameplay.Drawable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Bullets
{
    internal abstract class BaseBullet : GameEntity
    {
        public readonly double LifeTime = 3f;
        public double Time = 0f;
        public BaseBullet(Texture2D texture, Rectangle textureSourceRect, Vector2 textureScale, Vector2 position, float baseSpeed, float acceleration, float rotation = 0)
        : base(position)
        {
            var move = new MoveComponent(this);
            move.Velocity = new Vector2(0, -baseSpeed);
            move.Acceleration = new Vector2(0, -acceleration);
            AddComponent(move);
            var drawable = new Drawable.DrawableComponent(this, texture, textureSourceRect, textureSourceRect.GetRelativeCenter(), Vector2.One, syncRotation: false);
            AddComponent(drawable);
            //NOTE: Colision is added in derived classes
        }
        protected override void OnInitilize()
        {
            GetComponent<CollisionComponent>().OnCollisionRegistered += CollisionBehaviour;
        }

        protected abstract void CollisionBehaviour(CollisionData data);
    }
}
