using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Menus
{
    //TODO:: Implement
    internal class GameEndScene : IGameScene
    {
        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Begin();
            sb.GraphicsDevice.Clear(Color.CornflowerBlue);
            sb.End();
        }

        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
