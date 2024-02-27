using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.MusicTheory;
using System.Collections.Generic;
using System.Diagnostics;
using GXPEngine.UI;

namespace GXPEngine.LevelManager
{
    internal class Level
    {
        //Tweakable variables for each level c, d, e, g, a, b
        private readonly NoteName[] _laneNotes = { NoteName.C, NoteName.D, NoteName.E, NoteName.G, NoteName.A, NoteName.B };
        private Lane[] _lanes = new Lane[6];

        private readonly double _marginOfError = 0.2; //The margin of error in seconds
        private readonly int inputDelay = 0; //The delay in milliseconds between the input and the note
        private readonly float noteTime = 2; //The time in seconds it takes for the note to travel from the top to the bottom
        private MidiFile _midiFile; //The midi file that contains the song

        private readonly float _songDelay = DataStorage.SongDelay; //The delay in seconds before the song starts
        private ScoreManager _scoreManager;
        private SongManager _songManager;
        private Stopwatch _songTimer;
        private List<TrackChunk> _trackChunks;
        private Scene _parentScene;

        //Getter - DO NOT REMOVE
        public Lane[] LevelLanes => _lanes;
        public float SongDelay => _songDelay;
        public double MarginOfError => _marginOfError;
        public int InputDelay => inputDelay;
        public MidiFile LevelMidiFile => _midiFile;
        public float NoteTime => noteTime;
        public SongManager LevelSongManager => _songManager;
        public ScoreManager LevelScoreManager => _scoreManager;
        public Stopwatch LevelSongTimer => _songTimer;
        public List<TrackChunk> LevelTrackChunks
        {
            get => _trackChunks;
            set => _trackChunks = value;
        }

        public Level(string filename, Scene scene) 
        {
            _parentScene = scene;
            CreateLanes();
            CreateInstances(filename);
        }
        private void CreateLanes()
        {
            for (int i = 0; i < _lanes.Length; i++)
            {
                var pos = DataStorage.TargetVectors[i];
                _lanes[i] = new Lane(i, _laneNotes[i], _parentScene, this) { x = pos.x, y = pos.y };
            }
        }
        private void CreateInstances(string filename)
        {
            _midiFile = MidiFile.Read(filename);
            _trackChunks = new List<TrackChunk>();
            _scoreManager = new ScoreManager(_parentScene);
            _songManager = new SongManager(_midiFile, _parentScene, this);
            _songTimer = new Stopwatch();
        }
        public void PlayHitNotes()
        {
            var tempMidi = new MidiFile();
            foreach (var chunk in _trackChunks) tempMidi.Chunks.Add(chunk);
            tempMidi.GetPlayback(DataStorage.OutputDevice).Start();
            _trackChunks.Clear();
        }
        public double GetAudioSourceTime() => (double)_songTimer.ElapsedMilliseconds / 1000;
    }
}
