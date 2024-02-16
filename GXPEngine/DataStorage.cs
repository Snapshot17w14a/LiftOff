using GXPEngine.Core;
using GXPEngine.UI;
using Melanchall.DryWetMidi.Multimedia;
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
        private readonly Game _main = Game.main;

        //The coodinates of the targets where the enemies will spawn, and where the player can shoot at
        private readonly Vector2[] _targetVectors = { new Vector2(60, 175), new Vector2(960, 35), new Vector2(1860, 175), new Vector2(1860, 900), new Vector2(960, 1040), new Vector2(60, 900) };
        private readonly Vector2[] _tapVectors = { new Vector2(760, 450), new Vector2(960, 370), new Vector2(1160, 450), new Vector2(1160, 600), new Vector2(960, 676), new Vector2(760, 600) };

        //The keys that the player can use to shoot at the targets
        private readonly int[] _inputKeys = { Key.R, Key.T, Key.Y, Key.H, Key.G, Key.F };

        //The speed of the bullets and the enemies
        private readonly float _bulletSpeed = 0.3f;
        private readonly float _enemySpeed = 0.1f;

        //The time in second between enemy spawns
        private readonly float _enemySpawnInterval = 2;

        //Song parameters
        private readonly float _songDelay = 2; //The delay in seconds before the song starts

        //Set to true if you want to print the mouse data, and use other debug features
        private readonly bool _useDebug = true;

        //The current test scene
        private Scene _testScene;

        //The output device for the midi files
        private readonly OutputDevice _outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth");

        //Use this method to initialize the data such as Scenes, objects in Scenes, etc.
        //Look at the documentation of each parameter if you are stuck
        private void Initialize() 
        {
            _testScene = new Scene(); //Create a new Scene
            _testScene.SetBackground("background.png"); //Set the background of the scene
            //_testScene.SetAlignment(Scene.Alignment.CENTER, Scene.Alignment.CENTER); //Set the alignment of the scene
            //_testScene.CreateButton("square.png", 400, 300); //Create a button with the given filename and position
            //_testScene.CreateButton("square.png", 400, 400, new Scene()); //Create a button with the given filename and position
            //EasyDraw _testSceneCanvas = _testScene.Canvas; //Get the canvas of the scene
            //_testSceneCanvas.Fill(0, 0, 0); //Fill the canvas with the given color
            //_testSceneCanvas.Text("Test Scene", 400, 300); //Write the given text on the canvas
            _sceneManager.LoadScene(_testScene); //Load the scene
            if(_useDebug) Game.main.OnAfterStep += PrintMouseData; //Print the mouse data
        }
        private void PrintMouseData() => Console.WriteLine("Mouse X: " + Input.mouseX + " Mouse Y: " + Input.mouseY);

        //Getters and Setters - DO NOT REMOVE
        public Game MainGame => _main;
        public Vector2[] TargetVectors => _targetVectors;
        public Vector2[] TapVectors => _tapVectors;
        public int[] InputKeys => _inputKeys;
        public float BulletSpeed => _bulletSpeed;
        public float EnemySpeed  => _enemySpeed;
        public float EnemySpawnInterval => _enemySpawnInterval;
        public float SongDelay => _songDelay;
        public OutputDevice OutputDevice => _outputDevice;
    }
}
