using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Core;
using System.Linq;
using System;
using GXPEngine.UI;

namespace GXPEngine.LevelManager
{
    internal class SongManager
    {
        private readonly MidiFile _midiFile;
        private SoundChannel _soundChannel;
        private Level _level;
        private float _startTimer;
        private bool _startDelay;

        public SongManager(MidiFile midiFile, Level level)
        {
            _level = level;
            _midiFile = midiFile;
            GetData();
            Game.main.OnBeforeStep += Update;
        }

        private void GetData()
        {
            Note[] array = _midiFile.GetNotes().ToArray();
            foreach (var lane in _level.LevelLanes) lane.SetTimeStamps(array);
            _startDelay = true;
        }

        private void Update()
        {
            Console.Clear();
            if (_startDelay)
            {
                _startTimer += Time.deltaTime;
                if (_startTimer >= _level.SongDelay * 1000)
                {
                    _startDelay = false;
                    StartSong();
                }
            }
            if (_level.LevelSongTimer != null && _level.LevelSongTimer.IsRunning && !_soundChannel.IsPlaying) _level.LevelSongTimer.Stop();
        }

        private void StartSong() 
        { 
            _level.LevelSongTimer.Start();
            _soundChannel = new Sound("test.mp3").Play(false, 0, 0, 0);
        }
    }
}
