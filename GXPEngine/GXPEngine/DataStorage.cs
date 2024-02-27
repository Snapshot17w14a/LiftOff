using Melanchall.DryWetMidi.Multimedia;
using GXPEngine.UI.Scenes;
using GXPEngine.Core;
using GXPEngine.UI;
using System.Drawing;
using System;

namespace GXPEngine
{
    internal class DataStorage
    {
        //Singleton class to store the data of the game
        private DataStorage() { _ = SceneManager.Instance; Initialize(); }
        private static DataStorage _instance;
        public static DataStorage Instance
        {
            get
            {
                if (_instance == null) { _instance = new DataStorage(); }
                return _instance;
            }
        }

        //The coodinates of the targets where the enemies will spawn, and where the player can shoot at
        private static readonly Vector2[] _targetVectors = { new Vector2(515, 0), new Vector2(1400, 0), new Vector2(1920, 540), new Vector2(1400, 1080), new Vector2(515, 1080), new Vector2(0, 540) };
        private static readonly Vector2[] _tapVectors = { new Vector2(915, 465), new Vector2(1000, 465), new Vector2(1050, 540), new Vector2(1010, 620), new Vector2(910, 615), new Vector2(870, 540) };

        //The keys that the player can use to shoot at the targets
        private static readonly int[] _inputKeys = { Key.E, Key.O, Key.K, Key.M, Key.C, Key.D };

        //The speed of the bullets and the enemies
        private static readonly float _bulletSpeed = 0.3f;
        private static readonly float _enemySpeed = 0.1f;

        //The time in second between enemy spawns
        private readonly float _enemySpawnInterval = 2;

        //The delay in seconds before the song starts
        private static readonly float _songDelay = 2; 

        //Animation parameters
        private readonly int _animationSpeed = 100; //The speed of the animations

        //Set to true if you want to print the mouse data, and use other debug features
        private readonly bool _useDebug = false;

        //The color of the score text
        private static readonly Color _scoreColor = Color.FromArgb(0xff0063);

        //The first scene to be loaded

        //The output device for the midi files
        private static readonly OutputDevice _outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth");

        //Use this method to initialize the data such as Scenes, objects in Scenes, etc.
        //Look at the documentation of each parameter if you are stuck
        private void Initialize()
        {
            if(_useDebug) Game.main.OnAfterStep += PrintMouseData; //Print the mouse data is debug is enabled
            InstantiateScenes(); //Instantiate the scenes
            SceneManager.Instance.LoadInitialScene(); //Load the initial scene
        }

        private void InstantiateScenes()
        {
            _ = new InitialScene();
            _ = new SickSongScene();
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
        public static Color ScoreColor => _scoreColor;
        public static OutputDevice OutputDevice => _outputDevice;
    }
}
