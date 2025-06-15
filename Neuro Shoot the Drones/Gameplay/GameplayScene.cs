using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.Gameplay.Controls;
using Neuro_Shoot_the_Drones.Gameplay.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        RenderTarget2D lastFrame;

        Rectangle LeftBlackBorder = new Rectangle(0, 0, 250, ResolutionData.Resolution.Y);
        Rectangle RightBlackBorder = new Rectangle(250 + Resources.GameFrameUI.Width, 0, 500, ResolutionData.Resolution.Y);

        public delegate void PauseEventHandler(RenderTarget2D lastFrame);
        public event PauseEventHandler OnPause;
        public delegate void EndedEventHandler(GameResultData data);
        public event EndedEventHandler OnEnded;

        public GameplayScene()
        {
            GUIModel = new();
            GUIView = new(GUIModel);
            GUIController = new(GUIModel, GUIView);
            EntityManager.OnEnemyDied += (data) => GUIModel.Score += data.Score;
            EntityManager.OnPlayerHurt += () =>
            {
                if(GUIModel.Health == 0)
                {
                    var data = new GameResultData(false, GUIModel.Score);
                    OnEnded(data);
                }
                GUIModel.Health -= 1;
            };
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {

            var gameplay = EntityManager.Draw(sb);
            var gui = GUIView.Draw(sb);
            var gd = sb.GraphicsDevice;

            gd.SetRenderTarget(lastFrame);

            sb.Begin();
            sb.Draw(gameplay, Vector2.Zero, Color.White);
            sb.Draw(Resources.GameFrameUI, new Vector2(250, 0), Color.White);
            sb.Draw(gui, new Vector2(1070, 40), null, Color.White, -float.Pi / 30f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            sb.Draw(Resources.BlackPixel, LeftBlackBorder, Color.Black);
            sb.Draw(Resources.BlackPixel, RightBlackBorder, Color.Black);
            sb.End();
            gameplay.Dispose();
            gui.Dispose();

            gd.SetRenderTarget(null);
            sb.Begin();
            sb.Draw(lastFrame, Vector2.Zero, Color.White);
            sb.End();
        }

        public void Initialize()
        {
            EntityManager.ControlSystem = ControlSystem;
            EntityManager.Initialize();
            ControlSystem.BindKeyJustPress(Keys.Escape, () =>
            {
                OnPause(lastFrame);
            });
            EntityManager.LevelCompleted += () =>
            {
                var data = new GameResultData(true, GUIModel.Score);
                OnEnded(data);
            };
        }

        public void InitializeLastFrame(SpriteBatch sb)
        {
            lastFrame = new RenderTarget2D(sb.GraphicsDevice, ResolutionData.Resolution.X, ResolutionData.Resolution.Y);
        }

        public void Update(GameTime gameTime)
        {
            ControlSystem.Update(gameTime);
            EntityManager.Update(gameTime);
            GUIController.Update(gameTime);
        }
    }
}
