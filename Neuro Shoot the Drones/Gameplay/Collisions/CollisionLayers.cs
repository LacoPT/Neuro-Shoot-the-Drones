using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Collisions
{
    [Flags]
    public enum CollisionLayers
    {
        None = 0,
        Player = 1,
        Enemy = 2,
        PlayerBullet = 4,
        EnemyBullet = 8,
        PlayerBomb = 16,
        PlayerCollectZone = 32,
        Pickup = 64,
        PlayerGraze = 128
    }
}
