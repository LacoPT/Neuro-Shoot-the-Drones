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
    internal class ControlSystem
    {
        Dictionary<Keys, Action<GameTime>> KeyDownActions = new();
        Dictionary<Keys, Action<GameTime>> KeyUpActions = new();

        Dictionary<Keys, Action> KeyJustDownActions = new();
        //NOTE: it is static to prevent changing scenes from incorrectly read previous state
        //e.g. pause menu immediately switches back to unpause
        static KeyboardState previousState;


        public void BindKeyDownAction(Keys key, Action<GameTime> action)
        {
            KeyDownActions[key] = action;
        }

        public void BindKeyUpAction(Keys key, Action<GameTime> action)
        {
            KeyUpActions[key] = action;
        }

        public void UnbindKeyUp(Keys key)
        {
            KeyUpActions.Remove(key);
        }

        public void UnbindKeyDown(Keys key)
        {
            KeyDownActions.Remove(key);
        }

        public void BindKeyJustDownAction(Keys key, Action action)
        {
            KeyJustDownActions[key] = action;
        }
        public void UnbindKeyJustDown(Keys key)
        {
            KeyJustDownActions.Remove(key);
        }
        public void Update(GameTime gameTime)
        {
            var currentState = Keyboard.GetState();
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
            foreach(var key in KeyJustDownActions.Keys)
            {
                if(previousState.IsKeyUp(key) && currentState.IsKeyDown(key))
                {
                    KeyJustDownActions[key]();
                }
            }
            previousState = currentState;
        }
    }
}
