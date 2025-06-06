using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Tweens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.MVC
{
    internal abstract class BaseView
    {
        public abstract RenderTarget2D Draw(SpriteBatch sb);

        public void Initialize()
        {
            OnInitialize();
        }

        protected virtual void OnInitialize()
        {
        }
    }
}
