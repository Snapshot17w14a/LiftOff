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

        /// <summary>Load the scene with the given name, returns false if the scene does not exist, otherwise returns true</summary>
        /// <param name="sceneName"></param>
        /// <returns>The name of the scene that was given during instantiation</returns>
        public bool LoadScene(string sceneName)
        {
            if (!scenes.ContainsKey(sceneName)) return false;
            var game = Game.main;
            if (CurrentScene != null)
            {
                game.OnAfterStep -= CurrentScene.UpdateObjects;
                game.RemoveChild(CurrentScene);
            }
            CurrentScene = scenes[sceneName];
            game.AddChild(CurrentScene);
            game.OnAfterStep += CurrentScene.UpdateObjects;
            return true;
        }

        public void LoadInitialScene() => LoadScene("InitialScene");
        public void AddSceneToDictionary(string sceneName, Scene scene) => scenes.Add(sceneName, scene);
    }
}
