using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Gameplay.Enemies;
using Neuro_Shoot_the_Drones.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Bosses
{
    internal static class DemoBoss
    {
        public static Boss Create()
        {
            var boss = new Boss(ResolutionData.PlayerInitialPosition - new Vector2(0, 1000), Resources.Cerb, Resources.Cerb.Bounds, Vector2.One / 3, 32);
            boss.Phases.Enqueue(FirstPhase(boss));
            return boss;
        }

        static BossPhase FirstPhase(Boss boss)
        {
            var FirstPoint = ResolutionData.PlayerInitialPosition - new Vector2(0, 600);
            var SecontPoint = FirstPoint + new Vector2(300, 0);
            var ThrirdPoint = FirstPoint - new Vector2(300, 0);
            var timeLine = new TimeLineComponent();

            boss.MoveToY(FirstPoint.Y, 1, 0.5f, EasingType.CircularEaseOut, timeLine);

            var health = new HealthComponent(boss, 40);
            var bossPhase = new BossPhase(false, health, 30f, timeLine, new EnemyDeathDataComponent(boss, 1000));
            return bossPhase;
        }
    }
}
