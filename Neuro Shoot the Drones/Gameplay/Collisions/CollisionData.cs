using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Collisions
{
    //Consider using builder when there will be more fields
    internal class CollisionData
    {
        public readonly int Damage;

        public CollisionData(int damage = 0)
        {
            Damage = damage;
        }
    }
}
