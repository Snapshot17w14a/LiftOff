using GXPEngine.UI.Scenes;
using System.Collections.Generic;

namespace GXPEngine.UI
{
    internal class SceneManager
    {
        public IDictionary<string, Scene> scenes = new Dictionary<string, Scene>();
        private static SceneManager _instance;

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

        /// <summary>Load the scene with the given name</summary>
        /// <param name="sceneName">The name of the scene that was given during instantiation</param>
        /// <returns>Returns false if the scene does not exist or failed to load, otherwise returns true</returns>
        public bool LoadScene(string sceneName)
        {
            if (!scenes.ContainsKey(sceneName)) return false;
            var game = Game.main;
            if (CurrentScene != null)
            {
                game.RemoveChild(CurrentScene);
                game.OnAfterStep -= CurrentScene.UpdateObjects;
            }
            CurrentScene?.OnUnload();
            CurrentScene = scenes[sceneName];
            CurrentScene?.OnLoad();
            game.AddChild(CurrentScene);
            game.OnAfterStep += CurrentScene.UpdateObjects;
            return true;
        }

        public bool LoadInitialScene() => LoadScene("InitialScene");
        public void AddSceneToDictionary(string sceneName, Scene scene) => scenes.Add(sceneName, scene);
    }
}
