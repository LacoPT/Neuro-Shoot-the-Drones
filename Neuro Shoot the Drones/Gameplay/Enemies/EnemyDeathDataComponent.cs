using Neuro_Shoot_the_Drones.ECS;
using Neuro_Shoot_the_Drones.Gameplay.PickUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies
{
    //TODO: Add drop from the Enemy
    internal class EnemyDeathDataComponent : Component
    {
        public int Score = 0;
        public List<PickUp> Drop = new();
        public EnemyDeathDataComponent(BaseEntity entity, int score) : base(entity)
        {
            Score = score;
        }
    }
}
