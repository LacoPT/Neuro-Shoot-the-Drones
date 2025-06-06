using Neuro_Shoot_the_Drones.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Neuro_Shoot_the_Drones.Gameplay.GUI
{
    internal class GUIModel : BaseModel
    {
        private int _score = 0;
        private int _highScore = 0;
        private int _health = 5;
        private int _healthShards = 0;
        private int _bombs = 0;
        private int _bombShards = 1;

        const int MaxHealth = 6;
        const int MaxBombs = 6;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                NotifyPropertyChanged();
                if(_score > _highScore)
                    HighScore = _score;
            }
        }

        public int HighScore
        {
            get => _highScore;
            private set
            {
                _highScore = value;
                NotifyPropertyChanged();
            }
        }

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                _health = Math.Clamp(_health, 0, MaxHealth);
                NotifyPropertyChanged();
            }
        }

        public int HealthShards

        {
            get => _healthShards;
            set
            {
                _healthShards = value;

                if (value < 0) _healthShards = 0;
                else if (value > 3)
                {
                    Health += 1;
                    _healthShards = 0;
                }
               NotifyPropertyChanged();
            }
        }

        public int Bombs

        {
            get => _bombs;
            set
            {
                _bombs = value;
                _bombs = Math.Clamp(_bombs, 0, MaxHealth);
                NotifyPropertyChanged();
            }
        }

        public int BombShards
        {
            get => _bombShards;
            set
            {
                _bombShards = value;
                if (value < 0) _bombShards = 0;
                else if (value > 3)
                {
                    Health += 1;
                    _bombShards = 0;
                }
                NotifyPropertyChanged();
            }
        }

        public int DisplayScore;
        public int DisplayHighScore;
    }
}
