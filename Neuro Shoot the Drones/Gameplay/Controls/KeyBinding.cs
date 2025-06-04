using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Controls
{
    internal class KeyBinding
    {
        public Action OnJustPressed { get; set; }
        public Action<GameTime> OnHeld { get; set; }
        public Action OnReleased { get; set; }
    }
}
