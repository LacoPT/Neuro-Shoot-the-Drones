using Neuro_Shoot_the_Drones.ECS;
using Neuro_Shoot_the_Drones.Gameplay.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.CommonComponents
{
    internal class HealthComponent : Component
    {
        public float Health;
        /// <summary>
        ///     The maximum amount of damage that can be taken per tick / time piece
        /// </summary>
        public float Armour = float.PositiveInfinity;
        public float TickHurt = 0f;

        public delegate void DeathEventHandler();
        public event DeathEventHandler OnDeath;

        public HealthComponent(BaseEntity entity, float health, float armour = float.PositiveInfinity) : base(entity)
        {
            Health = health;
            Armour = armour;
        }

        public void Hurt(float amount)
        {
            TickHurt += amount;
        }

        public void Die()
        {
            var entity = (Enemy)Entity;
            OnDeath?.Invoke();
        }
    }
}
