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
        private Level _parentLevel;
        private Scene _parentScene;
        private float _startTimer;
        private bool _startDelay;

        public SongManager(MidiFile midiFile, Scene scene, Level level)
        {
            _parentLevel = level;
            _parentScene = scene;
            _midiFile = midiFile;
            GetData();
            scene.SceneUpdate += Update;
        }

        private void GetData()
        {
            Note[] array = _midiFile.GetNotes().ToArray();
            foreach (var lane in _parentLevel.LevelLanes) lane.SetTimeStamps(array);
            _startDelay = true;
        }

        private void Update()
        {
            if (_startDelay)
            {
                _startTimer += Time.deltaTime;
                if (_startTimer >= _parentLevel.SongDelay * 1000)
                {
                    _startDelay = false;
                    StartSong();
                }
            }
            if (_parentLevel.LevelSongTimer != null && _parentLevel.LevelSongTimer.IsRunning && !_soundChannel.IsPlaying) _parentLevel.LevelSongTimer.Stop();
        }

        private void StartSong() 
        { 
            _parentLevel.LevelSongTimer.Start();
            _soundChannel = new Sound("test.mp3").Play(false, 0, 0, 0);
        }

        public void Reset()
        {
            GetData();
        }

        public void Destroy()
        {
            _parentScene.SceneUpdate -= Update;
            _parentLevel = null;
            _parentScene = null;
        }
    }
}
