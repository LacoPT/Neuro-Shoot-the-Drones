using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.Gameplay;
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
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //TODO: Make a scene Maanager
        //private GameplayScene MainGameplayScene = new GameplayScene();
        private GameplayScene GamePlayScene;
        private PauseScene PauseScene = new PauseScene();
        private GameEndScene GameEndScene = new GameEndScene();
        private IGameScene currentScene;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = ResolutionData.Resolution.X;
            _graphics.PreferredBackBufferHeight = ResolutionData.Resolution.Y;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //!!! DO NOT DO ANYTHING BEFORE base.Initialize() !!!
            base.Initialize();

            EnemyID.Initialize();
            GamePlayScene = new();
            currentScene = GamePlayScene;
            currentScene.Initialize();
            PauseScene.Initialize();
/*            Ga.OnPause += (lastFrame) =>
            {
                currentScene = PauseScene;
                PauseScene.SetLastFrame(lastFrame);
            };

            MainGameplayScene.OnEnded += () => currentScene = GameEndScene;
*/
            //PauseScene.OnUnpause += () => currentScene = MainGameplayScene;
            PauseScene.OnExit += Exit;
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
            Resources.Hitcirle = Content.Load<Texture2D>("Hitcircle");
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
