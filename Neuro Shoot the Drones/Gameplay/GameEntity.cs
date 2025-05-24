using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
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


        public delegate void DestroyedEventHandler();
        public event DestroyedEventHandler OnDestroy;
        public delegate void InitializedEventHandler();
        public event InitializedEventHandler OnInitialized;

        //NOTE: It is recommeded to set value to CollisionComponent in the Constructor
        public CollisionComponent CollisionComponent { get; protected set; }
        public DrawableComponent DrawableComponent { get; protected set;  }

        //NOTE: It is recommended to base.Inialize() ater Initializing child class
        public virtual void Initialize() 
        {
            OnInitialized?.Invoke();
        }

        //NOTE: It is recomended to base.Update(gameTime) after Updating child class
        public virtual void Update(GameTime gameTime) 
        {
            CollisionComponent.UpdatePosition(Position);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb) 
        {
            DrawableComponent.Draw(gameTime, sb, Position, Rotation);
        }

        public void Destroy()
        {
            OnDestroy?.Invoke();
            OnDestroy = null;
        }
    }
}
