using System.Collections.Generic;
using GXPEngine.UI.Interactables;

namespace GXPEngine
{
    internal class Scene
    {
        private EasyDraw _canvas = new EasyDraw(Game.main.width, Game.main.height);
        private Alignment _verticalAlignment;
        private Alignment _horizontalAlignment;
        private List<GameObject> _buttons = new List<GameObject>();
        public List<GameObject> Buttons => _buttons;
        public EasyDraw Canvas => _canvas;

        public enum Alignment
        {
            Min,
            Center,
            Max
        }
        public Scene() { }

        private void Update()
        {
            foreach (Button button in _buttons)
            {
                if (button.HitTestPoint(Input.mouseX, Input.mouseY) && Input.GetMouseButtonDown(0))
                {
                    button.OnClick();
                }
            }
        }

        /// <summary>
        /// Creates a button with the given filename and position
        /// </summary>
        /// <param name="filename">The filename of the button texture in the bin/debug folder</param>
        public void CreateButton(string filename, int x, int y)
        {
            Button button = new Button(filename, x, y, _horizontalAlignment, _verticalAlignment);
            _buttons.Add(button);
        }

        public void AnimatedImage(string filename, int rows, int cols, int x, int y)
        {
            AnimationSprite sprite = new AnimationSprite(filename, rows, cols);

        }

        /// <summary>
        /// Sets the alignment of the objects in the scene
        /// </summary>
        /// <param name="horizontal">The horizontal alignment: Min - Left, Center - Middle, Max - Right</param>
        /// <param name="vertical">The vertical alignment: Min - Top, Center - Middle, Max - Bottom</param>
        public void SetAlignment(Alignment horizontal, Alignment vertical)
        {
            _horizontalAlignment = horizontal;
            _verticalAlignment = vertical;
        }

        private void SetAlignment(object obj)
        {
            int anchorx = 0;
            int anchory = 0;
            switch (_horizontalAlignment)
            {
                case Alignment.Min:
                    anchorx = 0;
                    break;
                case Alignment.Center:
                    anchorx = width / 2;
                    break;
                case Alignment.Max:
                    anchorx = width;
                    break;
            }
            switch (_verticalAlignment)
            {
                case Alignment.Min:
                    anchory = 0;
                    break;
                case Alignment.Center:
                    anchory = height / 2;
                    break;
                case Alignment.Max:
                    anchorx = height;
                    break;
            }
            SetOrigin(anchorx, anchory);
        }
    }
}
