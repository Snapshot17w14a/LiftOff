namespace GXPEngine.LevelManager
{
    internal class ScoreManager
    {
        private Sound _hitSound = new Sound("hit.wav");
        private Sound _missSound = new Sound("miss.wav");
        private int _comboScore = 0;
        public ScoreManager() { }

        public void Hit()
        {
            _hitSound.Play();
            _comboScore++;
        }
        public void Miss()
        {
            _missSound.Play();
            _comboScore = 0;
        }
    }
}
