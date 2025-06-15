using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.Gameplay;
using Neuro_Shoot_the_Drones.Gameplay.Enemies.EnemyFactories;
using Neuro_Shoot_the_Drones.Menus;
using Neuro_Shoot_the_Drones.Menus.Pause;

namespace Neuro_Shoot_the_Drones
{
    //TODO: Make Bombs
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //TODO: Make a scene Maanager

        SceneManager SceneManager = new();

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
            base.Initialize();
            Resources.Initialize(Content);

            EnemyID.Initialize();
            SceneManager.Initialize(_spriteBatch);
            SceneManager.OnExit += Exit;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Resources.Initialize(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SceneManager.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SceneManager.Draw(_spriteBatch, gameTime);
            base.Draw(gameTime);
        }
    }
}
