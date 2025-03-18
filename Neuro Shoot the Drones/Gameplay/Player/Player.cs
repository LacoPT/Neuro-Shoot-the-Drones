using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal class Player : IGameObject
    {
        private const int HitCircleSize = 5;
        //Pixels per seconds
        private const int Speed = 400;
        private const float FocusedSlowDownCoofficient = 0.4f;
        //Shots per seconds
        private const double Firerate = 12;
        private double TimeToShoot { get => 1 / Firerate; }
        private double ShootTimer = 0;
        private readonly static Vector2[] Directions = new Vector2[]
        {
            new(-1, 0),
            new(1, 0),
            new(0, -1),
            new(0, 1)
        };
        public Vector2 Direction { get; private set; } = new();

        public Vector2 Position { get; private set; } = new();
        public bool Focused { get; private set; }

        public Texture2D TextureAtlas { get; private set; }
        private Rectangle TextureSourceRect = new(Point.Zero, new(200, 300));
        private Vector2 TextureScale = new Vector2(75f / 200, 1f / 3);
        private Vector2 RelativeDestinationCenter = Vector2.Zero;
        private static Rectangle PlayerAllowedArea = GlobalVariables.VisibleGameplayArea.Grow(-HitCircleSize);

        public void Initialize()
        {
            TextureAtlas = Resources.PlayerTextureAtlas;
            RelativeDestinationCenter = TextureSourceRect.GetRelativeCenter(scale: TextureScale);
        }

        public void Update(GameTime gameTime)
        {
            Direction.Normalize();
            var currentSpeed = Speed * (1 - (Convert.ToInt32(Focused) * FocusedSlowDownCoofficient));
            Position += Direction * (float)gameTime.ElapsedGameTime.TotalSeconds * currentSpeed;
            Position = Position.RectClamp(PlayerAllowedArea);
            ResetState();
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(texture: TextureAtlas,
                    position: Position - RelativeDestinationCenter,
                    sourceRectangle: TextureSourceRect,
                    color: Color.White,
                    rotation: 0f,
                    origin: Vector2.Zero,
                    effects: SpriteEffects.None,
                    scale: TextureScale,
                    layerDepth: 0f);
        }

        //So we can record player's actions to a replay
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
                //BulletHell.CreatePlayerBullet(PlayerBulletFactory.CreateStandartPlayerBullet(Position + indent));
                //BulletHell.CreatePlayerBullet(PlayerBulletFactory.CreateStandartPlayerBullet(Position - indent));
                BulletHell.CreatePlayerBullet(Position + indent);
                BulletHell.CreatePlayerBullet(Position - indent);
            }    
            ShootTimer += gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Focus()
        {
            if(!Focused)
            {
                EnterFocus();
            }
        }

        private void EnterFocus()
        {
            Focused = true;
        }

        public void ExitFocus()
        {
            Focused = false;
        }

        private void ResetState()
        {
            Direction = Vector2.Zero;
        }
    }
}
