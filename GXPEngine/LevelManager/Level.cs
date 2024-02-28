using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Core;
using System.Collections.Generic;
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

        private readonly int _inputDelay = 0; //The delay in milliseconds between the input and the note
        private double _marginOfError = 0.2; //The margin of error in seconds
        private float _noteTime = 2; //The time in seconds it takes for the note to travel from the start to the finish

        public Lane[] LevelLanes { get; private set; } = new Lane[6];
        public ScoreManager LevelScoreManager { get; private set; }
        public SongManager LevelSongManager { get; private set; }
        public Stopwatch LevelSongTimer { get; private set; }
        public MidiFile LevelMidiFile { get; private set; }
        //public List<TrackChunk> LevelTrackChunks { get; set; }
        public uint CurrentRedTint { get; private set; }
        public int NoteCount { get; set; }

        private readonly Scene _parentScene;

        //Getter - DO NOT REMOVE
        public float SongDelay => DataStorage.SongDelay;
        public double MarginOfError => _marginOfError;
        public int InputDelay => _inputDelay;
        public float NoteTime => _noteTime;

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
            if(LevelSongTimer.IsRunning && NoteCount == 0) ResetLevel();
            //PlayHitNotes();
            NegateScaleAndRotation();
            UpdateRedTint();
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
                var pos = DataStorage.TargetVectors[i] - new Vector2(game.width / 2, game.height / 2);
                LevelLanes[i] = new Lane(i, _laneNotes[i], _parentScene, this) { x = pos.x, y = pos.y };
            }
        }

        private void CreateInstances(string filename)
        {
            LevelMidiFile = MidiFile.Read(filename);
            //LevelTrackChunks = new List<TrackChunk>();
            LevelScoreManager = new ScoreManager(_parentScene, this);
            LevelSongManager = new SongManager(LevelMidiFile, _parentScene, this);
            LevelSongTimer = new Stopwatch();
        }

        private void UpdateRedTint()
        {
            int num = Mathf.Clamp(LevelScoreManager.ComboScore, 0, 15);
            CurrentRedTint = Convert.ToUInt32(255 << 16 | 255 - num * 4 << 8 | 255 - num * 4);
            _parentScene.SetCanvasClearColor(255, 0, 0, num);
        }

        private void ResetLevel()
        {
            _noteTime -= 0.2f;
            _marginOfError -= 0.02;
            LevelSongTimer.Restart();
            foreach (var lane in LevelLanes) lane.Reset();
            LevelSongManager.Reset();
            
        }

        //public void PlayHitNotes()
        //{
        //    var tempMidi = new MidiFile();
        //    //foreach (var chunk in LevelTrackChunks) tempMidi.Chunks.Add(chunk);
        //    //try { tempMidi.GetPlayback(DataStorage.OutputDevice).Start(); }
        //    //catch { }
        //    //LevelTrackChunks.Clear();
        //}

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
            LevelScoreManager.Destroy();
            LevelSongManager.Destroy();
            LevelSongTimer.Stop();
            foreach (var lane in LevelLanes) lane.Destroy();
        }

        public double GetAudioSourceTime() => (double)LevelSongTimer.ElapsedMilliseconds / 1000;
    }
}
