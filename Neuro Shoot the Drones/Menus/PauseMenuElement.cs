using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Menus
{
    internal class PauseMenuElement: MenuElement
    {
        public readonly string Label;

        public PauseMenuElement(Vector2 position, string label)
        {
            Position = position;
            Label = label;
        }

        public override void Draw(SpriteBatch sb)
        {
            var color = Selected ? Color.White : Color.Pink;
            sb.DrawString(Resources.DefaultFont, Label, Position, color);
        }
    }
}
