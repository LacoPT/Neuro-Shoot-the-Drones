using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.MVC;
using Neuro_Shoot_the_Drones.Tweens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay.GUI
{
    internal class GUIController : BaseController
    {
        Tween ScoreTween = new(0, 0);
        Tween HighScoreTween = new(0, 0);
        public GUIController(GUIModel model, GUIView view) : base(model, view)
        {
            model.PropertyChanged += ModelPropertyChanged;
        }

        public void Update(GameTime gameTime)
        {
            ScoreTween.Update(gameTime);
            HighScoreTween.Update(gameTime);
        }

        void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var model = (GUIModel)Model;
            switch(e.PropertyName)
            {
                case "Score":
                    var oldScore = model.DisplayScore;
                    var newScore = model.Score;
                    ScoreTween.Interrupt();
                    ScoreTween = new Tween(oldScore, newScore, 0.3);
                    ScoreTween.OnUpdate += () => model.DisplayScore = (int)ScoreTween.Value;
                    ScoreTween.Start();
                    break;
                case "HighScore":
                    var oldHighScore = model.DisplayHighScore;
                    var newHighScore = model.HighScore;
                    ScoreTween.Interrupt();
                    ScoreTween = new Tween(oldHighScore, newHighScore, 0.3);
                    ScoreTween.OnUpdate += () => model.DisplayScore = (int)ScoreTween.Value;
                    ScoreTween.Start();
                    break;
                case "Health":
                    break;
                case "Bombs":
                    break;
                case "HealthShards":
                    break;
                case "BombShards":
                    break;
                default:
                    throw new Exception("No such property!");
            }
        }
    }
}
