using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Timeline
{
    internal class TimeLineSystem : BaseSystem
    {
        public TimeLineSystem() : base(typeof(TimeLineComponent))
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (TimeLineComponent timeline in Components)
            {
                if (!(timeline.State == TimeLineState.Running))
                    continue;
                if (timeline.TimeLine.TryPeek(out var element))
                {
                    if (timeline.Time >= element.Time)
                    {
                        timeline.TimeLine.Dequeue();
                        element.Invoke();
                    }
                }
                else
                {
                    timeline.Finish();
                }
                timeline.Time += dt;
            }
        }
    }
}
