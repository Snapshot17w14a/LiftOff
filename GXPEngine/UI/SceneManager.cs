namespace GXPEngine.UI
{
    internal class SceneManager
    {
        private readonly Game _game = Game.main;
        private static SceneManager _instance;
        private Scene _currentScene;

        public Scene CurrentScene => _currentScene;

        private SceneManager() { }

        public static SceneManager Instance
        {
            get
            {
                if (_instance == null) { _instance = new SceneManager(); }
                return _instance;
            }
        }

        public void LoadScene(Scene scene)
        {
            if (_currentScene != null)
            {
                _game.OnAfterStep -= _currentScene.UpdateObjects;
                _game.RemoveChild(_currentScene);
            }
            _currentScene = scene;
            _game.AddChild(_currentScene);
            _game.OnAfterStep += _currentScene.UpdateObjects;
        }
    }
}
