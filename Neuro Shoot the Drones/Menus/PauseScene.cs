using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Menus
{
    //TODO: Use MVC for UI
    internal class PauseScene : IGameScene
    {
        RenderTarget2D GamePlayLastFrame;

        public delegate void UnpauseEventHandler();
        public event UnpauseEventHandler OnUnpause;
        public delegate void ExitEventHandler();
        public event ExitEventHandler OnExit;

        ControlSystem ControlSystem = new();
        Effect grayscale;
        List<PauseMenuElement> PauseMenuOptions = new List<PauseMenuElement>()
        {
            new(new Vector2(100, 100), "Continue"),
            new(new Vector2(100, 200), "Settings"),
            new(new Vector2(100, 300), "Quit")
        };
        int SelectedOption = 0;

        public PauseScene()
        {
            PauseMenuOptions[0].Selected = true;
            PauseMenuOptions[0].OnActivated += Unpause;
            PauseMenuOptions[2].OnActivated += Exit;
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Begin(effect: grayscale);
            sb.Draw(GamePlayLastFrame, Vector2.Zero, Color.White);
            sb.End();
            sb.Begin();
            sb.DrawString(Resources.DefaultFont, "ON PAUSE", Vector2.Zero, Color.White);
            foreach (var option in PauseMenuOptions)
            {
                option.Draw(sb);
            }
            sb.End();
        }

        public void Initialize()
        {
            ControlSystem.BindKeyJustDownAction(Keys.Escape, Unpause);
            ControlSystem.BindKeyJustDownAction(Keys.Down, NextOption);
            ControlSystem.BindKeyJustDownAction(Keys.Up, PreviousOption);
            ControlSystem.BindKeyJustDownAction(Keys.Enter, () =>
            {
                PauseMenuOptions[SelectedOption].Activate();
            });
            grayscale = Resources.GrayScale;
            grayscale.Parameters["Intensity"].SetValue(0.8f);
        }

        void NextOption()
        {
            PauseMenuOptions[SelectedOption].Selected = false;
            SelectedOption++;
            SelectedOption %= PauseMenuOptions.Count;
            PauseMenuOptions[SelectedOption].Selected = true;
        }

        void PreviousOption()
        {
            PauseMenuOptions[SelectedOption].Selected = false;
            SelectedOption--;
            if(SelectedOption < 0)
                SelectedOption = PauseMenuOptions.Count - 1;
            PauseMenuOptions[SelectedOption].Selected = true;
        }


        public void SetLastFrame(RenderTarget2D lastFrame)
        {
            GamePlayLastFrame = lastFrame;
        }

        public void Update(GameTime gameTime)
        {
            ControlSystem.Update(gameTime);
        }

        private void Unpause()
        {
            OnUnpause();
        }

        private void Exit()
        {
            OnExit?.Invoke();
        }
    }
}
