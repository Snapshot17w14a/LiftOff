using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Core;
using System.Diagnostics;
using GXPEngine.Core;
using GXPEngine.UI;
using System;

namespace GXPEngine.LevelManager
{
    internal class Level : GameObject
    {
        //Tweakable variables for each level c, d, e, g, a, b
        private readonly NoteName[] _laneNotes = { NoteName.C, NoteName.D, NoteName.E, NoteName.G, NoteName.A, NoteName.B };
        private readonly Sound _deathSound = new Sound("ded.wav");
        private float _overlayTimer = 0f;
        private bool isGameOver = false;
        private int _finalScore = 0;

        public Lane[] LevelLanes { get; private set; } = new Lane[6];
        public ScoreManager LevelScoreManager { get; private set; }
        public SongManager LevelSongManager { get; private set; }
        public Stopwatch LevelSongTimer { get; private set; }
        public MidiFile LevelMidiFile { get; private set; }
        public Player LevelPlayer { get; private set; }
        public Pie MiddlePie { get; private set; }
        public double MarginOfError { get; private set; } = 0.2; //The margin of error in seconds
        public uint CurrentRedTint { get; private set; }
        public float NoteTime { get; private set; } = 2; //The time in seconds it takes for the note to travel from the start to the finish
        public int InputDelay { get; private set; } = 0; //The delay in milliseconds between the input and the note
        public int NoteCount { get; set; }

        private readonly Scene _parentScene;

        public float SongDelay => DataStorage.SongDelay;

        public Level(string filename, Scene scene) 
        {
            _parentScene = scene;
            CreateLanes();
            CreateInstances(filename);
            scene.AddChild(this);
            scene.SceneUpdate += Update;
        }

        private void Update()
        {
            if(!isGameOver) GameTick();
            else
            {
                _overlayTimer += Time.deltaTime / 1000f;
                if (_overlayTimer >= 3) 
                {
                    if (_parentScene.Overlay == null) _parentScene.CreateOverlay("gameover.png");
                    _parentScene.SetAlignment(Scene.Alignment.CENTER, Scene.Alignment.MIN, true);
                    _parentScene.Canvas.Fill(DataStorage.ScoreColor);
                    _parentScene.Canvas.Text($"Score: {_finalScore}", Game.main.width / 2, 20);
                    if (_overlayTimer >= 8) SceneManager.Instance.LoadScene("InitialScene");
                }
            }
        }

        private void GameTick()
        {
            if (LevelSongTimer.IsRunning && NoteCount == 0) ResetLevel();
            NegateScaleAndRotation();
            UpdateRedTint();
        }

        private void CreatePlayer(string filename, int cols, int rows)
        {
            LevelPlayer = new Player(filename, cols, rows);
            AddChild(LevelPlayer);
        }

        private void NegateScaleAndRotation()
        {
            rotation += rotation * -1 / 20;
            _parentScene.Background.rotation += _parentScene.Background.rotation * -1 / 20;
            scale = scale <= 1 ? 1 : scale >= 1.2f ? 1.2f : scale - 0.0003f; //Clamp the scale
            _parentScene.Background.scale = _parentScene.Background.scale <= 1 ? 1 : _parentScene.Background.scale >= 1.2f ? 1.2f : _parentScene.Background.scale - 0.0003f; //Clamp the scale
        }

        private void CreateLanes()
        {
            for (int i = 0; i < LevelLanes.Length; i++)
            {
                var pos = DataStorage.SpawnVectors[i] - new Vector2(game.width / 2, game.height / 2);
                LevelLanes[i] = new Lane(i, _laneNotes[i], _parentScene, this) { x = pos.x, y = pos.y };
            }
        }

        private void CreateInstances(string filename)
        {
            LevelMidiFile = MidiFile.Read(filename);
            LevelScoreManager = new ScoreManager(_parentScene, this);
            LevelSongManager = new SongManager(LevelMidiFile, _parentScene, this);
            MiddlePie = new Pie("Pies/pie6.png", this);
            LevelSongTimer = new Stopwatch();
            CreatePlayer("granny.png", 3, 6);
        }

        private void UpdateRedTint()
        {
            int num = Mathf.Clamp(LevelScoreManager.ComboScore, 0, 15);
            CurrentRedTint = Convert.ToUInt32(255 << 16 | 255 - num * 4 << 8 | 255 - num * 4);
            _parentScene.SetCanvasClearColor(255, 0, 0, num);
        }

        private void ResetLevel()
        {
            NoteTime *= 0.9f;
            MarginOfError -= 0.02;
            LevelScoreManager.AddLives();
            LevelSongTimer.Restart();
            foreach (var lane in LevelLanes) lane.Reset();
            LevelSongManager.Reset();
        }

        private void GameOver()
        {
            SoundChannel sc = _deathSound.Play();
            sc.Frequency *= 0.6f;
            LevelPlayer.GameOver();
            isGameOver = true;
            _finalScore = LevelScoreManager.Score;
            LevelSongManager.StopSong();
            DestroyObjects();
        }

        private void DestroyObjects()
        {
            LevelScoreManager.Destroy();
            LevelScoreManager = null;
            LevelSongManager.Destroy();
            LevelSongManager = null;
            MiddlePie.Destroy();
            MiddlePie = null;
            LevelSongTimer.Stop();
            foreach (var lane in LevelLanes) lane.Destroy();
            
        }

        public void CheckForGameOver()
        {
            if (!isGameOver && LevelScoreManager.PlayerLives <= 0) GameOver();
        }

        public void Shake()
        {
            var multiplier = Mathf.Clamp(LevelScoreManager.ComboScore, 0, 15);
            var randomRotation = Utils.Random(0f, 0.1f);
            rotation += randomRotation * multiplier;
            _parentScene.Background.rotation += randomRotation * multiplier;
            var randomScale = Utils.Random(0f, 0.0015f);
            scale += randomScale * multiplier;
            _parentScene.Background.scale += randomScale * multiplier;
            if(Utils.Random(0f, 1f) <= 0.5f)
            {
                rotation *= -1;
                _parentScene.Background.rotation *= -1;
            }
        }

        protected override void OnDestroy()
        {
            if(!isGameOver) { DestroyObjects(); isGameOver = true; }
            _parentScene.SceneUpdate -= Update;
        }

        public double GetAudioSourceTime() => (double)LevelSongTimer.ElapsedMilliseconds / 1000;
    }
}
