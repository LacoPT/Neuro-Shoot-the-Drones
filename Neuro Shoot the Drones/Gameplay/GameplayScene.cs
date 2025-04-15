using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.Gameplay.Enemies;
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
        public List<Enemy> enemies = new();

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Begin();
            BulletHell.Draw(gameTime, sb);
            player.Draw(gameTime, sb);
            foreach (Enemy enemy in enemies) enemy.Draw(gameTime, sb);
            sb.Draw(Resources.GameFrameUI, new Vector2(250, 0), Color.White);
            sb.End();
        }

        public void Initialize()
        {
            BulletHell.Initialize();
            player = new Player();
            player.Initialize();

            var enemy = EnemyFactory.CreateSimpleDrone();
            enemies.Add(enemy);
            enemy.Initialize();

        }

        public void Update(GameTime gameTime)
        {
            PlayerControl(gameTime);
            player.Update(gameTime);
            BulletHell.PlayerPosition = player.Position;
            BulletHell.Update(gameTime);
            foreach (Enemy enemy in enemies) 
                enemy.Update(gameTime);
        }

        private void PlayerControl(GameTime gameTime)
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
            {
                player.Focus();
            }
            else
                player.ExitFocus();
        }
    }
}
