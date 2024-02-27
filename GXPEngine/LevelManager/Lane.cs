using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using System.Collections.Generic;
using GXPEngine.UI;
using System;

namespace GXPEngine.LevelManager
{
    internal class Lane : GameObject
    {
        private readonly NoteName _noteRestiction;
        private TrackChunk _hitChunk;
        private Scene _parentScene;
        private Level _parentLevel;
        private int _inputKey;
        private int _index;
        private List<NoteObject> _noteObjects = new List<NoteObject>();
        private List<double> _timeStamps = new List<double>();

        int spawnIndex = 0;

        public Lane(int index, NoteName laneNote, Scene scene, Level level)
        {
            _parentLevel = level;
            _parentScene = scene;
            _inputKey = DataStorage.InputKeys[index];
            _noteRestiction = laneNote;
            _index = index;
            CreateHitNoteTrack();
            scene.SceneUpdate += Update;
        }

        private void CreateHitNoteTrack()
        {
            SevenBitNumber noteNumber = Melanchall.DryWetMidi.MusicTheory.Note.Get(_noteRestiction, 4).NoteNumber;
            var noteOnEvent = new NoteOnEvent(noteNumber, (SevenBitNumber)100);
            var noteOffEvent = new NoteOffEvent(noteNumber, (SevenBitNumber)100) { DeltaTime = 2000 };
            _hitChunk = new TrackChunk(noteOnEvent, noteOffEvent);
        }

        public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
        {
            foreach(var note in array)
            {
                if(note.NoteName == _noteRestiction)
                {
                    var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, _parentLevel.LevelMidiFile.GetTempoMap());
                    _timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                }
            }
        }

        private void Update()
        {
            if (spawnIndex < _timeStamps.Count)
            {
                if (_parentLevel.GetAudioSourceTime() >= _timeStamps[spawnIndex] - _parentLevel.NoteTime)
                {
                    _noteObjects.Add(new NoteObject(_timeStamps[spawnIndex], _index, _parentScene, _parentLevel) { x = -50, y = -50 });
                    spawnIndex++;
                }
            }
            if(Input.GetKeyDown(_inputKey) && _noteObjects.Count != 0)
            {
                double audioTime = _parentLevel.GetAudioSourceTime() - (_parentLevel.InputDelay / 1000f);
                if (Math.Abs(audioTime - _noteObjects[0].AssignedTime) <= _parentLevel.MarginOfError)
                {
                    _parentLevel.LevelTrackChunks.Add(_hitChunk);
                    _parentLevel.LevelScoreManager.Hit();
                }
                else _parentLevel.LevelScoreManager.Miss();
                _noteObjects[0].LateDestroy();
            }
        }

        public void RemoveNoteFromList(NoteObject obj) { _noteObjects.Remove(obj); }
    }
}
