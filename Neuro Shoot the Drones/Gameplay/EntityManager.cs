using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.ECS;
using Neuro_Shoot_the_Drones.Gameplay.Bosses;
using Neuro_Shoot_the_Drones.Gameplay.Bullets;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Gameplay.Controls;
using Neuro_Shoot_the_Drones.Gameplay.Drawable;
using Neuro_Shoot_the_Drones.Gameplay.Enemies;
using Neuro_Shoot_the_Drones.Gameplay.Levels;
using Neuro_Shoot_the_Drones.Gameplay.PickUps;
using Neuro_Shoot_the_Drones.Gameplay.Player;
using Neuro_Shoot_the_Drones.Timeline;
using Neuro_Shoot_the_Drones.Tweens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
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
        PickUpSystem PickUpSystem = new();
        BulletSystem BulletSystem;
        Level DemoLevel = new();

        public delegate void PlayerHurtEventHandler();
        public event PlayerHurtEventHandler OnPlayerHurt;

        public delegate void EnemyDiedhEventHandler(EnemyDeathDataComponent data);
        public event EnemyDiedhEventHandler OnEnemyDied;

        public event Action LevelCompleted;

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

            CollisionSystem.AddComponent(PlayerSystem.HitCircle.GetComponent<CollisionComponent>());
            CollisionSystem.AddComponent(PlayerSystem.CollectArea.GetComponent<CollisionComponent>());
            CollisionSystem.AddComponent(PlayerSystem.GrazeArea.GetComponent<CollisionComponent>());
            PlayerSystem.OnHurt += OnPlayerHurt.Invoke;
            PlayerSystem.Initialize();
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
            DemoLevel.OnBossSpawned += SummonBoss;
            DemoLevel.OnLevelEnded += () => LevelCompleted?.Invoke();
            DemoLevel.FillInDemo();
            //DemoLevel.FillInTest();
        }

        public RenderTarget2D Draw(SpriteBatch sb)
        {
            var graphicsDevice = sb.GraphicsDevice;
            var renderTarget = new RenderTarget2D(graphicsDevice, ResolutionData.Resolution.X, ResolutionData.Resolution.Y);
            graphicsDevice.SetRenderTarget(renderTarget);

            sb.Begin(sortMode: SpriteSortMode.BackToFront);
            DrawableSystem.Draw(sb);
            sb.End();

            graphicsDevice.SetRenderTarget(null);
            return renderTarget;
        }

        public void Update(GameTime gameTime)
        {
            PlayerSystem.Update(gameTime);
            MoveSystem.Update(gameTime);
            TweenSystem.Update(gameTime);
            TimeLineSystem.Update(gameTime);
            CollisionSystem.Update(gameTime);
            BulletSystem.Update(gameTime);
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
            var transform = enemy.GetComponent<TransformComponent>();

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
            enemy.OnDeath += (data) =>
            {
                OnEnemyDied(data);
                SummonPickups(data.Drop, transform.Position);
            };

            EnemySystem.CreateEnemy(enemy);
        }

        void AddTween(Tween tween)
        {
            TweenSystem.AddTween(tween);
            tween.Start();
        }

        void SummonPickups(List<PickUp> pickUps, Vector2 position)
        {
            const float spreadRate = 0.4f;
            var playerTransform = PlayerSystem.Transform;
            var rotation = -float.Pi / 2;
            var spread = MathF.PI * (1 - MathF.Exp(-spreadRate * (pickUps.Count - 1)));
            var startAngle = -spread / 2;
            var step = spread / (pickUps.Count - 1);
            rotation += startAngle;
            foreach(var pickup in pickUps)
            {
                PickUpSystem.AddPickup(pickup);
                var drawable = pickup.GetComponent<DrawableComponent>();
                var collision = pickup.GetComponent<CollisionComponent>();
                var move = pickup.GetComponent<MoveComponent>();
                var transform = pickup.GetComponent<TransformComponent>();
                transform.Position = position;

                move.Acceleration = new(900f, 0);
                transform.Rotation = rotation;
                rotation += step;
                var timer = new Tween(0, 1, 0.4);
                TweenSystem.AddTween(timer);
                timer.OnFinish += () =>
                {
                    transform.Rotation = 0;
                    move.Velocity = Vector2.Zero;
                    move.Acceleration = new(0, 250f);
                };
                timer.Start();

                collision.OnCollisionRegistered += (data) =>
                {
                    collision.Skip = true;
                    var tweenX = new Tween(transform.Position.X, playerTransform.Position.X, 0.1);
                    var tweenY = new Tween(transform.Position.Y, playerTransform.Position.Y, 0.1);
                    tweenX.OnUpdate += () => transform.Position.X = tweenX.Value;
                    tweenY.OnUpdate += () => transform.Position.Y = tweenY.Value;
                    AddTween(tweenX);
                    AddTween(tweenY);
                    tweenX.Start();
                    tweenY.Start();
                    tweenX.OnFinish += () =>
                    {
                        pickup.Destroy();
                    };
                };

                DrawableSystem.AddComponent(drawable);
                MoveSystem.AddComponent(move);
                CollisionSystem.AddComponent(collision);
            }
        }

        void SummonBoss(Boss boss)
        {
            var collision = boss.GetComponent<CollisionComponent>();
            var drawable = boss.GetComponent<Drawable.DrawableComponent>();
            var transform = boss.GetComponent<TransformComponent>();


            CollisionSystem.AddComponent(collision);
            DrawableSystem.AddComponent(drawable);

            boss.OnAddTween += AddTween;
            boss.PhaseStarted += (phase) =>
            {
                TimeLineSystem.AddComponent(phase.TimeLine);
                HealthSystem.AddComponent(phase.Health);
                collision.OnCollisionRegistered += (data) =>
                {
                    phase.Health.Hurt(data.Damage);
                };
                phase.Health.OnDeath += () =>
                {
                    TimeLineSystem.RemoveComponent(phase.TimeLine);
                    TimeLineSystem.RemoveComponent(phase.Health);
                    boss.NextPhase();
                };
                phase.TimeLine.Start();
            };
            boss.OnFightEnded += () =>
            {
                DrawableSystem.RemoveComponent(drawable);
                CollisionSystem.RemoveComponent(collision);
            };
            boss.NextPhase();
        }


        Enemy CreateTestEnemy()
        {
            var enemy = new Enemy(Resources.LightDrone, Resources.LightDrone.Bounds,
                                     Vector2.One / 1.3f, ResolutionData.PlayerInitialPosition + new Vector2(0, -300),
                                     health: 50, hitCircleSize:16);
            return enemy;
        }

        List<PickUp> CreateTestPickups()
        {
            var startPosition = ResolutionData.PlayerInitialPosition - new Vector2(0, 500);
            return new List<PickUp>()
            { 
                new(startPosition, PickUpType.PowerSmall), new(startPosition, PickUpType.PowerSmall), new(startPosition, PickUpType.PowerSmall),
                new(startPosition, PickUpType.PowerSmall), new(startPosition, PickUpType.PowerSmall), new(startPosition, PickUpType.PowerSmall)
            };
        }
    }
}
