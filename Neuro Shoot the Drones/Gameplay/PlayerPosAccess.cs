using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay
{
    internal static class PlayerPosAccess
    {
        private static TransformComponent playerTransform;

        public static void Set(TransformComponent transform)
        {
            playerTransform = transform;
        }

        public static Vector2 Get() => playerTransform.Position;
    }
}
