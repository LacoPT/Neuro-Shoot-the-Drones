using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.Gameplay.Bosses;
using Neuro_Shoot_the_Drones.Gameplay.CommonComponents;
using Neuro_Shoot_the_Drones.Gameplay.Enemies;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories;
using Neuro_Shoot_the_Drones.Gameplay.PickUps;
using Neuro_Shoot_the_Drones.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.Levels
{
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

        //NOTE: This method exist for the only demo level that exists right now
        public void FillInDemo()
        {
            var center = ResolutionData.VisibleGameplayArea.Center.ToVector2();
            var areaRect = ResolutionData.VisibleGameplayArea;

            double phaseStart = 0;

            #region phase1
            TimeLine.AddElement(phaseStart + 1.5, () =>
            {
                var enemy = EnemyID.Create("DemoDrone1", new Vector2(center.X - 75, areaRect.Top - 100));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 50;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 1.5, () =>
            {
                var enemy = EnemyID.Create("DemoDrone1", new Vector2(center.X + 75, areaRect.Top - 100));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 50;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 6, () =>
            {
                var enemy = EnemyID.Create("DemoDrone1", new Vector2(center.X - 175, areaRect.Top - 150));
                var health = enemy.GetComponent<HealthComponent>();
                health.Hurt(3);
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.ScoreSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 6, () =>
            {
                var enemy = EnemyID.Create("DemoDrone1", new Vector2(center.X + 175, areaRect.Top - 150));
                var health = enemy.GetComponent<HealthComponent>();
                health.Hurt(3);
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.ScoreSmall) };
                OnEnemySpawned(enemy);
            });
            #endregion


            #region phase2
            phaseStart = 12;
            TimeLine.AddElement(phaseStart, () =>
            {
                var enemy = EnemyID.Create("DemoDrone2_L", center);
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.ScoreSmall), new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + .75, () =>
            {
                var enemy = EnemyID.Create("DemoDrone2_L", center);
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.ScoreSmall), new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 1.5, () =>
            {
                var enemy = EnemyID.Create("DemoDrone2_L", center);
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.ScoreBig), new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });


            TimeLine.AddElement(phaseStart + 4, () =>
            {
                var enemy = EnemyID.Create("DemoDrone2_R", center);
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.ScoreSmall), new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 4.75, () =>
            {
                var enemy = EnemyID.Create("DemoDrone2_R", center);
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.ScoreSmall), new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 5.5, () =>
            {
                var enemy = EnemyID.Create("DemoDrone2_R", center);
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.ScoreBig), new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });
            #endregion


            #region phase3

            phaseStart = 23;

            TimeLine.AddElement(phaseStart, () =>
            {
                var enemy = EnemyID.Create("DemoGymbag3_L", center - new Vector2(areaRect.Width / 2 + 50, 175));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 250;
                deathData.Drop = new List<PickUp> { new PickUp(Vector2.Zero, PickUpType.PowerBig) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 4, () =>
            {
                var enemy = EnemyID.Create("DemoGymbag3_R", center + new Vector2(areaRect.Width / 2 + 50, -175));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 250;
                deathData.Drop = new List<PickUp> { new PickUp(Vector2.Zero, PickUpType.PowerBig) };
                OnEnemySpawned(enemy);
            });

            #endregion


            #region phase4
            phaseStart = 30;

            var timeDifference = 0.8;

            TimeLine.AddElement(phaseStart + timeDifference * 1, () =>
            {
                var enemy = EnemyID.Create("DemoDrone4_L", center - new Vector2(areaRect.Width / 2 + 50, 250));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + timeDifference * 2, () =>
            {
                var enemy = EnemyID.Create("DemoDrone4_R", center + new Vector2(areaRect.Width / 2 + 50, 250));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + timeDifference * 3, () =>
            {
                var enemy = EnemyID.Create("DemoDrone4_L", center - new Vector2(areaRect.Width / 2 + 50, 250));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall), new PickUp(Vector2.Zero, PickUpType.ScoreSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + timeDifference * 4, () =>
            {
                var enemy = EnemyID.Create("DemoDrone4_R", center + new Vector2(areaRect.Width / 2 + 50, 250));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall), new PickUp(Vector2.Zero, PickUpType.ScoreSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + timeDifference * 5, () =>
            {
                var enemy = EnemyID.Create("DemoDrone4_L", center - new Vector2(areaRect.Width / 2 + 50, 250));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + timeDifference * 6, () =>
            {
                var enemy = EnemyID.Create("DemoDrone4_R", center + new Vector2(areaRect.Width / 2 + 50, 250));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() {new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + timeDifference * 7, () =>
            {
                var enemy = EnemyID.Create("DemoDrone4_L", center - new Vector2(areaRect.Width / 2 + 50, 250));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall), new PickUp(Vector2.Zero, PickUpType.ScoreSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + timeDifference * 8, () =>
            {
                var enemy = EnemyID.Create("DemoDrone4_R", center + new Vector2(areaRect.Width / 2 + 50, 250));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall), new PickUp(Vector2.Zero, PickUpType.ScoreSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + timeDifference * 9, () =>
            {
                var enemy = EnemyID.Create("DemoDrone4_L", center - new Vector2(areaRect.Width / 2 + 50, 250));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });

            #endregion


            #region phase5
            phaseStart = 45;

            TimeLine.AddElement(phaseStart, () =>
            {
                var enemy = EnemyID.Create("DemoMinawan5_L", center - new Vector2(areaRect.Width / 2 + 100, 0));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 1.2, () =>
            {
                var enemy = EnemyID.Create("DemoMinawan5_L", center - new Vector2(areaRect.Width / 2 + 100, 100));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall), new PickUp(Vector2.Zero, PickUpType.ScoreBig) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 2.4, () =>
            {
                var enemy = EnemyID.Create("DemoMinawan5_R", center + new Vector2(areaRect.Width / 2 + 100, -100));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerBig) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 3.6, () =>
            {
                var enemy = EnemyID.Create("DemoMinawan5_R", center + new Vector2(areaRect.Width / 2 + 100, -250));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall), new PickUp(Vector2.Zero, PickUpType.ScoreBig) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 4.8, () =>
            {
                var enemy = EnemyID.Create("DemoMinawan5_L", center - new Vector2(areaRect.Width / 2 + 100, 175));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerBig), new PickUp(Vector2.Zero, PickUpType.ScoreBig) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 6, () =>
            {
                var enemy = EnemyID.Create("DemoMinawan5_R", center + new Vector2(areaRect.Width / 2 + 100, 150));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });
            #endregion

            TimeLine.AddElement(60, () =>
            {
                OnLevelEnded?.Invoke();
            });

            TimeLine.Start();
        }


        public void FillInTest()
        {
            var center = ResolutionData.VisibleGameplayArea.Center.ToVector2();
            var areaRect = ResolutionData.VisibleGameplayArea;
            var phaseStart = 0;

            TimeLine.AddElement(phaseStart, () =>
            {
                var enemy = EnemyID.Create("DemoMinawan5_L", center - new Vector2(areaRect.Width / 2 + 100, 0));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 1.2, () =>
            {
                var enemy = EnemyID.Create("DemoMinawan5_L", center - new Vector2(areaRect.Width / 2 + 100, 100));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall), new PickUp(Vector2.Zero, PickUpType.ScoreBig) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 2.4, () =>
            {
                var enemy = EnemyID.Create("DemoMinawan5_R", center + new Vector2(areaRect.Width / 2 + 100, -100));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerBig) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 3.6, () =>
            {
                var enemy = EnemyID.Create("DemoMinawan5_R", center + new Vector2(areaRect.Width / 2 + 100, -250));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall), new PickUp(Vector2.Zero, PickUpType.ScoreBig) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 4.8, () =>
            {
                var enemy = EnemyID.Create("DemoMinawan5_L", center - new Vector2(areaRect.Width / 2 + 100, 175));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerBig), new PickUp(Vector2.Zero, PickUpType.ScoreBig) };
                OnEnemySpawned(enemy);
            });

            TimeLine.AddElement(phaseStart + 6, () =>
            {
                var enemy = EnemyID.Create("DemoMinawan5_R", center + new Vector2(areaRect.Width / 2 + 100, 150));
                var deathData = enemy.GetComponent<EnemyDeathDataComponent>();
                deathData.Score = 100;
                deathData.Drop = new List<PickUp>() { new PickUp(Vector2.Zero, PickUpType.PowerSmall) };
                OnEnemySpawned(enemy);
            });

            TimeLine.Start();
        }
    }
}
