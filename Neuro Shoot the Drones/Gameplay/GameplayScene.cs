using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay.Controls;
using Neuro_Shoot_the_Drones.Gameplay.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay
{
    internal class GameplayScene : IGameScene
    {
        EntityManager EntityManager = new();
        ControlSystem ControlSystem = new();
        GUIModel GUIModel;
        GUIView GUIView;
        GUIController GUIController;

        public GameplayScene()
        {
            GUIModel = new();
            GUIView = new(GUIModel);
            GUIController = new(GUIModel, GUIView);
            EntityManager.OnEnemyDied += (data) => GUIModel.Score += data.Score;
            EntityManager.OnPlayerHurt += () => GUIModel.Health -= 1;
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            var gameplay = EntityManager.Draw(sb);
            var gui = GUIView.Draw(sb);
            var LeftBlackBorder = new Rectangle(0, 0, 250, ResolutionData.Resolution.Y);
            var RightBlackBorder = new Rectangle(250 + Resources.GameFrameUI.Width, 0, 500, ResolutionData.Resolution.Y);
            sb.Begin();
            sb.Draw(gameplay, Vector2.Zero, Color.White);
            sb.Draw(Resources.GameFrameUI, new Vector2(250, 0), Color.White);
            sb.Draw(gui, new Vector2(1070, 40), null, Color.White, -float.Pi / 30f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            sb.Draw(Resources.BlackPixel, LeftBlackBorder, Color.Black);
            sb.Draw(Resources.BlackPixel, RightBlackBorder, Color.Black);
            sb.End();
            gameplay.Dispose();
            gui.Dispose();
        }

        public void Initialize()
        {
            EntityManager.ControlSystem = ControlSystem;
            EntityManager.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            EntityManager.Update(gameTime);
            GUIController.Update(gameTime);
        }
    }
}
