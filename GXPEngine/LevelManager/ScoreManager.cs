using GXPEngine.UI;
using System;

namespace GXPEngine.LevelManager
{
    internal class ScoreManager
    {
        private Sound _missSound = new Sound("miss.wav");
        private Scene _parentScene;
        private int _comboScore = 0;
        private int _score = 0;
        public ScoreManager(Scene parentScene) { _parentScene = parentScene; _parentScene.SceneUpdate += UpdateScoreDispay; }

        private void UpdateScoreDispay()
        {
            _parentScene.SetAlignment(Scene.Alignment.CENTER, Scene.Alignment.MIN, true);
            _parentScene.SetCanvasColor(231, 123, 52);
            _parentScene.Canvas.Text($"Score: {_score} | COMBO: {_comboScore}", Game.main.width / 2, 40);
        }
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
