using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.Bosses;
using Neuro_Shoot_the_Drones.Gameplay.Enemies;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories;
using Neuro_Shoot_the_Drones.Timeline;
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

        public delegate void BossSpawnedEventHanlder(Boss boss);
        public event BossSpawnedEventHanlder OnBossSpawned;

        //NOTE: other possible events. This could be used e.g. for 
/*        public delegate void BossFightStartHandler();
        public event BossFightStartHandler OnBossFightStart;
        public delegate void BossFightEndHandler();
        public BossFightEndHandler OnBossFightEnd;*/

        public readonly TimeLineComponent TimeLine = new();
        public delegate void LevelEndedEventHandler();
        public event LevelEndedEventHandler OnLevelEnded;


        public void Update(GameTime gameTime)
        {
        }

        //TODO: Delete this method, Level is a bse class (even if it won't have childs)
        public void FillInTimeLine()
        {
            TimeLine.AddElement(0, () =>
            {
                var enemy = EnemyID.Create("Drone0", new Vector2(400, -300));
                enemy.GetComponent<EnemyDeathDataComponent>().Drop = new() { new PickUps.PickUp(Vector2.Zero, PickUps.PickUpType.PowerSmall),
                                                                             new PickUps.PickUp(Vector2.Zero, PickUps.PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });
            TimeLine.AddElement(0.5, () =>
            {
                var enemy = EnemyID.Create("Drone0", new Vector2(550, -200));
                enemy.GetComponent<EnemyDeathDataComponent>().Drop = new() { new PickUps.PickUp(Vector2.Zero, PickUps.PickUpType.PowerBig) };
                OnEnemySpawned(enemy);
            });
            TimeLine.AddElement(1, () =>
            {
                var enemy = EnemyID.Create("Drone0", new Vector2(700, -150));
                enemy.GetComponent<EnemyDeathDataComponent>().Drop = new() { new PickUps.PickUp(Vector2.Zero, PickUps.PickUpType.ScoreSmall) };
                OnEnemySpawned(enemy);
            });
            TimeLine.AddElement(1.5, () =>
            {
                var enemy = EnemyID.Create("Drone0", new Vector2(850, -250));
                enemy.GetComponent<EnemyDeathDataComponent>().Drop = new() { new PickUps.PickUp(Vector2.Zero, PickUps.PickUpType.ScoreBig) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(8, () =>
            {
                var enemy = EnemyID.Create("Minawan0", new Vector2(300, 500));
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(9, () =>
            {
                var enemy = EnemyID.Create("Minawan1", new Vector2(ResolutionData.VisibleGameplayArea.Right + 150, 400));
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(15, () =>
            {
                OnBossSpawned(DemoBoss.Create());
            });

            TimeLine.AddElement(60, () =>
            {
                OnLevelEnded?.Invoke();
            });

            TimeLine.Start();
        }
    }
}
