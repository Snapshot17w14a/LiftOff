using Melanchall.DryWetMidi.Multimedia;
using GXPEngine.UI.Scenes;
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

        //Instanticate the SceneManager
        private readonly SceneManager _sceneManager = SceneManager.Instance;

        //The coodinates of the targets where the enemies will spawn, and where the player can shoot at
        private static readonly Vector2[] _targetVectors = { new Vector2(410, 0), new Vector2(1500, 0), new Vector2(1920, 530), new Vector2(1500, 1080), new Vector2(385, 1080), new Vector2(0, 540) };
        private static readonly Vector2[] _tapVectors = { new Vector2(845, 390), new Vector2(1080, 390), new Vector2(1185, 530), new Vector2(1075, 660), new Vector2(845, 660), new Vector2(735, 530) };

        //The keys that the player can use to shoot at the targets
        private static readonly int[] _inputKeys = { Key.E, Key.O, Key.K, Key.M, Key.C, Key.D };

        //The speed of the bullets and the enemies
        private static readonly float _bulletSpeed = 0.3f;
        private static readonly float _enemySpeed = 0.1f;

        //The time in second between enemy spawns
        private readonly float _enemySpawnInterval = 2;

        //Song parameters
        private static readonly float _songDelay = 2; //The delay in seconds before the song starts

        //Animation parameters
        private readonly int _animationSpeed = 100; //The speed of the animations

        //Set to true if you want to print the mouse data, and use other debug features
        private readonly bool _useDebug = false;


        //All scenes
        private static readonly Scene[] _Scenes =
        {
            new InitialScene("InitialScene"),
            new Scene("TestScene")
        };

        //The first scene to be loaded
        public static Scene FirstSceneToLoad = _Scenes[0];

        //The output device for the midi files
        private static readonly OutputDevice _outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth");

        //Use this method to initialize the data such as Scenes, objects in Scenes, etc.
        //Look at the documentation of each parameter if you are stuck
        private void Initialize()
        {
            if(_useDebug) Game.main.OnAfterStep += PrintMouseData; //Print the mouse data is debug is enabled
            SceneManager.Instance.LoadInitialScene(); //Load the initial scene
        }
        private void PrintMouseData() => Console.WriteLine("Mouse X: " + Input.mouseX + " Mouse Y: " + Input.mouseY);

        //Getters and Setters - DO NOT REMOVE
        public static Vector2[] TargetVectors => _targetVectors;
        public static Vector2[] TapVectors => _tapVectors;
        public static int[] InputKeys => _inputKeys;
        public static float BulletSpeed => _bulletSpeed;
        public float EnemySpeed  => _enemySpeed;
        public float EnemySpawnInterval => _enemySpawnInterval;
        public static float SongDelay => _songDelay;
        public float AnimationSpeed => _animationSpeed;
        public static OutputDevice OutputDevice => _outputDevice;
    }
}
