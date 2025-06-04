using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Drawable
{
    //NOTE: DrawableSystem is unique System, because it needs Draw method
    internal class DrawableSystem : BaseSystem
    {
        public DrawableSystem() : base(typeof(DrawableComponent))
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
            foreach(DrawableComponent drawable in Components)
            {
                if (drawable.SyncRotation)
                    drawable.DisplayRotation = drawable.Transform.Rotation;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (DrawableComponent drawable in Components)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    var a = 0;
                }
                if (drawable.Skip) continue;
                var position = drawable.Transform.Position;
                sb.Draw(texture: drawable.Texture,
                        position: position,
                        sourceRectangle: drawable.SourceRect,
                        color: Color.White,
                        rotation: drawable.DisplayRotation,
                        origin: drawable.Origin,
                        scale: drawable.Scale,
                        effects: drawable.Effect,
                        layerDepth: drawable.LayerDepth);
            }
        }
    }
}
