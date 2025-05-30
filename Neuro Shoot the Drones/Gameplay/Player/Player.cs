using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay
{
    //TODO: Extract some methods to MoveComponent or smth
    //BUG: There might be a bug, where HitCircleSize is not set anywhere in the code
    internal class Player : GameEntity
    {
        //Pixels per seconds
        private const int Speed = 400;
        private const float FocusedSlowDownCoofficient = 0.4f;
        //Shots per seconds
        private const double Firerate = 12;
        private double TimeToShoot { get => 1 / Firerate; }
        private double ShootTimer = 0;

        //TODO: Think of a way to pass differen types of bullets to events
        //OR MAYBE this could be the companion's work
        public delegate void ShootEventHandler(Vector2 Position);
        public event ShootEventHandler OnShoot;
        public delegate void HurtEventHandler();
        public event HurtEventHandler OnHurt;

        private readonly static Vector2[] Directions = new Vector2[]
        {
            new(-1, 0),
            new(1, 0),
            new(0, -1),
            new(0, 1)
        };
        public readonly static Vector2 StartPosition = ResolutionData.PlayerInitialPosition;
        public Vector2 Direction { get; private set; } = new();
        //TODO: Hitcircle appear animation
        public bool Focused { get; private set; }
        bool DisplayHitcircle = false;

        private static Rectangle PlayerAllowedArea;
        private DrawableComponent hitcircle;

        public Player()
        {
            //May be too big right now, i'll change it later
            HitCircleSize = 4;
            CollisionComponent = new(HitCircleSize, CollisionLayers.EnemyBullet, CollisionLayers.Player, new CollisionData(0));
            CollisionComponent.OnCollisionRegistered += (data) => Hurt();
        }

        public override void Initialize()
        {
            PlayerAllowedArea = ResolutionData.VisibleGameplayArea.Grow(-HitCircleSize);
            var textureSourceRect = new Rectangle(Point.Zero, new(200, 300));
            Position = StartPosition;
            DrawableComponent = new(Resources.PlayerTextureAtlas,
                                    textureSourceRect, 
                                    new Vector2(75f / 200, 1f / 3),
                                    textureSourceRect.GetRelativeCenter());

            hitcircle = new(Resources.Hitcirle,
                            Resources.Hitcirle.Bounds,
                            Vector2.One,
                            Resources.Hitcirle.Bounds.GetRelativeCenter());

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Direction.Normalize();
            var currentSpeed = Speed * (1 - (Convert.ToInt32(Focused) * FocusedSlowDownCoofficient));
            Position += Direction * (float)gameTime.ElapsedGameTime.TotalSeconds * currentSpeed;
            Position = Position.RectClamp(PlayerAllowedArea);
            DisplayHitcircle = Focused;
            ResetState();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
            if(DisplayHitcircle)
            {
                hitcircle.Draw(gameTime, sb, Position, 0f);
            }
        }

        //NOTE: So we can record player's actions to a replay
        public void ControlLeft() => Direction += Directions[0];
        public void ControlRight() => Direction += Directions[1];
        public void ControlUp() => Direction += Directions[2];
        public void ControlDown() => Direction += Directions[3];

        public void Shoot(GameTime gameTime)
        {
            if(ShootTimer >= TimeToShoot)
            {
                ShootTimer = 0;
                Vector2 indent = new(6, 0);

                OnShoot?.Invoke(Position + indent);
                OnShoot?.Invoke(Position - indent);
            }    
            ShootTimer += gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void Focus()
        {
            Focused = true;
        }

        //TODO: Make maybe some events for this, to animate support fires
        private void EnterFocus()
        {
        }

        private void ExitFocus()
        {
        }

        private void Hurt()
        {
            OnHurt();
        }

        //TODO: Rename
        private void ResetState()
        {
            Direction = Vector2.Zero;
            Focused = false;
        }
    }
}
