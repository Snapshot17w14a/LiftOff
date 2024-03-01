using System.Collections.Generic;

namespace GXPEngine.UI
{
    internal class SceneManager
    {
        public IDictionary<string, Scene> scenes = new Dictionary<string, Scene>();
        private static SceneManager _instance;
        private Scene _sceneToLoad;

        public Scene CurrentScene { get; private set; }

        private SceneManager() { }

        public static SceneManager Instance
        {
            get
            {
                if (_instance == null) { _instance = new SceneManager(); }
                return _instance;
            }
        }

        private void CheckForFade()
        { 
            if (!CurrentScene.IsFadePlaying)
            {
                Game.main.OnBeforeStep -= CheckForFade;
                Load(_sceneToLoad);
            }
        }

        /// <summary>Load the scene with the given name</summary>
        /// <param name="sceneName">The name of the scene that was given during instantiation</param>
        public void LoadScene(string sceneName)
        {
            if (_sceneToLoad != null) _sceneToLoad = null;
            CurrentScene?.OnUnload();
            if (CurrentScene.IsFadePlaying) { _sceneToLoad = scenes[sceneName]; Game.main.OnBeforeStep += CheckForFade; } 
            else Load(scenes[sceneName]);
        }

        private void Load(Scene scene)
        {
            var game = Game.main;
            if (CurrentScene != null)
            {
                game.RemoveChild(CurrentScene);
                game.OnAfterStep -= CurrentScene.UpdateObjects;
            }
            CurrentScene = scene;
            CurrentScene?.OnLoad();
            game.AddChild(CurrentScene);
            game.OnAfterStep += CurrentScene.UpdateObjects;
        }

        public void LoadInitialScene() => Load(scenes["InitialScene"]);
        public void AddSceneToDictionary(string sceneName, Scene scene) => scenes.Add(sceneName, scene);
    }
}
