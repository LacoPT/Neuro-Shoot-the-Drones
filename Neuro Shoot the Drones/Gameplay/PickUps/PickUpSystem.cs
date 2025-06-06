using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.PickUps
{
    internal class PickUpSystem
    {
        List<PickUp> Pickups = new();
        List<PickUp> PickUpsToRemove = new();

        public void Update(GameTime gameTime)
        {
            foreach(var enemy in PickUpsToRemove)
            {
                Pickups.Remove(enemy);
            }
            PickUpsToRemove.Clear();
        }

        public void AddPickup(PickUp pickup)
        {
            Pickups.Add(pickup);
            pickup.OnDestroy += () => PickUpsToRemove.Add(pickup);
            pickup.Initialize();
        }

        public void AddPickups(List<PickUp> pickups)
        {
            foreach (var pickup in pickups)
                AddPickup(pickup);
        }
    }
}
