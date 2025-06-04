using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using Neuro_Shoot_the_Drones.Gameplay.Drawable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Bullets
{
    internal class PlayerBullet : BaseBullet
    {
        int HitLimit;
        //NOTE: It's not recommended to create manually, use factory method instead
        public PlayerBullet(Texture2D texture, Rectangle textureSourceRect, Vector2 textureScale, Vector2 position,
            float baseSpeed, float acceleration, float rotation = 0, int hitCircleSize = 1, int hitLimit = 1, int damage = 0)
        : base(texture, textureSourceRect, textureScale, position, baseSpeed, acceleration, 0 + rotation)
        {
            HitLimit = hitLimit;
            var data = new CollisionData(damage);
            var collision = new CollisionComponent(hitCircleSize, CollisionLayers.Enemy, CollisionLayers.PlayerBullet, data, this);
            AddComponent(collision);
            GetComponent<Drawable.DrawableComponent>().LayerDepth = 0.5f;
        }

        protected override void CollisionBehaviour(CollisionData data)
        {
            HitLimit--;
            if(HitLimit <= 0)
            {
                Destroy();
            }    
        }
    }
}
