using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    //TODO: Rewrite into abstract class
    //TODO: Consider making PatternID
    interface IBulletHellPattern
    {
        public List<EnemyBullet> Generate();
        public void UpdatePosition(Vector2 newPosition);
    }
}

