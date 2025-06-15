using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.Gameplay;
using Neuro_Shoot_the_Drones.Menus;
using Neuro_Shoot_the_Drones.Menus.Main;
using Neuro_Shoot_the_Drones.Menus.Pause;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal class SceneManager
    {
        IGameScene currentScene;
        GameplayScene GameplayScene;
        PauseScene PauseScene = new PauseScene();
        MainMenuScene MainMenuScene;
        GameEndScene GameEndScene = new();

        public event Action OnExit;

        public void Initialize(SpriteBatch sb)
        {
            MainMenuScene = new MainMenuScene();
            MainMenuScene.Initialize();
            MainMenuScene.OnStart += () => NewGame(sb);
            currentScene = MainMenuScene;
            PauseScene.Initialize();
            GameEndScene.Initialize();
            PauseScene.OnUnpause += () =>
            {
                currentScene = GameplayScene;
            };

            PauseScene.OnBackToMainMenu += () => currentScene = MainMenuScene;
            MainMenuScene.OnExit += () => OnExit?.Invoke();
            PauseScene.OnExit += () => OnExit?.Invoke();
            GameEndScene.OnEnterPressed += () => currentScene = MainMenuScene;
        }

        public void Draw(SpriteBatch sb, GameTime gt)
        {
            currentScene.Draw(gt, sb);
        }

        public void Update(GameTime gt)
        {
            currentScene.Update(gt);
        }

        void NewGame(SpriteBatch sb)
        {
            GameplayScene = new GameplayScene();
            GameplayScene.Initialize();
            GameplayScene.InitializeLastFrame(sb);

            GameplayScene.OnPause += (lastFrame) =>
            {
                currentScene = PauseScene;
                PauseScene.SetLastFrame(lastFrame);
            };

            GameplayScene.OnEnded += (data) =>
            {
                currentScene = GameEndScene;
                GameEndScene.ShowResult(data);
            };
            currentScene = GameplayScene; 
        }
    }
}
