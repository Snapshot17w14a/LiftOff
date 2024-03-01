using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System.Collections.Generic;
using GXPEngine.UI;
using System;

namespace GXPEngine.LevelManager
{
    internal class Lane : GameObject
    {
        private readonly List<NoteObject> _noteObjects = new List<NoteObject>();
        private readonly List<double> _timeStamps = new List<double>();
        private readonly NoteName _noteRestiction;
        private readonly int _inputKey;
        private readonly int _index;
        private Scene _parentScene;
        private Level _parentLevel;

        int spawnIndex = 0;

        public Lane(int index, NoteName laneNote, Scene scene, Level level)
        {
            _inputKey = DataStorage.InputKeys[index];
            _noteRestiction = laneNote;
            _parentLevel = level;
            _parentScene = scene;
            _index = index;
            //CreateHitNoteTrack();
            scene.SceneUpdate += Update;
        }

        public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
        {
            foreach (var note in array)
            {
                if (note.NoteName == _noteRestiction)
                {
                    var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, _parentLevel.LevelMidiFile.GetTempoMap());
                    _timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                    _parentLevel.NoteCount++;
                }
            }
        }

        private void Update()
        {
            if (spawnIndex < _timeStamps.Count)
            {
                if (_parentLevel.GetAudioSourceTime() >= _timeStamps[spawnIndex] - _parentLevel.NoteTime)
                {
                    _noteObjects.Add(new NoteObject(_timeStamps[spawnIndex], _index, _parentLevel) { x = -1010, y = -590 });
                    spawnIndex++;
                }
            }
            if (Input.GetKeyDown(_inputKey) && _noteObjects.Count != 0)
            {
                if (_noteObjects[0].IsCorrect)
                {
                    _parentLevel.LevelScoreManager.Hit();
                    _parentLevel.Shake();
                }
                else _parentLevel.LevelScoreManager.Miss();
                _noteObjects[0].LateDestroy();
            }
        }

        public void Reset()
        {
            _timeStamps.Clear();
            foreach (var note in _noteObjects) note.LateDestroy();
            _noteObjects.Clear();
            spawnIndex = 0;
        }

        public void RemoveNoteFromList(NoteObject obj) { _noteObjects.Remove(obj); }

        protected override void OnDestroy()
        {
            for (int i = _noteObjects.Count - 1; i >= 0; i--) { _noteObjects[i].PlayParticle = false; _noteObjects[i].Destroy(); }
            _parentScene.SceneUpdate -= Update;
            _timeStamps.Clear();
            _parentLevel = null;
            _parentScene = null;
        }
    }
}
