using GXPEngine.UI;

namespace GXPEngine.LevelManager
{
    internal class ScoreManager
    {
        private readonly Sound _missSound = new Sound("miss.wav");
        private Scene _parentScene;
        private Level _parentLevel;

        public int PlayerLives { get; private set; } = 6;
        public int ComboScore { get; private set; } = 0;
        private int _score = 0;

        public ScoreManager(Scene parentScene, Level parentLevel) { _parentScene = parentScene; _parentLevel = parentLevel; _parentScene.SceneUpdate += UpdateScoreDispay; }

        private void UpdateScoreDispay()
        {
            _parentScene.SetAlignment(Scene.Alignment.CENTER, Scene.Alignment.MIN, true);
            _parentScene.Canvas.Fill(DataStorage.ScoreColor);
            _parentScene.Canvas.Text($"Score: {_score} | COMBO: {ComboScore}", Game.main.width / 2, 40);
        }

        public void Hit()
        {
            _score++;
            ComboScore++;
        }

        public void Miss()
        {
            _missSound.Play();
            PlayerLives--;
            ComboScore = 0;
            _parentLevel.scale = 1;
            _parentLevel.rotation = 0;
            _parentScene.Background.scale = 1;
            _parentScene.Background.rotation = 0;
            _parentLevel.MiddlePie.SetPieSprite(PlayerLives);
        }

        public void Destroy()
        {
            _parentScene.SceneUpdate -= UpdateScoreDispay;
            _parentScene = null;
        }
    }
}
