using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    public static class Vector2Extensions
    {
        public static Vector2 RectClamp(this Vector2 v, Rectangle rect)
        {
            var x = Math.Clamp(v.X, rect.Left, rect.Right);
            var y = Math.Clamp(v.Y, rect.Top, rect.Bottom);
            return new Vector2(x, y);
        }
    }
}
