using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Menus.Pause
{
    internal class PauseView : BaseView
    {
        private readonly PauseModel model;
        private Effect grayscale;

        public PauseView(PauseModel model)
        {
            this.model = model;
        }

        public void Initialize()
        {
            grayscale = Resources.GrayScale;
            grayscale.Parameters["Intensity"].SetValue(0.8f);
        }

        public override RenderTarget2D Draw(SpriteBatch sb)
        {
            var renderTarget = new RenderTarget2D(sb.GraphicsDevice, ResolutionData.Resolution.X, ResolutionData.Resolution.Y);
            sb.GraphicsDevice.SetRenderTarget(renderTarget);
            sb.GraphicsDevice.Clear(Color.Transparent);

            sb.Begin(effect: grayscale);
            sb.Draw(model.GamePlayLastFrame, Vector2.Zero, Color.White);
            sb.End();

            sb.Begin();
            sb.DrawString(Resources.DefaultFont, "ON PAUSE", Vector2.Zero, Color.White);
            foreach (var option in model.Options)
            {
                option.Draw(sb);
            }
            sb.End();

            sb.GraphicsDevice.SetRenderTarget(null);

            return renderTarget;
        }
    }
}
