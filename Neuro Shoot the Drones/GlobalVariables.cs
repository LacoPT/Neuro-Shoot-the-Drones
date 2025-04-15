using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal static class GlobalVariables
    {
        public static Point Resolution = new(1920, 1080);
        public static Rectangle VisibleGameplayArea = new Rectangle(350, 50, 650, 980);
    }
}
