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
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{ 
    //TODO: Make in-game UI, featuring score, health, bombs, power
    //TODO: Make bombs
    //TODO: Make Pickups
    internal class GameplayScene: IGameScene
    {
        public Player Player;

        //TODO: Consider extraction to EnemySystem or EnemyComponent
        List<Enemy> Enemies = new();
        List<Enemy> EnemiesToRemove = new();

        BulletSystem BulletSystem = new();
        ControlSystem Controls = new();
        CollisionSystem CollisionSystem = new();
        Level Level = new Level();

        //TODO: extract into score/UI system

        Texture2D BlackPixel;
        Rectangle LeftBlackBorder;
        Rectangle RightBlackBorder;
        String ScoreString = "SCORE: ";
        int Score = 0;
        int DisplayScore;
        Vector2 ScorePosition = new(1110, 150);
        Color ScoreColor = new Color(255, 240, 215);
        Tween ScoreTween = new(0, 0);

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Begin();
            BulletSystem.Draw(gameTime, sb);
            Player.Draw(gameTime, sb);
            foreach (Enemy enemy in Enemies) enemy.Draw(gameTime, sb);
            sb.Draw(Resources.GameFrameUI, new Vector2(250, 0), Color.White);
            sb.DrawString(Resources.DefaultFont, ScoreString, ScorePosition, ScoreColor, -float.Pi / 26, Vector2.Zero, 1, SpriteEffects.None, 0);
            sb.Draw(BlackPixel, LeftBlackBorder, Color.Black);
            sb.Draw(BlackPixel, RightBlackBorder, Color.Black);
            sb.End();
        }

        public void Initialize()
        {
            BlackPixel = Resources.BlackPixel;
            LeftBlackBorder = new(0, 0, 250, GlobalVariables.Resolution.Y);
            RightBlackBorder = new(250 + Resources.GameFrameUI.Width, 0, 500, GlobalVariables.Resolution.Y);
            InitializePlayer();
            ConfigurePlayerControl();
            //NOTE: This is temporal decision
            Level.FillInTimeLine();

            Level.OnEnemySpawned += (enemy) =>
            {
                Enemies.Add(enemy);
                CollisionSystem.AddCollider(enemy.CollisionComponent);
                enemy.OnPatternGenerated += (bullets) =>
                {
                    foreach(var bullet in bullets)
                        BulletSystem.CreateEnemyBullet(bullet);
                };
                enemy.OnDestroy += () => EnemiesToRemove.Add(enemy);
                enemy.OnDestroy += () => CollisionSystem.RemoveCollider(enemy.CollisionComponent);
                enemy.OnDeath += () =>
                {
                    Score += 100;
                    UpdateScore(Score);
                };
                enemy.Initialize();
            };
        }

        public void Update(GameTime gameTime)
        {
            Controls.Update(gameTime);
            Level.Update(gameTime);
            Player.Update(gameTime);
            BulletSystem.Update(gameTime);
            CollisionSystem.Update();

            foreach(var enemy in EnemiesToRemove)
                Enemies.Remove(enemy);

            EnemiesToRemove.Clear();

            foreach (Enemy enemy in Enemies)
                enemy.Update(gameTime);

            ScoreTween.Update(gameTime);
            ScoreString = $"SCORE: {DisplayScore}";
        }

        private void InitializePlayer()
        {
            Player = new Player();
            Player.OnInitialized += () => CollisionSystem.AddCollider(Player.CollisionComponent);
            Player.OnDestroy += () => CollisionSystem.RemoveCollider(Player.CollisionComponent);
            Player.OnShoot += (position) =>
            {
                var bullet = BulletSystem.CreatePlayerBullet(position);
                CollisionSystem.AddCollider(bullet.CollisionComponent);
                bullet.OnDestroy += () => CollisionSystem.RemoveCollider(bullet.CollisionComponent);
            };


            Player.Initialize();
        }

        private void UpdateScore(int newScore)
        {
            ScoreTween?.Interrupt();
            ScoreTween = new Tween(DisplayScore, newScore, 0.4f);
            ScoreTween.OnUpdate += () => DisplayScore = (int)ScoreTween.Value;
            ScoreTween.Start();
        }
        

        //TODO: This should be another class that holds the Controls setting, and loads it from there, REMOVE LATER
        private void ConfigurePlayerControl()
        {
            Controls.BindKeyDownAction(Keys.Left, (gameTime) => Player.ControlLeft());
            Controls.BindKeyDownAction(Keys.Right, (gameTime) => Player.ControlRight());
            Controls.BindKeyDownAction(Keys.Down, (gameTime) => Player.ControlDown());
            Controls.BindKeyDownAction(Keys.Up, (gameTime) => Player.ControlUp());
            Controls.BindKeyDownAction(Keys.Z, (gameTime) => Player.Shoot(gameTime));
            Controls.BindKeyDownAction(Keys.LeftShift, (gameTime) => Player.Focus());
        }
    }
}
