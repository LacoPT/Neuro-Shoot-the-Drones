using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay.Controls;
using Neuro_Shoot_the_Drones.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Menus.Pause
{
    internal class PauseScene : IGameScene
    {
        private readonly PauseModel model;
        private readonly PauseView view;
        private readonly PauseController controller;


        public event Action OnUnpause;
        public event Action OnExit;
        public event Action OnBackToMainMenu;

        public PauseScene()
        {
            model = new PauseModel();
            view = new PauseView(model);
            controller = new PauseController(new ControlSystem(), model, view);

            model.OnUnpause += () => OnUnpause?.Invoke();
            model.OnExit += () => OnExit?.Invoke();
            model.OnBackToMainMenu += () => OnBackToMainMenu?.Invoke();
        }

        public void SetLastFrame(RenderTarget2D lastFrame)
        {
            model.GamePlayLastFrame = lastFrame;
        }

        public void Update(GameTime gameTime)
        {
            controller.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            var ui = view.Draw(sb);

            sb.Begin();
            sb.Draw(ui, Vector2.Zero, Color.White);
            sb.End();

            ui.Dispose();
        }

        public void Initialize()
        {
            view.Initialize();
        }
    }
}
