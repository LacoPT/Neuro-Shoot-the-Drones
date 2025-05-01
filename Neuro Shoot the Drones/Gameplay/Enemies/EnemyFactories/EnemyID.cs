using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories
{
    internal static class EnemyID
    {
        //TODO: Consider using Dictionary<String, EnemyFactory> instead
        readonly static List<EnemyFactory> factories = new();

        //Usage: when adding a new type of enemy to the game, add its factory to the list
        public static void Initialize()
        {
            factories.Add(new Drone0());
        }

        public static Enemy Create(int id, Vector2 initialPosition)
        {
            return factories.ElementAt(id).Create(initialPosition);
        }
    }
}
