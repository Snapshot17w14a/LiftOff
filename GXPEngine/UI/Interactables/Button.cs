using System;

namespace GXPEngine.UI.Interactables
{
    internal class Button : Sprite
    {
        private readonly Scene _nextScene;
        public Button(string filename) : base(filename, false, true) { collider.isTrigger = true; }
        public Button(string filename, Scene nextScene) : base(filename, false, true) { _nextScene = nextScene; collider.isTrigger = true; }

        public void OnClick()
        {
            if(_nextScene != null) { SceneManager.Instance.LoadScene(_nextScene); }
            else Console.WriteLine("Button Clicked!");
        }
    }
}
