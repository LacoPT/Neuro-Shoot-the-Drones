using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Bullets
{
    internal class EnemyBullet : BaseBullet
    {
        //NOTE: It's not recommended to create manually, use factory method instead
        public float BaseSpeed = 0;
        public EnemyBullet(Texture2D texture, Rectangle textureSourceRect, Vector2 textureScale, Vector2 position, float baseSpeed, float acceleration,
            float rotation = 0, int hitCircleSize = 1, float rotationSpeed = 0, float rotationAcceleration = 0)
        : base(texture, textureSourceRect, textureScale, position, baseSpeed, acceleration, float.Pi + rotation)
        {
            var move = GetComponent<MoveComponent>();
            BaseSpeed = baseSpeed;
            move.AngularSpeed = rotationSpeed;
            move.AngularAcceleration = rotationAcceleration;
            var data = new CollisionData(0);
            var collision = new CollisionComponent(hitCircleSize, CollisionLayers.Player | CollisionLayers.PlayerGraze, CollisionLayers.EnemyBullet, data, this);
            AddComponent(collision);
        }

        protected override void CollisionBehaviour(CollisionData data)
        {
            Destroy();
        }
    }
}
