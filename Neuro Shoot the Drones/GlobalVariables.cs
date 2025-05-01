using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    //TODO: Refactor this, make clear that this is a class for storing Screen Resolution parameters
    internal static class GlobalVariables
    {
        public static Point Resolution = new(1920, 1080);
        public static Rectangle VisibleGameplayArea = new Rectangle(350, 50, 650, 980);
    }
}
