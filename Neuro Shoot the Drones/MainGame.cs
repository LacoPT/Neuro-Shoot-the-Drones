using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories;
using Neuro_Shoot_the_Drones.Menus;

namespace Neuro_Shoot_the_Drones
{
    //TODO: Finish architecture migration to ECS
    //TODO: Add Player stats - Graze, Power
    //TODO: Make Bombs
    //TODO: Add levels System
    //TODO: Add bosses to architecture
    //TODO: Consider making ID superclass for EnemyID, PatternID or LevelID
    //NOTE: It was to late to rewrite all of the code when i found out the existence of ComponentModel namespace
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameplayScene MainGameplayScene = new GameplayScene();
        //TODO: Make a scene Maanager
        private PauseScene PauseScene = new PauseScene();
        private IGameScene currentScene = new GameplayScene();

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = GlobalVariables.Resolution.X;
            _graphics.PreferredBackBufferHeight = GlobalVariables.Resolution.Y;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //!!! DO NOT DO ANYTHING BEFORE base.Initialize() !!!
            base.Initialize();
            EnemyID.Initialize();
            currentScene = MainGameplayScene;
            currentScene.Initialize();
            PauseScene.Initialize();
            MainGameplayScene.OnPause += (lastFrame) =>
            {
                currentScene = PauseScene;
                PauseScene.SetLastFrame(lastFrame);
            };

            PauseScene.OnUnpause += () => currentScene = MainGameplayScene;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Resources.PlayerTextureAtlas = Content.Load<Texture2D>("PlayerAtlas");
            Resources.GameFrameUI = Content.Load<Texture2D>("GameFrame");
            Resources.BulletTextureAtlas = Content.Load<Texture2D>("Bullets");
            Resources.Drone = Content.Load<Texture2D>("drone");
            Resources.LightDrone = Content.Load<Texture2D>("lightdrone");
            Resources.Minawan = Content.Load<Texture2D>("minawan");
            Resources.DefaultFont = Content.Load<SpriteFont>("ActualFontMap");
            Resources.DefaultFont.Spacing = -6;
            Resources.BlackPixel = Content.Load<Texture2D>("blackPixel");
            Resources.HealthBarAtlas = Content.Load<Texture2D>("HealthBarAtlas");
            Resources.GrayScale = Content.Load<Effect>("GrayScale");
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            currentScene.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            currentScene.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }
    }
}
