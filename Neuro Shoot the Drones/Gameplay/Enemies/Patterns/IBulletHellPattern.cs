using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    //TODO: Rewrite into abstract class
    interface IBulletHellPattern
    {
        public void Generate();
        public void UpdatePosition(Vector2 newPosition);
    }
}

