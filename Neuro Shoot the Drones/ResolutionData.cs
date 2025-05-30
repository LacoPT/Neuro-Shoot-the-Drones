using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal static class ResolutionData
    {
        public readonly static Point Resolution = new(1920, 1080);
        public readonly static Rectangle VisibleGameplayArea = new Rectangle(350, 50, 650, 980);
        public readonly static Vector2 PlayerInitialPosition = new(VisibleGameplayArea.Left + VisibleGameplayArea.Width / 2,
                                                        VisibleGameplayArea.Bottom - VisibleGameplayArea.Height / 5);
 
    }
}
