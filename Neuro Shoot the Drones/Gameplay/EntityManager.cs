using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.ECS;
using Neuro_Shoot_the_Drones.Gameplay.Bullets;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Gameplay.Controls;
using Neuro_Shoot_the_Drones.Gameplay.Drawable;
using Neuro_Shoot_the_Drones.Gameplay.Enemies;
using Neuro_Shoot_the_Drones.Gameplay.Levels;
using Neuro_Shoot_the_Drones.Gameplay.Player;
using Neuro_Shoot_the_Drones.Timeline;
using Neuro_Shoot_the_Drones.Tweens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay
{
    internal class EntityManager
    {
        List<BaseEntity> Entities = new List<BaseEntity>();
        DrawableSystem DrawableSystem = new();
        MoveSystem MoveSystem = new();
        CollisionSystem CollisionSystem = new(ResolutionData.VisibleGameplayArea);
        PlayerSystem PlayerSystem = new();
        TimeLineSystem TimeLineSystem = new();
        HealthSystem HealthSystem = new();
        TweenSystem TweenSystem = new();
        EnemySystem EnemySystem = new();
        BulletSystem BulletSystem;
        Level DemoLevel = new();

        public ControlSystem ControlSystem;

        public EntityManager()
        {
            BulletSystem = new(PlayerSystem.Transform);
        }

        public void Initialize()
        {
            InitializePlayer();
            InitializeControls();
            InitializeLevel();
        }

        void InitializePlayer()
        {
            var playerDrawable = PlayerSystem.Player.GetComponent<Drawable.DrawableComponent>();
            var hitCircleDrawable = PlayerSystem.HitCircle.GetComponent<Drawable.DrawableComponent>();
            DrawableSystem.AddComponent(playerDrawable);
            DrawableSystem.AddComponent(hitCircleDrawable);
            MoveSystem.AddComponent(PlayerSystem.Move);
            PlayerSystem.OnShoot += SummonBullet;
            //TODO: Add player hitcircles
        }

        void InitializeControls()
        {
            ControlSystem.UnbindAll();
            ControlSystem.BindKeyHeld(Keys.Left, (gt) => PlayerSystem.MoveLeft());
            ControlSystem.BindKeyHeld(Keys.Up, (gt) => PlayerSystem.MoveUp());
            ControlSystem.BindKeyHeld(Keys.Right, (gt) => PlayerSystem.MoveRight());
            ControlSystem.BindKeyHeld(Keys.Down, (gt) => PlayerSystem.MoveDown());
            ControlSystem.BindKeyJustPress(Keys.LeftShift, () => PlayerSystem.EnterFocus());
            ControlSystem.BindKeyRelease(Keys.LeftShift, () => PlayerSystem.ExitFocus());
            ControlSystem.BindKeyHeld(Keys.Z, PlayerSystem.Shoot);
        }

        void InitializeLevel()
        {
            TimeLineSystem.AddComponent(DemoLevel.TimeLine);
            DemoLevel.OnEnemySpawned += SummonEnemy;
            DemoLevel.FillInTimeLine();
        }

        public RenderTarget2D Draw(SpriteBatch sb)
        {
            var graphicsDevice = sb.GraphicsDevice;
            var renderTarget = new RenderTarget2D(graphicsDevice, ResolutionData.Resolution.X, ResolutionData.Resolution.Y);
            graphicsDevice.SetRenderTarget(renderTarget);

            sb.Begin(sortMode: SpriteSortMode.BackToFront);
            DrawableSystem.Draw(sb);
            sb.End();

            return renderTarget;
        }

        public void Update(GameTime gameTime)
        {
            ControlSystem.Update(gameTime);
            PlayerSystem.Update(gameTime);
            MoveSystem.Update(gameTime);
            TweenSystem.Update(gameTime);
            TimeLineSystem.Update(gameTime);
            CollisionSystem.Update(gameTime);
            BulletSystem.Update();
            DrawableSystem.Update(gameTime);
            HealthSystem.Update(gameTime);
        }

        void SummonBullet(BaseBullet bullet)
        {
            var move = bullet.GetComponent<MoveComponent>();
            var drawable = bullet.GetComponent<Drawable.DrawableComponent>();    
            var collision = bullet.GetComponent<CollisionComponent>();
            MoveSystem.AddComponent(move);
            DrawableSystem.AddComponent(drawable);
            CollisionSystem.AddComponent(collision);    
            BulletSystem.AddBullet(bullet);
        }

        void SummonEnemy(Enemy enemy)
        {
            var collision = enemy.GetComponent<CollisionComponent>();
            var drawable = enemy.GetComponent<Drawable.DrawableComponent>();
            var health = enemy.GetComponent<HealthComponent>();
            var timeline = enemy.GetComponent<TimeLineComponent>();

            CollisionSystem.AddComponent(collision);
            DrawableSystem.AddComponent(drawable);
            HealthSystem.AddComponent(health);
            TimeLineSystem.AddComponent(timeline);

            enemy.OnAddTween += AddTween;
            enemy.OnShoot += (bullets) =>
            {
                foreach (var b in bullets)
                    SummonBullet(b);
            };

            EnemySystem.CreateEnemy(enemy);
        }

        void AddTween(Tween tween)
        {
            TweenSystem.AddTween(tween);
            tween.Start();
        }


        Enemy CreateTestEnemy()
        {
            var enemy = new Enemy(Resources.LightDrone, Resources.LightDrone.Bounds,
                                     Vector2.One / 1.3f, ResolutionData.PlayerInitialPosition + new Vector2(0, -300),
                                     health: 50, hitCircleSize:16);
            return enemy;
        }
    }
}
