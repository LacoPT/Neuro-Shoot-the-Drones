using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Tweens
{
    //NOTE: Tween is not component, so it's not needed to inherite from BaseSystem 
    internal class TweenSystem
    {
        List<Tween> Tweens = new();
        List<Tween> TweensToRemove = new();

        public void Update(GameTime gameTime)
        {
            foreach (var tween in TweensToRemove)
            {
                Tweens.Remove(tween);
            }
            TweensToRemove.Clear();

            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (var tween in Tweens)
            {
                tween.Update(gameTime);
            }
        }

        public void AddTween(Tween tween)
        {
            if(Tweens.Contains(tween)) return;
            Tweens.Add(tween);
            if (tween.DestroyOnEnd)
                tween.OnFinish += tween.Destroy;
            tween.OnDestroy += () => RemoveTween(tween);
        }

        public void RemoveTween(Tween tween)
        {
            if (!Tweens.Contains(tween))
                throw new ArgumentException("Tween already removed or not added!");
            TweensToRemove.Add(tween);
        }
    }
}
