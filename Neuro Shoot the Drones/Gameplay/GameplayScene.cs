using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.Gameplay;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using Neuro_Shoot_the_Drones.Gameplay.Enemies;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories;
using Neuro_Shoot_the_Drones.Gameplay.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{ 
    //TODO: Make in-game UI, featuring score, health, bombs, power
    //TODO: Make bombs
    //TODO: Make Pickups
    internal class GameplayScene: IGameScene
    {
        public Player player;

        //TODO: Consider extraction to EnemySystem or EnemyComponent
        List<Enemy> Enemies = new();
        List<Enemy> EnemiesToRemove = new();

        ControlComponent Controls = new();
        CollisionSystem Collisions = new();
        Level Level = new Level();

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Begin();
            BulletHell.Draw(gameTime, sb);
            player.Draw(gameTime, sb);
            foreach (Enemy enemy in Enemies) enemy.Draw(gameTime, sb);
            sb.Draw(Resources.GameFrameUI, new Vector2(250, 0), Color.White);
            sb.End();
        }

        public void Initialize()
        {
            BulletHell.Initialize();
            BulletHell.OnBulletAdded += (bullet) => Collisions.AddCollider(bullet.CollisionComponent);
            BulletHell.OnBulletRemoved += (bullet) => Collisions.RemoveCollider(bullet.CollisionComponent);

            player = new Player();
            player.Initialize();
            Collisions.AddCollider(player.CollisionComponent);

            ConfigurePlayerControl();

            //NOTE: This is temporal decision
            Level.FillInTimeLine();

            Level.OnEnemySpawned += (enemy) =>
            {
                Enemies.Add(enemy);
                Collisions.AddCollider(enemy.CollisionComponent);
                enemy.OnDeath += () => EnemiesToRemove.Add(enemy);
                enemy.OnDeath += () => Collisions.RemoveCollider(enemy.CollisionComponent);
                enemy.Initialize();
            };
        }

        public void Update(GameTime gameTime)
        {
            Controls.Update(gameTime);
            Level.Update(gameTime);
            player.Update(gameTime);
            BulletHell.PlayerPosition = player.Position;
            BulletHell.Update(gameTime);
            Collisions.Update();

            foreach(var enemy in EnemiesToRemove)
                Enemies.Remove(enemy);
            EnemiesToRemove.Clear();

            foreach (Enemy enemy in Enemies)
                enemy.Update(gameTime);
        }


        //TODO: This should be another class that holds the Controls setting, and loads it from there, REMOVE LATER
        private void ConfigurePlayerControl()
        {
            Controls.BindKeyDownAction(Keys.Left, (gameTime) => player.ControlLeft());
            Controls.BindKeyDownAction(Keys.Right, (gameTime) => player.ControlRight());
            Controls.BindKeyDownAction(Keys.Down, (gameTime) => player.ControlDown());
            Controls.BindKeyDownAction(Keys.Up, (gameTime) => player.ControlUp());
            Controls.BindKeyDownAction(Keys.Z, (gameTime) => player.Shoot(gameTime));
            Controls.BindKeyDownAction(Keys.LeftShift, (gameTime) => player.Focus());
        }
    }
}
