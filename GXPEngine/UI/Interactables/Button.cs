using System;

namespace GXPEngine.UI.Interactables
{
    internal class Button : Sprite
    {
        private readonly string _nextSceneName;
        public Button(string filename) : base(filename, false, true) { collider.isTrigger = true; }
        public Button(string filename, Scene nextScene) : base(filename, false, true) { _nextSceneName = nextScene.name; collider.isTrigger = true; }
        public Button(string filename, string nextSceneName) : base(filename, false, true) { _nextSceneName = nextSceneName; collider.isTrigger = true; }

        public void OnClick()
        {
            if(_nextSceneName.Length != 0) { SceneManager.Instance.LoadScene(_nextSceneName); }
            else Console.WriteLine("Button Clicked!");
        }
    }
}
