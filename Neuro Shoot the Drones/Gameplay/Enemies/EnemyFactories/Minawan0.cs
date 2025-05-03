using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories
{
    internal class Minawan0 : EnemyFactory
    {
        public override Enemy Create(Vector2 initialPosition)
        {
            var targetPosX = GlobalVariables.VisibleGameplayArea.Right + 100;
            var initialPositionY = initialPosition.Y;
            var amplitude = 100;
            var minawan = new Enemy(20, 20, initialPosition, Resources.Minawan, Resources.Minawan.Bounds, Vector2.One / 5);
            minawan.MoveToX(targetPosX, 0, 3, EasingType.Linear);
            var tween = new Tween(0, float.Pi * 5, 3, EasingType.Linear);
            tween.OnUpdate += () => minawan.Position.Y = initialPosition.Y - amplitude * MathF.Abs(MathF.Sin(tween.Value));
            for(int i = 0; i < 5; i++)
            {
                var pattern = new SimpleCircularPattern(7, initialPosition, MathF.PI * 2f / 5f * (float)i);
                minawan.Shoot(0.05 + (float)i * 3 / 5, pattern);
            }
            minawan.AddTween(tween);
            minawan.OnInitialized += () => tween.Start();
            return minawan;
        }
    }
}
