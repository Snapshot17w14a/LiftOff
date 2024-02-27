using System.Collections.Generic;
using GXPEngine.UI.Interactables;
using GXPEngine.LevelManager;
using System.Drawing;
using System;

namespace GXPEngine.UI
{
    internal class Scene : GameObject
    {

        private Alignment _horizontalAlignment;
        private Alignment _verticalAlignment;
        private Player _player;

        public EasyDraw Canvas { get; } = new EasyDraw(Game.main.width, Game.main.height, false);
        private Color ClearColor { get; set; } = Color.Transparent;
        private List<Button> Buttons { get; } = new List<Button>();
        public bool ClearAfterUpdate { get; set; } = true;
        public Sprite Background { get; private set; }
        public string Name { get; private set; }
        public Level SceneLevel { get;  set; }

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
            if(ClearAfterUpdate) Canvas.Clear(ClearColor);
            foreach (Button button in Buttons) if (Input.GetMouseButtonDown(0) && button.HitTestPoint(Input.mouseX, Input.mouseY)) { button.OnClick(); }
            SceneUpdate?.Invoke();
        }

        /// <summary>Set the background of the scene</summary>
        /// <param name="filename">The filename of the button texture in the bin/debug folder</param>
        public void SetBackground(string filename)
        {
            RemoveChild(Canvas);
            Background?.Destroy();
            Background = new Sprite(filename, false, false);
            Background.SetOrigin(Background.width / 2, Background.height / 2);
            Background.SetXY(game.width / 2, game.height / 2);
            Background.width = game.width;
            Background.height = game.height;
            AddChild(Background);
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
        /// <param name="filename">The filename of the midi file used for the notes in the level</param>
        public void CreateLevel(string filename) { SceneLevel = new Level(filename, this) { x = Game.main.width / 2, y = Game.main.height / 2 }; SceneUpdate += SceneLevel.PlayHitNotes; }

        /// <summary>Set the clear color of the canvas</summary>
        /// <param name="red">The red value of the color from 0-255</param>
        /// <param name="green">The green value of the color from 0-255</param>
        /// <param name="blue">The blue value of the color from 0-255</param>
        /// <param name="alpha">The alpha value of the color from 0-255</param>
        public void SetCanvasClearColor(int red, int green, int blue, int alpha) => ClearColor = Color.FromArgb(alpha, red, green, blue);

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

        public virtual void OnLoad() { }
        public virtual void OnUnload() { }
    }
}
