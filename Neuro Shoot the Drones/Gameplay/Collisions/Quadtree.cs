using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Collisions
{
    internal class Quadtree
    {
        private const int MAX_OBJECTS = 10;
        private const int MAX_LEVELS = 5;

        private int level;
        private List<CollisionComponent> objects;
        private Rectangle bounds;
        private Quadtree[] nodes;

        public Quadtree(int level, Rectangle bounds)
        {
            this.level = level;
            this.bounds = bounds;
            objects = new List<CollisionComponent>();
            nodes = new Quadtree[4];
        }

        public void Clear()
        {
            objects.Clear();
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i]?.Clear();
                nodes[i] = null;
            }
        }

        private void Split()
        {
            int subWidth = bounds.Width / 2;
            int subHeight = bounds.Height / 2;
            int x = bounds.X;
            int y = bounds.Y;

            nodes[0] = new Quadtree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight)); 
            nodes[1] = new Quadtree(level + 1, new Rectangle(x, y, subWidth, subHeight)); 
            nodes[2] = new Quadtree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight)); 
            nodes[3] = new Quadtree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight)); 
        }

        private int GetIndex(CollisionComponent collider)
        {
            int index = -1;
            float verticalMidpoint = bounds.X + (bounds.Width / 2f);
            float horizontalMidpoint = bounds.Y + (bounds.Height / 2f);

            float left = collider.Position.X - collider.HitCircleSize;
            float right = collider.Position.X + collider.HitCircleSize;
            float top = collider.Position.Y - collider.HitCircleSize;
            float bottom = collider.Position.Y + collider.HitCircleSize;

            bool topQuadrant = (top >= bounds.Y && bottom <= horizontalMidpoint);
            bool bottomQuadrant = (top >= horizontalMidpoint && bottom <= bounds.Y + bounds.Height);

            if (left >= bounds.X && right <= verticalMidpoint)
            {
                if (topQuadrant)
                    index = 1;
                else if (bottomQuadrant)
                    index = 2;
            }
            else if (left >= verticalMidpoint && right <= bounds.X + bounds.Width)
            {
                if (topQuadrant)
                    index = 0; 
                else if (bottomQuadrant)
                    index = 3;             }

            return index;
        }

        public void Insert(CollisionComponent collider)
        {
            if (nodes[0] != null)
            {
                int index = GetIndex(collider);
                if (index != -1)
                {
                    nodes[index].Insert(collider);
                    return;
                }
            }

            objects.Add(collider);

            if (objects.Count > MAX_OBJECTS && level < MAX_LEVELS)
            {
                if (nodes[0] == null)
                    Split();

                int i = 0;
                while (i < objects.Count)
                {
                    int index = GetIndex(objects[i]);
                    if (index != -1)
                    {
                        CollisionComponent moveObj = objects[i];
                        objects.RemoveAt(i);
                        nodes[index].Insert(moveObj);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
        public List<CollisionComponent> Retrieve(List<CollisionComponent> returnObjects, CollisionComponent collider)
        {
            int index = GetIndex(collider);
            if (index != -1 && nodes[0] != null)
            {
                nodes[index].Retrieve(returnObjects, collider);
            }

            returnObjects.AddRange(objects);
            return returnObjects;
        }
    }
}
