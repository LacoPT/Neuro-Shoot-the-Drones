using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Gameplay
{
    internal class GameResultData
    {
        public bool Survived;
        public int Score;

        public GameResultData(bool survived, int score)
        {
            Survived = survived;
            Score = score;
        }
    }
}
