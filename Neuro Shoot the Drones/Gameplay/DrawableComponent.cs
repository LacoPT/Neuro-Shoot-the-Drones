using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    //TODO: ECS MIGRATION NOTE: Translation must be a field
    //TODO: ECS MIGRATION NOTE: Drawable component
    internal class DrawableComponent
    {
        public Texture2D TextureAtlas { get; private set; }
        public Rectangle TextureSourceRectangle { get; private set; }
        public Vector2 TextureScale { get; private set; }
        public Vector2 Origin { get; private set; }

        public SpriteEffects Effect = SpriteEffects.None;

        public DrawableComponent(Texture2D textureAtlas, Rectangle textureSourceRectangle, Vector2 textureScale, Vector2 origin, BaseEntity entity) 
        //: base(entity)
        {
            TextureAtlas = textureAtlas;
            TextureSourceRectangle = textureSourceRectangle;
            TextureScale = textureScale;
            Origin = origin;
        }

        //TODO: ECS MIGRATION NOTE: This method should not exist, this must be inside DrawableSystem
        public void Draw(GameTime gameTime, SpriteBatch sb, Vector2 position, float rotation)
        {
            sb.Draw(texture: TextureAtlas,
                    position: position,
                    sourceRectangle: TextureSourceRectangle,
                    color: Color.White,
                    rotation: rotation,
                    origin: Origin,
                    effects: Effect,
                    scale: TextureScale,
                    layerDepth: 0f);
        }
    }
}
