using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal static class RectangleExtensions
    {

        public static Rectangle Grow(this Rectangle rect,  int x, int y)
        {
            return rect.Grow(new Point(x, y));
        }
        public static Rectangle Grow(this Rectangle rect, int amount)
        {
            return rect.Grow(amount, amount);
        }
        public static Rectangle Grow(this Rectangle rect, Point amount)
        {
            return new Rectangle(rect.Location - amount, rect.Size + amount.Scale(2f));
        }

        public static Vector2 GetRelativeCenter(this Rectangle rect)
        {
            return (rect.Center - rect.Location).ToVector2();
        }
    }
}
