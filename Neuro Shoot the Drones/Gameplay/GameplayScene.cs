using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay
{
    internal class GameplayScene : IGameScene
    {
        EntityManager EntityManager = new();
        ControlSystem ControlSystem = new();

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            var target = EntityManager.Draw(sb);
            sb.GraphicsDevice.SetRenderTarget(null);
            sb.Begin();
            sb.Draw(target, Vector2.Zero, Color.White);
            sb.Draw(Resources.GameFrameUI, new Vector2(250, 0), Color.White);
            sb.End();
            target.Dispose();
        }

        public void Initialize()
        {
            EntityManager.ControlSystem = new ControlSystem();
            EntityManager.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            EntityManager.Update(gameTime);
        }
    }
}
