using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.CommonComponents
{
    internal class HealthSystem : BaseSystem
    {
        public HealthSystem() : base(typeof(HealthComponent))
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
            foreach (HealthComponent health in Components)
            {
                if(health.Skip) continue;
                health.TickHurt = MathF.Min(health.TickHurt, health.Armour);
                health.Health -= health.TickHurt;
                health.TickHurt = 0;
                if (health.Health <= 0) health.Die();
            }
        }
    }
}
