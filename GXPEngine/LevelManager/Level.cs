using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using System.Collections.Generic;
using System.Diagnostics;

namespace GXPEngine.LevelManager
{
    internal class Level
    {
        //Tweakable variables for each level c, d, e, g, a, b
        private static readonly Lane[] _lanes = {
            new Lane(0, Melanchall.DryWetMidi.MusicTheory.NoteName.C),
            new Lane(1, Melanchall.DryWetMidi.MusicTheory.NoteName.D),
            new Lane(3, Melanchall.DryWetMidi.MusicTheory.NoteName.E),
            new Lane(2, Melanchall.DryWetMidi.MusicTheory.NoteName.G),
            new Lane(4, Melanchall.DryWetMidi.MusicTheory.NoteName.A),
            new Lane(5, Melanchall.DryWetMidi.MusicTheory.NoteName.B) 
        };

        private static readonly double _marginOfError = 0.2; //The margin of error in seconds
        private static readonly int inputDelay = 0; //The delay in milliseconds between the input and the note
        private static readonly float noteTime = 2; //The time in seconds it takes for the note to travel from the top to the bottom
        private static readonly MidiFile _midiFile = MidiFile.Read("test.mid"); //The midi file that contains the song

        private static readonly float _songDelay = DataStorage.Instance.SongDelay; //The delay in seconds before the song starts
        private static readonly ScoreManager _scoreManager = new ScoreManager();
        private static readonly SongManager _songManager = new SongManager();
        private static readonly Stopwatch _songTimer = new Stopwatch();
        private static List<TrackChunk> _trackChunks = new List<TrackChunk>();
        private static Level _instance;

        //Getter - DO NOT REMOVE
        public static Lane[] LevelLanes => _lanes;
        public static float SongDelay => _songDelay;
        public static double MarginOfError => _marginOfError;
        public static int InputDelay => inputDelay;
        public static MidiFile LevelMidiFile => _midiFile;
        public static float NoteTime => noteTime;
        public static SongManager LevelSongManager => _songManager;
        public static ScoreManager LevelScoreManager => _scoreManager;
        public static Stopwatch LevelSongTimer => _songTimer;
        public static List<TrackChunk> LevelTrackChunks
        {
            get => _trackChunks;
            set => _trackChunks = value;
        }

        public static Level Instance
        {
            get
            {
                if (_instance == null) _instance = new Level();
                return _instance;
            }
        }
        private Level() { DataStorage.Instance.MainGame.OnAfterStep += PlayHitNotes; }
        public static void PlayHitNotes()
        {
            var tempMidi = new MidiFile();
            foreach (var chunk in _trackChunks) tempMidi.Chunks.Add(chunk);
            tempMidi.GetPlayback(DataStorage.Instance.OutputDevice).Start();
            _trackChunks.Clear();
        }
        public static double GetAudioSourceTime() => (double)_songTimer.ElapsedMilliseconds / 1000;
    }
}
