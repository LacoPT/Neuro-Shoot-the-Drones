using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Collisions
{
    internal class CollisionSystem
    {
        private readonly Rectangle worldBounds;
        private readonly List<CollisionComponent> colliders = new List<CollisionComponent>();
        private readonly List<CollisionComponent> collidersToRemove = new List<CollisionComponent>();

        private HashSet<(CollisionComponent, CollisionComponent)> processedPairs = new HashSet<(CollisionComponent, CollisionComponent)>();

        public CollisionSystem(Rectangle worldBounds)
        {
            this.worldBounds = worldBounds;
        }

        public void Update()
        {
            foreach (var collider in collidersToRemove)
                colliders.Remove(collider);
            Quadtree quadtree = new Quadtree(0, worldBounds);
            foreach (var collider in colliders)
            {
                quadtree.Insert(collider);
            }

            processedPairs.Clear();

            foreach (var col in colliders)
            {
                List<CollisionComponent> potentialColliders = new List<CollisionComponent>();
                quadtree.Retrieve(potentialColliders, col);

                foreach (var other in potentialColliders)
                {
                    if (col == other)
                        continue;

                    if (IsPairProcessed(col, other))
                        continue;

                    MarkPairProcessed(col, other);

                    if (CheckCollision(col, other))
                    {
                        if ((col.ImpactLayers & other.AffectedByLayers) != 0)
                        {
                            other.RegisterCollision(col.CollisionData);
                        }
                        if ((other.ImpactLayers & col.AffectedByLayers) != 0)
                        {
                            col.RegisterCollision(other.CollisionData);
                        }
                    }
                }
            }
        }

        private bool CheckCollision(CollisionComponent col1, CollisionComponent col2)
        {
            Vector2 delta = col1.Position - col2.Position;
            float distanceSquared = delta.LengthSquared();
            float combinedRadius = col1.HitCircleSize + col2.HitCircleSize;
            return distanceSquared < combinedRadius * combinedRadius;
        }

        private bool IsPairProcessed(CollisionComponent col1, CollisionComponent col2)
        {
            return processedPairs.Contains((col1, col2)) || processedPairs.Contains((col2, col1));
        }

        private void MarkPairProcessed(CollisionComponent col1, CollisionComponent col2)
        {
            processedPairs.Add((col1, col2));
        }

        public void AddCollider(CollisionComponent collider)
        {
            if (!colliders.Contains(collider))
                colliders.Add(collider);
        }

        public void RemoveCollider(CollisionComponent collider)
        {
            collidersToRemove.Add(collider);
        }
    }
}
