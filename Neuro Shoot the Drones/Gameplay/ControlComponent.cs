using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay
{
    //NOTE: There maybe can be a superclass, and this is actually would be KeyboardControlComponent
    //this would help the replay compatibility, so we could just change the control component for GamplayScene when runing a replay
    //TODO: Consider making OnActionJustPressed and OnKeyJustReleased
    //TODO: Think of a way to avoid passing GameTime to Key actions, because that is often unused and violate Single Responsibility principle
    //TODO: Think of a way to bing multiple Keys to one Action
    //TODO: Rename into ControlSystem
    internal class ControlComponent
    {
        Dictionary<Keys, Action<GameTime>> KeyDownActions = new();
        Dictionary<Keys, Action<GameTime>> KeyUpActions = new();

        public void BindKeyDownAction(Keys key, Action<GameTime> action)
        {
            KeyDownActions[key] = action;
        }

        public void BindKeyUpAction(Keys key, Action<GameTime> action)
        {
            KeyUpActions[key] = action;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var key in KeyDownActions.Keys)
            {
                if (Keyboard.GetState().IsKeyDown(key))
                {
                    KeyDownActions[key](gameTime);
                }
            }
            foreach (var key in KeyUpActions.Keys)
            {
                if(Keyboard.GetState().IsKeyUp(key))
                {
                    KeyUpActions[key](gameTime);
                }
            }
        }
    }
}
