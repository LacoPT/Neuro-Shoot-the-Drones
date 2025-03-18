using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal class GameplayScene: IGameScene
    {
        public Player player;

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Begin();
            BulletHell.Draw(gameTime, sb);
            player.Draw(gameTime, sb);
            sb.Draw(Resources.GameFrameUI, new Vector2(250, 0), Color.White);
            sb.End();
        }

        public void Initialize()
        {
            BulletHell.Initialize();
            player = new Player();
            player.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                player.ControlLeft();
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                player.ControlRight();
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                player.ControlDown();
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                player.ControlUp();
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
                player.Shoot(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                player.Focus();
            else
                player.ExitFocus();
            player.Update(gameTime);
            BulletHell.Update(gameTime);
        }
    }
}
