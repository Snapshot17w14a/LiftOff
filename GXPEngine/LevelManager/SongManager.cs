using System;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace GXPEngine.LevelManager
{
    internal class SongManager
    {
        private readonly MidiFile midiFile;
        private SoundChannel _soundChannel;
        private float _startTimer;
        private bool _startDelay;
        public SongManager()
        {
            midiFile = Level.LevelMidiFile;
            GetData();
            DataStorage.Instance.MainGame.OnBeforeStep += Update;
        }

        private void GetData()
        {
            Note[] array = midiFile.GetNotes().ToArray();
            foreach (var lane in Level.LevelLanes) lane.SetTimeStamps(array);
            _startDelay = true;
        }

        private void Update()
        {
            Console.Clear();
            if (_startDelay)
            {
                _startTimer += Time.deltaTime;
                if (_startTimer >= Level.SongDelay * 1000)
                {
                    _startDelay = false;
                    StartSong();
                }
            }
            if (Level.LevelSongTimer != null && Level.LevelSongTimer.IsRunning && !_soundChannel.IsPlaying) Level.LevelSongTimer.Stop();
        }

        private void StartSong() 
        { 
            Level.LevelSongTimer.Start();
            _soundChannel = new Sound("test.mp3").Play(false, 0, 0, 0);
        }
    }
}
