using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Gameplay.Enemies;
using Neuro_Shoot_the_Drones.Timeline;
using Neuro_Shoot_the_Drones.Tweens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Bosses
{
    internal class BossPhase
    {
        public readonly bool HealthLocked;
        public readonly HealthComponent Health;
        public readonly float Duration;
        public readonly TimeLineComponent TimeLine;
        public readonly EnemyDeathDataComponent ScoreDrop;

        public BossPhase(bool healthLocked, HealthComponent health, float duration, TimeLineComponent timeLine, EnemyDeathDataComponent scoreDrop)
        {
            HealthLocked = healthLocked;
            Health = health;
            Duration = duration;
            TimeLine = timeLine;
            ScoreDrop = scoreDrop;
        }
    }
}
