using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Menus.Main
{
    internal class MainMenuView : BaseView
    {
        private readonly MainMenuModel model;

        public MainMenuView(MainMenuModel model)
        {
            this.model = model;
        }

        public override RenderTarget2D Draw(SpriteBatch sb)
        {
            var gd = sb.GraphicsDevice;
            var target = new RenderTarget2D(gd, ResolutionData.Resolution.X, ResolutionData.Resolution.Y);
            gd.SetRenderTarget(target);

            sb.Begin();
            sb.Draw(Resources.MainMenuBG, Vector2.Zero, Color.White);
            foreach (var option in model.Options)
            {
                option.Draw(sb);
            }
            sb.End();

            gd.SetRenderTarget(null);

            return target;
        }
    }
}
