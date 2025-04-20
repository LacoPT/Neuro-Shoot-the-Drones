using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.ExtensionAndHelpers;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.Patterns;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies
{
    //TODO: Consider rewriting using Builder pattern
    internal abstract class EnemyFactory
    {
        public abstract Enemy CreateEnemy(Vector2 initialPosition);
    }
}
