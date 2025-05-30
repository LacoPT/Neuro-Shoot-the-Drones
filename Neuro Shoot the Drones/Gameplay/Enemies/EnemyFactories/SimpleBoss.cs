using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories
{
    //NOTE: When properly migrated to ECS, bosses will be easier to make
    internal class SimpleBoss : EnemyFactory
    {
        public override Enemy Create(Vector2 initialPosition)
        {
            throw new NotImplementedException();
        }
    }
}
