namespace GXPEngine
{
    internal class SceneManager
    {
        private static SceneManager _instance;
        private Game _game;
        private Scene _currentScene;
        private Scene _nextScene;
        private bool _sceneChanged = false;

        private SceneManager()
        {
            _game = Game.main;
        }

        public static SceneManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SceneManager();
                }
                return _instance;
            }
        }

        public void Update()
        {
            if (_sceneChanged)
            {
                _currentScene = _nextScene;
                _sceneChanged = false;
            }
        }

        public void LoadScene(Scene scene)
        {
            _nextScene = scene;
            _sceneChanged = true;
        }
    }
}
