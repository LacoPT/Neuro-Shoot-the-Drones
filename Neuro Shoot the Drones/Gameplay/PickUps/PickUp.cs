using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Gameplay.Drawable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.PickUps
{
    internal class PickUp : GameEntity
    {
        public readonly PickUpType Type;
        static Dictionary<PickUpType, Rectangle> AtlasData = new()
        {
            {PickUpType.PowerSmall, new Rectangle(0, 0, 32, 32) },
            {PickUpType.ScoreSmall, new Rectangle(0, 32, 32, 32) },
            {PickUpType.PowerBig, new Rectangle(33, 0, 64, 64) },
            {PickUpType.ScoreBig, new Rectangle(98, 0, 64, 64) },
            {PickUpType.Health, new Rectangle(163, 0, 64, 64) },
            {PickUpType.Bomb, new Rectangle(228, 0, 64, 64) },
            {PickUpType.BombShard, new Rectangle(293, 0, 64, 64) },
            {PickUpType.HealthShard, new Rectangle(358, 0, 64, 64) },
        };
        public PickUp(Vector2 startPosition, PickUpType type) : base(startPosition, 0)
        {
            Type = type;
            var sourceRect = AtlasData[type];
            var drawable = new DrawableComponent(this, Resources.PickUpAtlas, sourceRect, sourceRect.GetRelativeCenter(), Vector2.One, syncRotation: false);
            drawable.LayerDepth = 0.4f;
            AddComponent(drawable);
            var collision = new CollisionComponent(sourceRect.Width / 2, CollisionLayers.PlayerCollectZone, CollisionLayers.Pickup, new(), this);
            AddComponent(collision);
            var move = new MoveComponent(this);
            AddComponent(move);
        }

    }
}
