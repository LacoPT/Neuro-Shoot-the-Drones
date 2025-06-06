using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay.Bullets;
using Neuro_Shoot_the_Drones.Gameplay.Collisions;
using Neuro_Shoot_the_Drones.Gameplay.Drawable;
using Neuro_Shoot_the_Drones.Gameplay.Enemies;
using Neuro_Shoot_the_Drones.Tweens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Bosses
{
    internal class Boss : GameEntity
    {
        public delegate void PhaseStartedEventHandler();
        public event PhaseStartedEventHandler PhaseStarted;

        public delegate void PhaseClearedEventHandler(EnemyDeathDataComponent deathData);
        public event PhaseClearedEventHandler PhaseCleared;

        public delegate void PhaseOverEventHandler();
        public event PhaseOverEventHandler OnPhaseOver;

        public delegate void AddTweenEventHandler(Tween tween);
        public event AddTweenEventHandler OnAddTween;

        public delegate void ShootEventHandler(List<EnemyBullet> bullets);
        public event ShootEventHandler OnShoot;

        public readonly Queue<BossPhase> Phases = new();
        public Boss(Vector2 startPosition, Texture2D texture, Rectangle textureSourceRect, Vector2 textureScale, int hitCircleSize) : base(startPosition)
        {
            var drawable = new DrawableComponent(this, texture, textureSourceRect, textureSourceRect.GetRelativeCenter(), textureScale);
            AddComponent(drawable);
            var collision = new CollisionComponent(hitCircleSize, CollisionLayers.Player | CollisionLayers.PlayerBullet,
                                                   CollisionLayers.Enemy, new(0), this);
            AddComponent(collision);
        }
    }
}
