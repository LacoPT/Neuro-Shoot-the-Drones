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
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
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
        public bool PlayerAlive = false;

        private int _score = 0;
        private int _health = 5;
        private int _healthShards = 0;
        private int _bombs = 0;
        private int _bombShards = 1;

        public delegate void PauseEventHandler(RenderTarget2D lastFrame);
        public event PauseEventHandler OnPause;

        Tween PlayerRespawnTimer = new(0, 0);
        Tween PlayerRespawnAnimation = new(0, 0);

        EnemySystem EnemySystem = new();
        BulletSystem BulletSystem = new();
        ControlSystem ControlSystem = new();
        CollisionSystem CollisionSystem = new();
        GUISystem GUISystem = new();
        Level Level = new();
        RenderTarget2D LastFrame;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                GUISystem.UpdateScore(value); // Sync with GUI
            }
        }

        // Health property
        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                if (value < 0) Health = 0;
                GUISystem.Health = Health; 
            }
        }

        // HealthShards property
        public int HealthShards
        {
            get => _healthShards;
            set
            {
                _healthShards = value;
                if (value < 0) _healthShards = 0;
                else if (value > 3)
                {
                    Health += 1;
                    _healthShards = 0;
                }
                GUISystem.HealthShards = _healthShards;
            }
        }

        // Bombs property
        public int Bombs
        {
            get => _bombs;
            set
            {
                _bombs = value;
                if (value < 0) _bombs = 0;
                GUISystem.Bombs = value; 
            }
        }
        public int BombShards
        {
            get => _bombShards;
            set
            {
                _bombShards = value;
                if (value < 0) _bombShards = 0;
                else if (value > 3)
                {
                    Health += 1;
                    _bombShards = 0;
                }
                GUISystem.BombShards = value;
            }
        }

        private void Pause()
        {
            OnPause(LastFrame);
        }

        Texture2D BlackPixel;
        Rectangle LeftBlackBorder;
        Rectangle RightBlackBorder;

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            LastFrame?.Dispose();
            //Draw the gameplay
            var graphicsDevice = sb.GraphicsDevice;
            var gamePlayRenderTarget = new RenderTarget2D(graphicsDevice, GlobalVariables.Resolution.X, GlobalVariables.Resolution.Y);

            LastFrame = new RenderTarget2D(graphicsDevice, GlobalVariables.Resolution.X, GlobalVariables.Resolution.Y);
            graphicsDevice.SetRenderTarget(gamePlayRenderTarget);
            sb.Begin();
            BulletSystem.Draw(gameTime, sb);
            Player.Draw(gameTime, sb);
            EnemySystem.Draw(gameTime, sb);
            sb.Draw(Resources.GameFrameUI, new Vector2(250, 0), Color.White);
            //sb.DrawString(Resources.DefaultFont, ScoreString, ScorePosition, ScoreColor, -float.Pi / 30, Vector2.Zero, 1, SpriteEffects.None, 0);
            sb.Draw(BlackPixel, LeftBlackBorder, Color.Black);
            sb.Draw(BlackPixel, RightBlackBorder, Color.Black);
            sb.End();

            //DrawUI
            //var healthBarRenderTarget = DrawHealthBar(sb);
            var gui = GUISystem.DrawGUI(sb);
            graphicsDevice.SetRenderTarget(LastFrame);

            //Draw all together
            sb.Begin();
            sb.Draw(gamePlayRenderTarget, Vector2.Zero, Color.White);
            sb.Draw(gui, new Vector2(1070,40), null, Color.White, -float.Pi / 30f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            sb.End();

            //Draw it to the screen
            graphicsDevice.SetRenderTarget(null);
            sb.Begin();
            sb.Draw(LastFrame, Vector2.Zero, Color.White);
            sb.End();

            gamePlayRenderTarget.Dispose();
            gui.Dispose();
        }

        public void Initialize()
        {
            BlackPixel = Resources.BlackPixel;
            LeftBlackBorder = new(0, 0, 250, GlobalVariables.Resolution.Y);
            RightBlackBorder = new(250 + Resources.GameFrameUI.Width, 0, 500, GlobalVariables.Resolution.Y);
            SpawnPlayer();
            //NOTE: This is temporal decision
            Level.FillInTimeLine();
            GUISystem.Initialize();
            Health = 5;
            Bombs = 0;
            HealthShards = 2;
            BombShards = 1;

            Level.OnEnemySpawned += (enemy) =>
            {
                EnemySystem.CreateEnemy(enemy);
                CollisionSystem.AddCollider(enemy.CollisionComponent);
                enemy.OnPatternGenerated += (bullets) =>
                {
                    foreach(var bullet in bullets)
                    {
                        BulletSystem.CreateEnemyBullet(bullet);
                        CollisionSystem.AddCollider(bullet.CollisionComponent);
                        bullet.OnDestroy += () => CollisionSystem.RemoveCollider(bullet.CollisionComponent);
                    }
                };

                enemy.OnDestroy += () => CollisionSystem.RemoveCollider(enemy.CollisionComponent);
                enemy.OnDeath += () =>
                {
                    Score += 100;
                    GUISystem.UpdateScore(Score);
                };
                enemy.Initialize();
                ControlSystem.BindKeyJustDownAction(Keys.Escape, Pause);
            };
        }

        public void Update(GameTime gameTime)
        {
            ControlSystem.Update(gameTime);
            Level.Update(gameTime);
            if(PlayerAlive)
                Player.Update(gameTime);
            PlayerRespawnAnimation.Update(gameTime);
            PlayerRespawnTimer.Update(gameTime);
            BulletSystem.Update(gameTime);
            CollisionSystem.Update();
            EnemySystem.Update(gameTime);
            GUISystem.Update(gameTime);
        }

        private void SpawnPlayer()
        {
            Player = new Player();
            Player.Initialize();
            Player.OnDestroy += () => CollisionSystem.RemoveCollider(Player.CollisionComponent);
            Player.OnHurt += () =>
            {
                Health -= 1;
                PlayerDeath();
            };
            Player.OnShoot += (position) =>
            {
                var bullet = BulletSystem.CreatePlayerBullet(position);
                CollisionSystem.AddCollider(bullet.CollisionComponent);
                bullet.OnDestroy += () => CollisionSystem.RemoveCollider(bullet.CollisionComponent);
            };

            PlayerRespawnAnimation = new Tween(Player.Position.Y + 200, Player.Position.Y, 0.5f, EasingType.CubicEaseOut);
            Player.Position.Y += 200;
            PlayerRespawnAnimation.OnUpdate += () =>
            {
                var newPosition = Player.Position.WithY(PlayerRespawnAnimation.Value);
                Player.Position = newPosition;
            };

            PlayerRespawnAnimation.OnFinish += () =>
            {
                CollisionSystem.AddCollider(Player.CollisionComponent);
                PlayerAlive = true;
                ConfigurePlayerControl();
            };

            PlayerRespawnAnimation.Start();
        }

        private void PlayerDeath()
        {
            Player.Position = new Vector2(-100, -100);
            Player.Destroy();
            UnbindPlayerControls();
            PlayerAlive = false;
            PlayerRespawnTimer = new(0, 0, 1);
            PlayerRespawnTimer.OnFinish += SpawnPlayer;
            PlayerRespawnTimer.Start();
        }


        //TODO: This should be another class that holds the Controls setting, and loads it from there, REMOVE LATER
        private void ConfigurePlayerControl()
        {
            ControlSystem.BindKeyDownAction(Keys.Left, (gameTime) => Player.ControlLeft());
            ControlSystem.BindKeyDownAction(Keys.Right, (gameTime) => Player.ControlRight());
            ControlSystem.BindKeyDownAction(Keys.Down, (gameTime) => Player.ControlDown());
            ControlSystem.BindKeyDownAction(Keys.Up, (gameTime) => Player.ControlUp());
            ControlSystem.BindKeyDownAction(Keys.Z, (gameTime) => Player.Shoot(gameTime));
            ControlSystem.BindKeyDownAction(Keys.LeftShift, (gameTime) => Player.Focus());
        }

        private void UnbindPlayerControls()
        {
            ControlSystem.UnbindKeyDown(Keys.Left);
            ControlSystem.UnbindKeyDown(Keys.Right);
            ControlSystem.UnbindKeyDown(Keys.Up);
            ControlSystem.UnbindKeyDown(Keys.Down);
            ControlSystem.UnbindKeyDown(Keys.Z);
            ControlSystem.UnbindKeyDown(Keys.LeftShift);
        }
    }
}
