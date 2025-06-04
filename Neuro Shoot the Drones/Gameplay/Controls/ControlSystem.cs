using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Controls
{
    internal class ControlSystem
    {
        readonly Dictionary<Keys, KeyBinding> keyBindings = new();
        static KeyboardState previousState;
        KeyboardState currentState;


        private void AddBinding(Keys key, Action onPressed, Action<GameTime> onHeld, Action onReleased)
        {
            if (!keyBindings.TryGetValue(key, out var binding))
            {
                binding = new KeyBinding();
                keyBindings[key] = binding;
            }

            if (onPressed != null) binding.OnJustPressed = onPressed;
            if (onHeld != null) binding.OnHeld = onHeld;
            if (onReleased != null) binding.OnReleased = onReleased;
        }

        public void BindKeyJustPress(Keys key, Action onPressed)
        {
            AddBinding(key, onPressed, null, null);
        }

        public void BindKeyHeld(Keys key, Action<GameTime> onHeld)
        {
            AddBinding(key, null, onHeld, null);
        }

        public void BindKeyRelease(Keys key, Action onReleased)
        {
            AddBinding(key, null, null, onReleased);
        }

        public void UnbindAll()
        {
            keyBindings.Clear();
        }

        public void Update(GameTime gameTime)
        {
            currentState = Keyboard.GetState();

            foreach (var pair in keyBindings)
            {
                Keys key = pair.Key;
                KeyBinding binding = pair.Value;

                bool isKeyDown = currentState.IsKeyDown(key);
                bool isKeyUp = currentState.IsKeyUp(key);
                bool isKeyJustPressed = previousState.IsKeyUp(key) && isKeyDown;
                bool isKeyReleased = previousState.IsKeyDown(key) && isKeyUp;   

                if (isKeyJustPressed && binding.OnJustPressed != null)
                {
                    binding.OnJustPressed();
                }

                if (isKeyDown && binding.OnHeld != null)
                {
                    binding.OnHeld(gameTime);
                }

                if (isKeyReleased && binding.OnReleased != null)
                {
                    binding.OnReleased();
                }
            }

            previousState = currentState;
        }
    }
}
