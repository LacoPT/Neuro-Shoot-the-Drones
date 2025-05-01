using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.Patterns;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies
{
    //NOTE: No builder needed, EnemyID works great with factory pattern
    internal abstract class EnemyFactory
    {
        public abstract Enemy Create(Vector2 initialPosition);
    }
}
