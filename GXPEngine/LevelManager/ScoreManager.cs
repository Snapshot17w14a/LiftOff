using System;

namespace GXPEngine.LevelManager
{
    internal class ScoreManager
    {
        private Sound _missSound = new Sound("miss.wav");
        private int _comboScore = 0;
        private int _score = 0;
        public ScoreManager() { }

        public void Hit()
        {
            _score++;
            _comboScore++;
        }
        public void Miss()
        {
            _missSound.Play();
            _comboScore = 0;
        }
    }
}
