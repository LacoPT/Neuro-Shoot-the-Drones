using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.Bullets;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies.Patterns
{
    internal class WhirlCircularPattern : IBulletHellPattern
    {
        public readonly int Count = 5;
        public  Vector2 Position;
        public float InitialRotation;

        public WhirlCircularPattern(int count, Vector2 position, float initialRotation = 0)
        {
            Count = count;
            Position = position;
            InitialRotation = initialRotation;
        }

        public List<EnemyBullet> Generate()
        {
            var result = new List<EnemyBullet>();
            float rotation = InitialRotation;
            float step = MathF.PI * 2 / Count;
            for(int i = 0; i < Count; i++)
            {
                var b = EnemyBulletFactory.CreateStandart(Position);
                var transform = b.GetComponent<TransformComponent>();
                var move = b.GetComponent<MoveComponent>();
                transform.Rotation = rotation;
                move.AngularSpeed = 5f;
                move.AngularAcceleration = -2.5f;
                result.Add(b);
                rotation += step;
            }

            return result;
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            Position = newPosition;
        }
    }
}
