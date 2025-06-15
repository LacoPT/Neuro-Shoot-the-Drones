using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Menus.Main
{
    internal class MainMenuScene : IGameScene
    {
        MainMenuModel model;
        MainMenuView view;
        MainMenuController controller;

        public event Action OnStart;
        public event Action OnExit;

        public MainMenuScene()
        {
            model = new();
            view = new(model);
            controller = new(new ControlSystem(), model, view);
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
            model.OnStart += () => OnStart();
            model.OnExit += () => OnExit();
        }

        public void Update(GameTime gameTime)
        {
            controller.Update(gameTime);
        }
    }
}
