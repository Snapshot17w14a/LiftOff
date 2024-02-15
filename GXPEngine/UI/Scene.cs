using System.Collections.Generic;
using GXPEngine.UI.Interactables;

namespace GXPEngine.UI
{
    internal class Scene : GameObject
    {
        private readonly EasyDraw _canvas = new EasyDraw(Game.main.width, Game.main.height);
        private Sprite _background;
        private Alignment _verticalAlignment;
        private Alignment _horizontalAlignment;
        public List<Button> Buttons { get; } = new List<Button>();
        public EasyDraw Canvas => _canvas;

        public enum Alignment
        {
            MIN,
            CENTER,
            MAX
        }
        public Scene() { }

        public void UpdateObjects()
        {
            foreach (Button button in Buttons)
            {
                if (button.HitTestPoint(Input.mouseX, Input.mouseY) && Input.GetMouseButtonDown(0)) { button.OnClick(); }
            }
        }

        /// <summary>Set the background of the scene</summary>
        /// <param name="filename">The filename of the button texture in the bin/debug folder</param>
        public void SetBackground(string filename)
        {
            _background?.Destroy();
            _background = new Sprite(filename, false, false);
            _background.SetOrigin(_background.width / 2, _background.height / 2);
            _background.SetXY(game.width / 2, game.height / 2);
            _background.width = game.width;
            _background.height = game.height;
            AddChild(_background);
        }

        /// <summary>Creates a button with the given filename and position</summary>
        /// <param name="filename">The filename of the button texture in the bin/debug folder</param>
        public void CreateButton(string filename, int x, int y)
        {
            Button button = new Button(filename);
            SetAnchor(button);
            button.SetXY(x, y);
            Buttons.Add(button);
            AddChild(button);
        }

        /// <summary>Creates a button with the given filename and position</summary>
        /// <param name="filename">The filename of the button texture in the bin/debug folder</param>
        /// <param name="nextScene">The scene which the button should load when clicked</param>
        public void CreateButton(string filename, int x, int y, Scene nextScene)
        {
            Button button = new Button(filename, nextScene);
            SetAnchor(button);
            button.SetXY(x, y);
            Buttons.Add(button);
            AddChild(button);
        }

        /// <summary>Create an animated image with the given filename, rows, columns and position</summary>
        /// <param name="filename">The filename of the button texture in the bin/debug folder</param>
        /// <param name="cols">The number of colums in the source image</param>
        /// <param name="rows">The number of rows in the source image</param>
        public void AnimatedImage(string filename, int rows, int cols, int x, int y)
        {
            AnimationSprite sprite = new AnimationSprite(filename, rows, cols);
            SetAnchor(sprite);
            sprite.SetXY(x, y);
            AddChild(sprite);
        }

        /// <summary>Sets the alignment of the objects in the scene</summary>
        /// <param name="horizontal">The horizontal alignment of objects; Min is Left, Center is Middle, Max is Right</param>
        /// <param name="vertical">The vertical alignment of objects; Min is Top, Center is Middle, Max is Bottom</param>
        public void SetAlignment(Alignment horizontal, Alignment vertical)
        {
            _horizontalAlignment = horizontal;
            _verticalAlignment = vertical;
        }

        private void SetAnchor(Sprite obj)
        {
            int anchorx = 0;
            int anchory = 0;
            switch (_horizontalAlignment)
            {
                case Alignment.MIN:
                    anchorx = 0;
                    break;
                case Alignment.CENTER:
                    anchorx = obj.width / 2;
                    break;
                case Alignment.MAX:
                    anchorx = obj.width;
                    break;
            }
            switch (_verticalAlignment)
            {
                case Alignment.MIN:
                    anchory = 0;
                    break;
                case Alignment.CENTER:
                    anchory = obj.height / 2;
                    break;
                case Alignment.MAX:
                    anchorx = obj.height;
                    break;
            }
            obj.SetOrigin(anchorx, anchory);
        }
    }
}
