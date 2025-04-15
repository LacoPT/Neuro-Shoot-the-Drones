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

        public static Vector2 Rotated(this Vector2 vector, float rotation)
        {
            float sin = MathF.Sin(rotation);
            float cos = MathF.Cos(rotation);

            float newX = (vector.X * cos) - (vector.Y * sin);
            float newY = (vector.X * sin) + (vector.Y * cos);

            return new Vector2(newX, newY);
        }

        public static float LookAt(this Vector2 position,  Vector2 target)
        {
            Vector2 direction = target - position;
            float angle = MathF.Atan2(direction.Y, direction.X);
            return angle;
        }

        public static Vector2 WithY(this Vector2 v, float y)
        {
            return new Vector2(v.X, y);
        }
        public static Vector2 WithX(this Vector2 v, float x)
        {
            return new Vector2(x, v.Y);
        }
    }
}
