using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Neuro_Shoot_the_Drones.Gameplay.GUI
{
    internal class GUIView : BaseView
    {
        readonly GUIModel Model;
        private enum HealthBarTile
        {
            Empty,
            Heart,
            ShardHealth,
            TwoShardsHeart,
            ThreeShardsHeart,
            Bomb,
            ShardBomb,
            TwoShardsBomb,
            ThreeShardsBomb
        }

        public readonly Vector2 ScorePosition = new Vector2(25, 100);
        public readonly int HealthBarMargin = 10;
        public readonly Vector2 HealthPosition = new Vector2(10, 400);
        public readonly Vector2 BombsPosition = new Vector2(10, 500);
        const int MaxHealth = 6;
        const int MaxBombs = 6;

        readonly Color ScoreColor = new Color(255, 239, 239);
        Texture2D HealthBarAtlas;
        //It is needed because i couldn't tile the atlas properly
        readonly Dictionary<HealthBarTile, Rectangle> TileMap = new()
        {
            { HealthBarTile.Empty, new Rectangle(0, 0, 80, 85) },
            { HealthBarTile.Heart, new Rectangle(82, 0, 80, 85) },
            { HealthBarTile.ShardHealth, new Rectangle(164, 0, 80, 85) },
            { HealthBarTile.TwoShardsHeart, new Rectangle(246, 0, 80, 85) },
            { HealthBarTile.ThreeShardsHeart, new Rectangle(327, 0, 80, 85) },
            { HealthBarTile.Bomb, new Rectangle(406, 0, 80, 85) },
            { HealthBarTile.ShardBomb, new Rectangle(487, 0, 80, 85) },
            { HealthBarTile.TwoShardsBomb, new Rectangle(571, 0, 80, 85) },
            { HealthBarTile.ThreeShardsBomb, new Rectangle(653, 0, 80, 85) }
        };

        public GUIView(GUIModel model)
        {
            this.Model = model;
            HealthBarAtlas = Resources.HealthBarAtlas;
        }

        public override RenderTarget2D Draw(SpriteBatch sb)
        {
            var gd = sb.GraphicsDevice;
            var renderTarget = new RenderTarget2D(gd, 600, 900);
            var ScoreString = $"HIGH SCORE\n{Model.DisplayScore:D9}\nSCORE\n{Model.DisplayScore:D9}";
            gd.SetRenderTarget(renderTarget);
            gd.Clear(Color.Transparent);

            sb.Begin();

            sb.DrawString(Resources.DefaultFont, ScoreString, ScorePosition, ScoreColor, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            DrawHealth(sb);
            DrawBombs(sb);


            sb.End();
            gd.SetRenderTarget(null);
            return renderTarget;
        }

        void DrawHealth(SpriteBatch sb)
        {
            float currentX = HealthPosition.X;
            var health = Model.Health;
            var shards = Model.HealthShards;

            for (int i = 0; i < MaxHealth; i++)
            {
                HealthBarTile tile;
                if (i < health)
                {
                    tile = HealthBarTile.Heart;
                }
                else if (i == health && shards > 0)
                {
                    switch (shards)
                    {
                        case 1: tile = HealthBarTile.ShardHealth; break;
                        case 2: tile = HealthBarTile.TwoShardsHeart; break;
                        case 3: tile = HealthBarTile.ThreeShardsHeart; break;
                        default: tile = HealthBarTile.Empty; break;
                    }
                }
                else
                {
                    tile = HealthBarTile.Empty;
                }

                Rectangle sourceRect = TileMap[tile];
                Vector2 pos = new Vector2(currentX, HealthPosition.Y);
                sb.Draw(HealthBarAtlas, pos, sourceRect, Color.White);
                currentX += sourceRect.Width + HealthBarMargin;
            }
        }

        void DrawBombs(SpriteBatch sb)
        {
            var bombs = Model.Bombs;
            var shards = Model.BombShards;
            var currentX = BombsPosition.X;
            for (int i = 0; i < MaxBombs; i++)
            {
                HealthBarTile tile;
                if (i < bombs)
                {
                    tile = HealthBarTile.Bomb;
                }
                else if (i == bombs && shards > 0)
                {
                    switch (shards)
                    {
                        case 1: tile = HealthBarTile.ShardBomb; break;
                        case 2: tile = HealthBarTile.TwoShardsBomb; break;
                        case 3: tile = HealthBarTile.ThreeShardsBomb; break;
                        default: tile = HealthBarTile.Empty; break;
                    }
                }
                else
                {
                    tile = HealthBarTile.Empty;
                }

                Rectangle sourceRect = TileMap[tile];
                Vector2 pos = new Vector2(currentX, BombsPosition.Y);
                sb.Draw(HealthBarAtlas, pos, sourceRect, Color.White);
                currentX += sourceRect.Width + HealthBarMargin;
            }
        }
    }
}
