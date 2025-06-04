using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Enemies
{
    //TODO: Insert into GamePlayScene
    internal class EnemySystem
    {
        List<Enemy> Enemies = new();
        List<Enemy> EnemiesToRemove = new();

        public void Update(GameTime gameTime)
        {
            foreach(var enemy in EnemiesToRemove)
            {
                Enemies.Remove(enemy);
            }
            EnemiesToRemove.Clear();
        }

        public void CreateEnemy(Enemy enemy)
        {
            Enemies.Add(enemy);
            enemy.OnDeath += (data) => enemy.Destroy();
            enemy.OnDestroy += () => EnemiesToRemove.Add(enemy);
            enemy.Initialize();
        }
    }
}
