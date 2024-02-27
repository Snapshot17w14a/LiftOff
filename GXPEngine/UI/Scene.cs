using System.Collections.Generic;
using GXPEngine.UI.Interactables;
using GXPEngine.LevelManager;
using System;
using System.Drawing;

namespace GXPEngine.UI
{
    internal class Scene : GameObject
    {
        public EasyDraw Canvas { get; } = new EasyDraw(Game.main.width, Game.main.height, false);
        public string Name { get; private set; }
        private Alignment _horizontalAlignment;
        private Alignment _verticalAlignment;
        private Sprite _background;
        private Level _sceneLevel;
        private Player _player;
        public List<Button> Buttons { get; } = new List<Button>();
        public Level SceneLevel => _sceneLevel;

        ///<summary>Add method to be called every frame for the scene</summary>
        public Action SceneUpdate;
        public enum Alignment
        {
            MIN,
            CENTER,
            MAX
        }
        public Scene(string sceneName)
        {
            Name = sceneName;
            AddChild(Canvas);
            SceneManager.Instance.AddSceneToDictionary(sceneName, this);
        }

        public void UpdateObjects()
        {
            Canvas.Clear(Color.Transparent);
            foreach (Button button in Buttons)
            {
                if (button.HitTestPoint(Input.mouseX, Input.mouseY) && Input.GetMouseButtonDown(0)) { button.OnClick(); }
            }
            SceneUpdate?.Invoke();
        }

        /// <summary>Set the background of the scene</summary>
        /// <param name="filename">The filename of the button texture in the bin/debug folder</param>
        public void SetBackground(string filename)
        {
            RemoveChild(Canvas);
            _background?.Destroy();
            _background = new Sprite(filename, false, false);
            _background.SetOrigin(_background.width / 2, _background.height / 2);
            _background.SetXY(game.width / 2, game.height / 2);
            _background.width = game.width;
            _background.height = game.height;
            AddChild(_background);
            AddChild(Canvas);
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

        /// <summary>Creates a button with the given filename and position</summary>
        /// <param name="filename">The filename of the button texture in the bin/debug folder</param>
        /// <param name="nextSceneName">The name of the scene which the button should load when clicked</param>
        public void CreateButton(string filename, int x, int y, string nextSceneName)
        {
            Button button = new Button(filename, nextSceneName);
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
        public void SetAlignment(Alignment horizontal, Alignment vertical, bool isCanvasObject = false)
        {
            _horizontalAlignment = horizontal;
            _verticalAlignment = vertical;
            if(isCanvasObject) SetCanvasAlignment();
        }

        public void SetCanvasColor(int red, int green, int blue, int alpha = 255) => Canvas.Fill(red, green, blue, alpha);

        /// <summary>Create a level for the current scene with the provided filename used for the notes of the level</summary>
        /// <param name="filename">The filename of the midi file used for the noted in the level</param>
        public void CreateLevel(string filename, Scene scene) { _sceneLevel = new Level(filename, scene); SceneUpdate += _sceneLevel.PlayHitNotes; }

        /// <summary>Create a player with the given filename and position</summary>
        /// <param name="filename">The filename of the sprite used for the player</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        public void CreatePlayer(string filename, int x, int y) { _player = new Player(filename); SetAnchor(_player); _player.SetXY(x, y); AddChild(_player); }

        private void SetCanvasAlignment()
        {
            CenterMode horizontal = CenterMode.Min;
            CenterMode vertical = CenterMode.Min;
            switch (_horizontalAlignment)
            {
                case Alignment.MIN:
                    horizontal = CenterMode.Min;
                    break;
                case Alignment.CENTER:
                    horizontal = CenterMode.Center;
                    break;
                case Alignment.MAX:
                    horizontal = CenterMode.Max;
                    break;

            }
            switch(_verticalAlignment)
            {
                case Alignment.MIN:
                    vertical = CenterMode.Min;
                    break;
                case Alignment.CENTER:
                    vertical = CenterMode.Center;
                    break;
                case Alignment.MAX:
                    vertical = CenterMode.Max;
                    break;
            }
            Canvas.TextAlign(horizontal, vertical);
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
