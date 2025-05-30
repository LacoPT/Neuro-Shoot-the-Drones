using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories
{
    internal static class EnemyID
    {
        readonly static Dictionary<String, EnemyFactory> factoriesByStrings = new();

        public static void Initialize()
        {
            InitializeStrings();
        }

        public static void InitializeStrings()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(EnemyFactory));
            var derivedTypes = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(EnemyFactory).IsAssignableFrom(t));
            foreach(var type in derivedTypes)
            {
                var attr = type.GetCustomAttribute<NameAttribute>();
                string name = attr?.Name ?? type.Name;

                var instance = (EnemyFactory)Activator.CreateInstance(type);

                factoriesByStrings[name] = instance;
            }
        }

        //NOTE: This can throw expection, this is intended because if name doesn't exist, that's level's description fault
        public static Enemy Create(string id, Vector2 initialPosition)
        {
            return factoriesByStrings[id].Create(initialPosition);
        }
    }
}
