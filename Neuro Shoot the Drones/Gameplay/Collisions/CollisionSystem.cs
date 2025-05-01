using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Collisions
{
    internal class CollisionSystem
    {
        List<CollisionComponent> Colliders = new();

        public void Update()
        {
            for(int i = 0; i < Colliders.Count; i++)
            {
                for(int j = i+1; j< Colliders.Count; j++)
                {
                    var col1 = Colliders[i];
                    var col2 = Colliders[j];
                    if( (col1.ImpactLayers & col2.AffectedByLayers) != 0 && CheckCollisions(col1, col2) )
                        col2.RegisterCollision(col1.CollisionData);
                    if((col2.ImpactLayers & col1.AffectedByLayers) != 0 && CheckCollisions(col1, col2) )
                        col1.RegisterCollision(col2.CollisionData);
                }
            }
        }

        public void AddCollider(CollisionComponent col)
        {
            Colliders.Add(col);
        }

        public void RemoveCollider(CollisionComponent col)
        {
            Colliders.Remove(col);
        }

        //NOTE: Maybe later i'll introduce different CollisionShape, but now it is how it is
        bool CheckCollisions(CollisionComponent col1, CollisionComponent col2)
        {
            return (col1.Position - col2.Position).Length() < (col1.HitCircleSize + col2.HitCircleSize);
        }
    }
}
