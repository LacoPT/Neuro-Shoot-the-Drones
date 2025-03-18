using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal interface IGameObject
    {
        public void Initialize();
        public void Update(GameTime gameTime);
        public void Draw(GameTime gameTime, SpriteBatch sb);
    }
}
