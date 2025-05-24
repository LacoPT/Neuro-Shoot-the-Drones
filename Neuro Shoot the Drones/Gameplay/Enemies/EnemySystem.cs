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

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            foreach (Enemy enemy in Enemies)
                enemy.Draw(gameTime, sb);
        }

        public void Update(GameTime gameTime)
        {
            foreach(var enemy in EnemiesToRemove)
            {
                Enemies.Remove(enemy);
            }
            EnemiesToRemove.Clear();

            foreach(var enemy in Enemies)
            {
                enemy.Update(gameTime);
            }
        }

        public void CreateEnemy(Enemy enemy)
        {
            Enemies.Add(enemy);
            enemy.OnDestroy += () => EnemiesToRemove.Add(enemy);
            enemy.Initialize();
        }
    }
}
