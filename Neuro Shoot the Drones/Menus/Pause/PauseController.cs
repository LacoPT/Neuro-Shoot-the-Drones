using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neuro_Shoot_the_Drones.MVC;

namespace Neuro_Shoot_the_Drones.Menus.Pause
{
    internal class PauseController : BaseController
    {
        private readonly ControlSystem controlSystem;
        private readonly PauseModel model;

        public PauseController(ControlSystem controlSystem, PauseModel model, PauseView view) : base(model, view)
        {
            this.controlSystem = controlSystem;
            this.model = model;

            controlSystem.BindKeyJustPress(Keys.Escape, model.Unpause);
            controlSystem.BindKeyJustPress(Keys.Down, model.SelectNext);
            controlSystem.BindKeyJustPress(Keys.Up, model.SelectPrevious);
            controlSystem.BindKeyJustPress(Keys.Enter, model.ActivateSelected);
            controlSystem.BindKeyJustPress(Keys.Z, model.ActivateSelected);
        }

        public void Update(GameTime gameTime)
        {
            controlSystem.Update(gameTime);
        }
    }
}
