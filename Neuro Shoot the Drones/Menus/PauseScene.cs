using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Menus
{
    internal class PauseScene : IGameScene
    {
        RenderTarget2D GamePlayLastFrame;

        public delegate void UnpauseEventHandler();
        public event UnpauseEventHandler OnUnpause;

        ControlSystem ControlSystem = new();
        Effect grayscale;
        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Begin(effect: grayscale);
            sb.Draw(GamePlayLastFrame, Vector2.Zero, Color.White);
            sb.End();
            sb.Begin();
            sb.DrawString(Resources.DefaultFont, "ON PAUSE", Vector2.Zero, Color.White);
            sb.End();
        }

        public void Initialize()
        {
            ControlSystem.BindKeyJustDownAction(Keys.Escape, Unpause);
            grayscale = Resources.GrayScale;
            grayscale.Parameters["Intensity"].SetValue(0.8f);
        }


        public void SetLastFrame(RenderTarget2D lastFrame)
        {
            GamePlayLastFrame = lastFrame;
        }

        public void Update(GameTime gameTime)
        {
            ControlSystem.Update(gameTime);
        }
        private void Unpause()
        {
            OnUnpause();
        }
    }
}
