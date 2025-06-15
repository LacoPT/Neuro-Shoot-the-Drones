using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.ECS;
using Neuro_Shoot_the_Drones.Gameplay.Bullets;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Gameplay.Drawable;
using Neuro_Shoot_the_Drones.Tweens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Neuro_Shoot_the_Drones.Gameplay.Player
{
    internal class PlayerSystem
    {
        public readonly BaseEntity Player = new();
        public readonly BaseEntity HitCircle = new();
        public readonly BaseEntity GrazeArea = new();
        public readonly BaseEntity CollectArea = new();
        List<BaseEntity> Companions = new();
        public readonly TransformComponent Transform;
        public readonly MoveComponent Move;

        static readonly Vector2[] Directions = new Vector2[]
        {
            new(-1, 0),
            new(0, -1),
            new(1, 0),
            new(0, 1)
        };

        Rectangle PlayerAllowedArea;

        //TODO: Extract into class/struct (there is no other character yet)
        #region CharacterStats
        const int HitCircleSize = 4;
        const int GrazeAreaSize = 8;
        const int CollectAreaSize = 64;
        const float Speed = 600;
        const float FocusedSlowDownCoefficient = 0.4f;
        const double FireRate = 12;
        readonly double TimeToShoot = 1 / FireRate;
        readonly Rectangle TextureSourceRect = new Rectangle(new(400, 0), new(200, 300));
        readonly Vector2 TextureScale = new Vector2(100f / 200, 1.3f / 3);
        readonly Vector2 StartPosition;
        #endregion


        public delegate void ShootEventHalder(PlayerBullet bullet);
        public event ShootEventHalder OnShoot;

        public delegate void HurtEventHandler();
        public event HurtEventHandler OnHurt;

        public delegate void GrazeEventHandler();
        public event GrazeEventHandler OnGraze;

        //TODO: Add parameter: type of collected
        public delegate void CollectEventHandler();
        public event CollectEventHandler OnCollect;

        double ShootTimer = 0;
        bool SkipInput = false;
        bool Respawning = false;
        bool Focused = false;
        Vector2 Direction = Vector2.Zero;
        int PowerLevel = 0;

        Tween RespawnTimer = new(0, 0);
        Tween RespawnAnimation = new(0, 0);

        public PlayerSystem()
        {
            PlayerAllowedArea = ResolutionData.VisibleGameplayArea.Grow(-HitCircleSize);
            StartPosition = ResolutionData.PlayerInitialPosition; 
            Transform = new TransformComponent(Player);
            Transform.Position = StartPosition;
            Player.AddComponent(Transform);
            HitCircle.AddComponent(Transform);
            GrazeArea.AddComponent(Transform);
            CollectArea.AddComponent(Transform);
            Move = new MoveComponent(Player);
            Player.AddComponent(Move);
            HitCircle.AddComponent(Move);
            GrazeArea.AddComponent(Move);
            CollectArea.AddComponent(Move);
            var hitCirlcleCollision = new CollisionComponent(HitCircleSize, CollisionLayers.EnemyBullet | CollisionLayers.Enemy,
                                                             CollisionLayers.Player, new(0), HitCircle);
            hitCirlcleCollision.OnCollisionRegistered += (data) => Hurt();
            HitCircle.AddComponent(hitCirlcleCollision);
            var grazeAreaCollision = new CollisionComponent(GrazeAreaSize, CollisionLayers.None, CollisionLayers.PlayerGraze, new(0), GrazeArea);
            GrazeArea.AddComponent(grazeAreaCollision);
            var collectAreaCollision = new CollisionComponent(CollectAreaSize, CollisionLayers.Pickup, CollisionLayers.PlayerCollectZone, new(0), CollectArea);
            CollectArea.AddComponent(collectAreaCollision);

            var drawable = new Drawable.DrawableComponent(Player, Resources.PlayerTextureAtlas, TextureSourceRect,
                                                    TextureSourceRect.GetRelativeCenter(), TextureScale, transform: Transform, layerDepth: 0.1f);
            Player.AddComponent(drawable);
            var hitCircleDrawable = new Drawable.DrawableComponent(HitCircle, Resources.HitCircle, Resources.HitCircle.Bounds,
                                                             Resources.HitCircle.Bounds.GetRelativeCenter(), Vector2.One, transform: Transform, layerDepth: 0.0f);
            HitCircle.AddComponent(hitCircleDrawable);
        }

        public void Initialize()
        {
            PlayerPosAccess.Set(Transform);
        }

        public void Update(GameTime gameTime)
        {
            if(Respawning)
            {
                RespawnTimer.Update(gameTime);
                RespawnAnimation.Update(gameTime);
                return;
            }
            Move.Velocity = Vector2.Zero;
            if (Direction != Vector2.Zero)
            {
                Direction.Normalize();
                float currentSpeed = Speed * (Focused ? (1 - FocusedSlowDownCoefficient) : 1);
                Move.Velocity = Direction * currentSpeed;
                Transform.Position = Transform.Position.RectClamp(PlayerAllowedArea);
            }

            HitCircle.GetComponent<Drawable.DrawableComponent>().Skip = !Focused;
            ResetDirection();
        }

        public void MoveLeft()
        {
            if (SkipInput) return;
            Direction += Directions[0];
        }

        public void MoveUp()
        {
            if (SkipInput) return;
            Direction += Directions[1];
        }

        public void MoveRight()
        {
            if (SkipInput) return;
            Direction += Directions[2];
        }


        public void MoveDown()
        {
            if (SkipInput) return;
            Direction += Directions[3];
        }

        public void Shoot(GameTime gameTime)
        {
            if(SkipInput) return;
            ShootTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (ShootTimer >= TimeToShoot)
            {
                ShootTimer = 0;

                Vector2 offset = new(6, 0);
                OnShoot?.Invoke(PlayerBulletFactory.CreateStandartPlayerBullet(Transform.Position + offset));
                OnShoot?.Invoke(PlayerBulletFactory.CreateStandartPlayerBullet(Transform.Position - offset));
            }
        }

        public void EnterFocus()
        {
            if (SkipInput) return;
            Focused = true;
        }

        public void ExitFocus()
        {
            Focused = false;
        }

        public void ResetDirection()
        {
            Direction = Vector2.Zero;
        }

        public void Hurt()
        {
            OnHurt();
            Move.Velocity = Vector2.Zero;
            Transform.Position = ResolutionData.PlayerInitialPosition + new Vector2(0, 300);
            HitCircle.GetComponent<CollisionComponent>().Skip = true;
            CollectArea.GetComponent<CollisionComponent>().Skip = true;
            GrazeArea.GetComponent<CollisionComponent>().Skip = true;
            SkipInput = true;
            Respawning = true;
            RespawnTimer = new(0, 1, 1.5);
            RespawnTimer.OnFinish += Respawn;
            RespawnTimer.Start();
        }

        public void Respawn()
        {
            RespawnAnimation = new(Transform.Position.Y, StartPosition.Y, 0.6, EasingType.QuarticEaseOut);
            RespawnAnimation.OnUpdate += () => Transform.Position = Transform.Position.WithY(RespawnAnimation.Value);
            RespawnAnimation.OnFinish += () =>
            {
                HitCircle.GetComponent<CollisionComponent>().Skip = false;
                CollectArea.GetComponent<CollisionComponent>().Skip = false;
                GrazeArea.GetComponent<CollisionComponent>().Skip = false;
                SkipInput = false;
                Respawning = false;
            };
            RespawnAnimation.Start();
        }
    }
}
