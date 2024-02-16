using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using GXPEngine.Core;
using System.Collections.Generic;
using System;

namespace GXPEngine.LevelManager
{
    internal class Lane : GameObject
    {
        private readonly NoteName _noteRestiction;
        private TrackChunk _hitChunk;
        private int _inputKey;
        private int _index;
        private List<NoteObject> _notesObjects = new List<NoteObject>();
        private List<double> _timeStamps = new List<double>();

        int spawnIndex = 0;

        public Lane(int index, NoteName laneNote)
        {
            Vector2 pos = DataStorage.Instance.TargetVectors[index];
            SetXY(pos.x, pos.y);
            _inputKey = DataStorage.Instance.InputKeys[index];
            _noteRestiction = laneNote;
            _index = index;
            CreateHitNoteTrack();
            DataStorage.Instance.MainGame.OnAfterStep += Update;
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
                    var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, Level.LevelMidiFile.GetTempoMap());
                    _timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                }
            }
        }

        private void Update()
        {
            if (spawnIndex < _timeStamps.Count)
            {
                if (Level.GetAudioSourceTime() >= _timeStamps[spawnIndex] - Level.NoteTime)
                {
                    _notesObjects.Add(new NoteObject(_timeStamps[spawnIndex], _index));
                    spawnIndex++;
                }
            }
            if(Input.GetKeyDown(_inputKey) && _notesObjects.Count != 0)
            {
                double audioTime = Level.GetAudioSourceTime() - (Level.InputDelay / 1000f);
                if (Math.Abs(audioTime - _notesObjects[0].AssignedTime) <= Level.MarginOfError)
                {
                    Level.LevelTrackChunks.Add(_hitChunk);
                    Level.LevelScoreManager.Hit();
                }
                else Level.LevelScoreManager.Miss();
                _notesObjects[0].LateDestroy();
                _notesObjects.RemoveAt(0);
            }
        }

        public void RemoveNoteFromList(NoteObject obj) => Console.WriteLine(_notesObjects.Remove(obj));
    }
}
