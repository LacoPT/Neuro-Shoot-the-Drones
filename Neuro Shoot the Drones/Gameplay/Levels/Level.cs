using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Levels
{
    //NOTE: There will be 20 Levels, perhaps should i use LevelID, similar to EnemyID?
    //NOTE: This class could also have backgrounds, music change, Timer changing behaviour based on boss etc
    internal class Level
    {
        //NOTE: Do not use with '?', if the event was not binded to anything, that means the class is used wrong
        public delegate void EnemySpawnedEventHandler(Enemy enemy);
        public event EnemySpawnedEventHandler OnEnemySpawned;

        //NOTE: other possible events. This could be used e.g. for 
/*        public delegate void BossFightStartHandler();
        public event BossFightStartHandler OnBossFightStart;
        public delegate void BossFightEndHandler();
        public BossFightEndHandler OnBossFightEnd;*/

        TimeLineComponent TimeLine = new();

        public void Update(GameTime gameTime)
        {
            TimeLine.Update(gameTime);
        }

        //TODO: Delete this method, Level is a bse class (even if it won't have childs)
        public void FillInTimeLine()
        {
            TimeLine.AddElement(0, () =>
            {
                var enemy = EnemyID.Create(0, new Vector2(400, -300));
                OnEnemySpawned(enemy);
            });
            TimeLine.AddElement(0.5, () =>
            {
                var enemy = EnemyID.Create(0, new Vector2(550, -200));
                OnEnemySpawned(enemy);
            });
            TimeLine.AddElement(1, () =>
            {
                var enemy = EnemyID.Create(0, new Vector2(700, -150));
                OnEnemySpawned(enemy);
            });
            TimeLine.AddElement(1.5, () =>
            {
                var enemy = EnemyID.Create(0, new Vector2(850, -250));
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(8, () =>
            {
                var enemy = EnemyID.Create(1, new Vector2(300, 500));
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(9, () =>
            {
                var enemy = EnemyID.Create(2, new Vector2(GlobalVariables.VisibleGameplayArea.Right + 150, 400));
                OnEnemySpawned(enemy);
            });

            TimeLine.Start();
        }
    }
}
