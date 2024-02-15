using GXPEngine.Core;
using GXPEngine.UI;
using System;

namespace GXPEngine
{
    internal class DataStorage
    {
        //Singleton class to store the data of the game
        private DataStorage() { Initialize(); }
        private static DataStorage _instance;
        public static DataStorage Instance
        {
            get
            {
                if (_instance == null) { _instance = new DataStorage(); }
                return _instance;
            }
        }
        private readonly SceneManager _sceneManager = SceneManager.Instance;

        //The coodinates of the targets where the enemies will spawn, and where the player can shoot at
        private readonly Vector2[] _targetVectors = { new Vector2(60, 175), new Vector2(960, 35), new Vector2(1860, 175), new Vector2(1860, 900), new Vector2(960, 1040), new Vector2(60, 900) };

        //The keys that the player can use to shoot at the targets
        private readonly int[] _inputKeys = { Key.ONE, Key.TWO, Key.THREE, Key.FOUR, Key.FIVE, Key.SIX };

        //The speed of the bullets and the enemies
        private readonly float _bulletSpeed = 0.3f;
        private readonly float _enemySpeed = 0.1f;

        //The time in second between enemy spawns
        private readonly float _enemySpawnInterval = 2;

        //
        private readonly bool _useDebug = true;

        //The current test scene
        private Scene _testScene;

        //Use this method to initialize the data such as Scenes, objects in Scenes, etc.
        //Look at the documentation of each parameter if you are stuck
        private void Initialize() 
        {
            _testScene = new Scene(); //Create a new Scene
            _testScene.SetBackground("background.png"); //Set the background of the scene
            //_testScene.CreateButton("square.png", 400, 300); //Create a button with the given filename and position
            //_testScene.CreateButton("square.png", 400, 400, new Scene()); //Create a button with the given filename and position
            _sceneManager.LoadScene(_testScene); //Load the scene
            if(_useDebug) Game.main.OnAfterStep += PrintMouseData; //Print the mouse data
        }
        private void PrintMouseData() => Console.WriteLine("Mouse X: " + Input.mouseX + " Mouse Y: " + Input.mouseY);

        //Getters and Setters - DO NOT REMOVE
        public Vector2[] TargetVectors => _targetVectors;
        public int[] InputKeys => _inputKeys;
        public float BulletSpeed => _bulletSpeed;
        public float EnemySpeed  => _enemySpeed;
        public float EnemySpawnInterval => _enemySpawnInterval;
    }
}
