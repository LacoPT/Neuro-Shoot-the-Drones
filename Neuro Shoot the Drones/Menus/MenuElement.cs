using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Menus
{
    abstract class MenuElement
    {
        public Vector2 Position { get; protected set; }
        public bool Selected;
        public bool Activated;
        public delegate void ActivatedEventHandler();
        public event ActivatedEventHandler OnActivated;

        public abstract void Draw(SpriteBatch sb);

        public void Activate()
        {
            OnActivated?.Invoke();
        }
    }
}
