using System;

namespace GXPEngine.LevelManager
{
    internal class ScoreManager
    {
        private Sound _hitSound = new Sound("hit.wav");
        private Sound _missSound = new Sound("miss.wav");
        private int _score = 0;
        private int _comboScore = 0;
        public ScoreManager() { DataStorage.Instance.MainGame.OnAfterStep += Update; }

        private void Update() { Console.WriteLine($"Score: {_score}, Combo: {_comboScore}"); }
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
