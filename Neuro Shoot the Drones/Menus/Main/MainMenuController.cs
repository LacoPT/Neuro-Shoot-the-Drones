using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.Gameplay.Controls;
using Neuro_Shoot_the_Drones.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Menus.Main
{
    internal class MainMenuController : BaseController
    {
        MainMenuModel model;
        ControlSystem ControlSystem;
        public MainMenuController(ControlSystem controlSystem, MainMenuModel model, MainMenuView view) : base(model, view)
        {
            this.model = model;
            this.ControlSystem = controlSystem;

            controlSystem.BindKeyJustPress(Keys.Down, model.SelectNext);
            controlSystem.BindKeyJustPress(Keys.Up, model.SelectPrevious);
            controlSystem.BindKeyJustPress(Keys.Enter, model.ActivateSelected);
            controlSystem.BindKeyJustPress(Keys.Z, model.ActivateSelected);
        }

        public void Update(GameTime gameTime)
        {
            ControlSystem.Update(gameTime);
        }
    }
}
