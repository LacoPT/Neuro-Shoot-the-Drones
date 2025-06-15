using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neuro_Shoot_the_Drones.Gameplay;
using Neuro_Shoot_the_Drones.Gameplay.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Menus
{
    //TODO:: Implement
    internal class GameEndScene : IGameScene
    {
        Texture2D resultBg;
        string Score;
        ControlSystem ControlSystem = new();

        public event Action OnEnterPressed;
        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(resultBg, Vector2.Zero, Color.White);
            sb.DrawString(Resources.DefaultFont, Score, new Vector2(200, 550), Color.White);
            sb.End();
        }

        public void Initialize()
        {
            ControlSystem.BindKeyHeld(Keys.Enter, (gt) => OnEnterPressed());
        }

        public void Update(GameTime gameTime)
        {
            ControlSystem.Update(gameTime);
        }

        public void ShowResult(GameResultData data)
        {
            resultBg = data.Survived ? Resources.EndComplete : Resources.EndDeath;
            Score = data.Score.ToString();
        }
    }
}
