using System.Drawing;

namespace GXPEngine.UI.Scenes
{
    internal class InitialScene : Scene
    {
        private bool[] _textPlayDirection = new bool[1];
        private float[] _textTimers = new float[1]; 

        public InitialScene() : base("InitialScene") { Initialize(); }

        private void Initialize()
        {
            for (int i = 0; i < _textPlayDirection.Length; i++) _textPlayDirection[i] = Utils.Random(0, 100) >= 50;
            for (int i = 0; i < _textTimers.Length; i++) _textTimers[i] = Utils.Random(0f, 1f);
            SetBackground("titlebackground.png");
            SetAlignment(Alignment.CENTER, Alignment.CENTER, true);
            CreateTexts();
        }

        protected override void Draw()
        {
            for(int i = 0; i < _textTimers.Length; i++) UpdateTimer(i);
            MoveTitle();
            CheckInput();
        }

        private void CheckInput()
        {
            if (Input.AnyKey()) SceneManager.Instance.LoadScene("SickScene");
        }

        private void MoveTitle()
        {
            for(int i = 0; i < TextObjects.Count; i++)
            {
                TextObjects[i].rotation = Mathf.Lerp(-2, 2, Mathf.EaseInOut(_textTimers[i]));
                TextObjects[i].scale = Mathf.Lerp(1f, 1.05f, Mathf.EaseInOut(_textTimers[i]) / 2);
            }
        }

        private void UpdateTimer(int i)
        {
            if (_textPlayDirection[i] && _textTimers[i] >= 1) { _textPlayDirection[i] = false; } //ColorTitle(i); }
            else if (!_textPlayDirection[i] && _textTimers[i] <= 0) { _textPlayDirection[i] = true; } //ColorTitle(i);}
            _textTimers[i] += _textPlayDirection[i] ? Time.deltaTime / 1000f : -Time.deltaTime / 1000f;
        }

        private void CreateTexts()
        {
            SetTextObjectFont("Foont.ttf", 32);
            Text("Press any key to play", game.width / 2, 900, Color.FromArgb(3220022));
        }

        private void ColorTitle(int i) => TextObjects[i].Fill(Color.FromArgb(255, Utils.Random(0, 256), Utils.Random(0, 256), Utils.Random(0, 256)));
    }
}
