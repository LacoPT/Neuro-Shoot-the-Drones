using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.Gameplay.Enemies;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories;
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

        //TODO: Extract into Level System;
        TimeLineComponent TimeLine = new();

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

            FillTimeLine();
            TimeLine.Start();
        }

        private void FillTimeLine()
        {
            TimeLine.AddElement(0, () =>
            {
                var enemy = EnemyID.Create(0, new Vector2(400, -300));
                enemies.Add(enemy);
                enemy.Initialize();
            });
            TimeLine.AddElement(0.5, () =>
            {
                var enemy = EnemyID.Create(0, new Vector2(550, -200));
                enemies.Add(enemy);
                enemy.Initialize();
            });
            TimeLine.AddElement(1, () =>
            {
                var enemy = EnemyID.Create(0, new Vector2(700, -150));
                enemies.Add(enemy);
                enemy.Initialize();
            });
            TimeLine.AddElement(1.5, () =>
            {
                var enemy = EnemyID.Create(0, new Vector2(850, -250));
                enemies.Add(enemy);
                enemy.Initialize();
            });
        }

        public void Update(GameTime gameTime)
        {
            PlayerControl(gameTime);
            TimeLine.Update(gameTime);
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
