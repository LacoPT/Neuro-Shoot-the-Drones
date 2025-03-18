using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal static class PointExtensions
    {
        public static void AddVector(this Point p, Vector2 v)
        {
            p.X += (int)Math.Round(v.X);
            p.Y += (int)Math.Round(v.Y);
        }
        public static Point Scale(this Point p, float scale)
        {
            return new Point((int)(p.X * scale), (int)(p.X * scale));
        }
    }
}
