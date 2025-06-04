using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.ECS;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Drawable
{
    //TODO: ESC MIGRATION NOTE: When migration is complete, rename all of the classes with ECS prefix to be without prefix and delete messy originals
    internal class DrawableComponent : Component
    {
        public Texture2D Texture;
        public Rectangle SourceRect;
        public TransformComponent Transform;
        public Vector2 Scale;
        public Vector2 Origin;
        public float DisplayRotation;
        public bool SyncRotation;
        public SpriteEffects Effect;
        public float LayerDepth;


        public DrawableComponent(BaseEntity entity, Texture2D texture, Rectangle sourceRect, Vector2 origin, Vector2 scale,
        TransformComponent transform = null, float displayRotation = 0, bool syncRotation = true,
        SpriteEffects effect = SpriteEffects.None, float layerDepth = 0f)
        : base(entity)
        {
            Texture = texture;

            if(sourceRect.IsEmpty) SourceRect = texture.Bounds;
            else SourceRect = sourceRect;

            if(transform != null ) Transform = transform;
            else Transform = entity.GetComponent<TransformComponent>();
            Origin = origin;
            Scale = scale;
            DisplayRotation = displayRotation;
            SyncRotation = syncRotation;
            Effect = effect;
            LayerDepth = layerDepth;
        }
    }
}
