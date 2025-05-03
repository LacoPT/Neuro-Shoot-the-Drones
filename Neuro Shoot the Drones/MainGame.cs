using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories;

namespace Neuro_Shoot_the_Drones
{
    //TODO: Finish architecture migration to ECS
    //TODO: Add collsions check
    //TODO: Add Player stats - Health, Bombs, Score, Graze, Power
    //TODO: Add levels System
    //TODO: Add bosses to architecture
    //TODO: Consider making ID superclass for EnemyID, PatternID or LevelID
    //NOTE: It was to late to rewrite all of the code when i found out the existence of ComponentModel namespace
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
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
            currentScene.Initialize();
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
            Resources.DefaultFont = Content.Load<SpriteFont>("DefaultFont");
            Resources.BlackPixel = Content.Load<Texture2D>("blackPixel");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
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
