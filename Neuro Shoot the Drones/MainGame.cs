using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Neuro_Shoot_the_Drones
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IGameScene currentScene = new GameplayScene();

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //!!! DO NOT DO ANYTHING BEFORE base.Initialize() !!!
            base.Initialize();
            currentScene.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Resources.PlayerTextureAtlas = Content.Load<Texture2D>("PlayerAtlas");
            Resources.GameFrameUI = Content.Load<Texture2D>("GameFrame");
            Resources.BulletTextureAtlas = Content.Load<Texture2D>("Bullets");
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
