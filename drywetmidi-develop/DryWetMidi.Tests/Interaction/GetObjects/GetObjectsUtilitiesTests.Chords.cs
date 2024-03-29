﻿using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using NUnit.Framework;

namespace Melanchall.DryWetMidi.Tests.Interaction
{
    [TestFixture]
    public sealed partial class GetObjectsUtilitiesTests
    {
        #region Test methods

        [Test]
        public void GetObjects_Chords_FromNotes_SingleNote() => GetObjects_Chords(
            inputObjects: new ITimedObject[]
            {
                new Note((SevenBitNumber)50),
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50)),
            });

        [Test]
        public void GetObjects_Chords_FromNotes_MultipleNotes_SameTime() => GetObjects_Chords(
            inputObjects: new ITimedObject[]
            {
                new Note((SevenBitNumber)50),
                new Note((SevenBitNumber)70),
                new Note((SevenBitNumber)90, 100, 0),
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50),
                    new Note((SevenBitNumber)70),
                    new Note((SevenBitNumber)90, 100, 0)),
            });

        [Test]
        public void GetObjects_Chords_FromNotes_MultipleNotes_ExceedingNotesTolerance([Values(0, 10)] long notesTolerance) => GetObjects_Chords(
            inputObjects: new ITimedObject[]
            {
                new Note((SevenBitNumber)50),
                new Note((SevenBitNumber)70),
                new Note((SevenBitNumber)90, 100, notesTolerance + 1),
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50),
                    new Note((SevenBitNumber)70)),
                new Chord(
                    new Note((SevenBitNumber)90, 100, notesTolerance + 1)),
            },
            notesTolerance: notesTolerance);

        [Test]
        public void GetObjects_Chords_FromNotes_MultipleNotes_DifferentChannels() => GetObjects_Chords(
            inputObjects: new ITimedObject[]
            {
                new Note((SevenBitNumber)50),
                new Note((SevenBitNumber)70),
                new Note((SevenBitNumber)90) { Channel = (FourBitNumber)1 },
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50),
                    new Note((SevenBitNumber)70)),
                new Chord(
                    new Note((SevenBitNumber)90) { Channel = (FourBitNumber)1 }),
            });

        [Test]
        public void GetObjects_Chords_FromNotesAndTimedEvents_SingleNote() => GetObjects_Chords(
            inputObjects: new ITimedObject[]
            {
                new Note((SevenBitNumber)50),
                new TimedEvent(new TextEvent("A"), 40),
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50)),
            });

        [Test]
        public void GetObjects_Chords_FromNotesAndTimedEvents_MultipleNotes_SameTime() => GetObjects_Chords(
            inputObjects: new ITimedObject[]
            {
                new TimedEvent(new TextEvent("A"), 10),
                new Note((SevenBitNumber)50),
                new TimedEvent(new TextEvent("B"), 0),
                new TimedEvent(new TextEvent("C"), 30),
                new Note((SevenBitNumber)70),
                new Note((SevenBitNumber)90, 100, 0),
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50),
                    new Note((SevenBitNumber)70),
                    new Note((SevenBitNumber)90, 100, 0)),
            });

        [Test]
        public void GetObjects_Chords_FromNotesAndTimedEvents_MultipleNotes_ExceedingNotesTolerance([Values(0, 10)] long notesTolerance) => GetObjects_Chords(
            inputObjects: new ITimedObject[]
            {
                new TimedEvent(new TextEvent("A"), 10),
                new Note((SevenBitNumber)50),
                new TimedEvent(new TextEvent("B"), 0),
                new TimedEvent(new TextEvent("C"), 30),
                new Note((SevenBitNumber)70),
                new Note((SevenBitNumber)90, 100, notesTolerance + 1),
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50),
                    new Note((SevenBitNumber)70)),
                new Chord(
                    new Note((SevenBitNumber)90, 100, notesTolerance + 1)),
            },
            notesTolerance: notesTolerance);

        [Test]
        public void GetObjects_Chords_FromNotesAndTimedEvents_MultipleNotes_DifferentChannels() => GetObjects_Chords(
            inputObjects: new ITimedObject[]
            {
                new TimedEvent(new TextEvent("A"), 10),
                new Note((SevenBitNumber)50),
                new Note((SevenBitNumber)70),
                new TimedEvent(new TextEvent("B"), 0),
                new Note((SevenBitNumber)90) { Channel = (FourBitNumber)1 },
                new TimedEvent(new TextEvent("C"), 30),
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50),
                    new Note((SevenBitNumber)70)),
                new Chord(
                    new Note((SevenBitNumber)90) { Channel = (FourBitNumber)1 }),
            });

        [Test]
        public void GetObjects_Chords_FromTimedEvents_SameTime() => GetObjects_Chords(
            inputObjects: new ITimedObject[]
            {
                new TimedEvent(new TextEvent("A"), 10),
                new TimedEvent(new NoteOnEvent((SevenBitNumber)50, Note.DefaultVelocity), 0),
                new TimedEvent(new NoteOffEvent((SevenBitNumber)50, SevenBitNumber.MinValue), 30),
                new TimedEvent(new NoteOnEvent((SevenBitNumber)70, Note.DefaultVelocity), 0),
                new TimedEvent(new NoteOffEvent((SevenBitNumber)70, SevenBitNumber.MinValue), 40),
                new TimedEvent(new TextEvent("B"), 0),
                new TimedEvent(new TextEvent("C"), 30),
                new TimedEvent(new NoteOnEvent((SevenBitNumber)90, Note.DefaultVelocity), 0),
                new TimedEvent(new NoteOffEvent((SevenBitNumber)90, SevenBitNumber.MinValue), 100),
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50, 30, 0),
                    new Note((SevenBitNumber)70, 40, 0),
                    new Note((SevenBitNumber)90, 100, 0)),
            });

        [Test]
        public void GetObjects_Chords_FromTimedEvents_ExceedingNotesTolerance([Values(0, 10)] long notesTolerance) => GetObjects_Chords(
            inputObjects: new ITimedObject[]
            {
                new TimedEvent(new TextEvent("A"), 10),
                new TimedEvent(new NoteOnEvent((SevenBitNumber)50, Note.DefaultVelocity), 0),
                new TimedEvent(new NoteOffEvent((SevenBitNumber)50, SevenBitNumber.MinValue), 30),
                new TimedEvent(new NoteOnEvent((SevenBitNumber)70, Note.DefaultVelocity), notesTolerance + 1),
                new TimedEvent(new NoteOffEvent((SevenBitNumber)70, SevenBitNumber.MinValue), notesTolerance + 1 + 40),
                new TimedEvent(new TextEvent("B"), 0),
                new TimedEvent(new TextEvent("C"), 30),
                new TimedEvent(new NoteOnEvent((SevenBitNumber)90, Note.DefaultVelocity), 0),
                new TimedEvent(new NoteOffEvent((SevenBitNumber)90, SevenBitNumber.MinValue), 100),
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50, 30, 0),
                    new Note((SevenBitNumber)90, 100, 0)),
                new Chord(
                    new Note((SevenBitNumber)70, 40, notesTolerance + 1)),
            },
            notesTolerance: notesTolerance);

        [Test]
        public void GetObjects_Chords_FromTimedEvents_DifferentChannels() => GetObjects_Chords(
            inputObjects: new ITimedObject[]
            {
                new TimedEvent(new TextEvent("A"), 10),
                new Note((SevenBitNumber)50),
                new Note((SevenBitNumber)70),
                new TimedEvent(new TextEvent("B"), 0),
                new TimedEvent(new NoteOnEvent((SevenBitNumber)90, Note.DefaultVelocity) { Channel = (FourBitNumber)1 }, 0),
                new TimedEvent(new NoteOffEvent((SevenBitNumber)90, SevenBitNumber.MinValue) { Channel = (FourBitNumber)1 }, 40),
                new TimedEvent(new TextEvent("C"), 30),
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50),
                    new Note((SevenBitNumber)70)),
                new Chord(
                    new Note((SevenBitNumber)90, 40, 0) { Channel = (FourBitNumber)1 }),
            });

        [Test]
        public void GetObjects_Chords_FromTimedEvents_SameTime_UncompletedNote() => GetObjects_Chords(
            inputObjects: new ITimedObject[]
            {
                new TimedEvent(new NoteOnEvent((SevenBitNumber)50, Note.DefaultVelocity), 0),
                new TimedEvent(new TextEvent("A"), 10),
                new TimedEvent(new NoteOffEvent((SevenBitNumber)50, SevenBitNumber.MinValue), 30),
                new TimedEvent(new NoteOnEvent((SevenBitNumber)70, Note.DefaultVelocity), 0),
                new TimedEvent(new TextEvent("B"), 0),
                new TimedEvent(new TextEvent("C"), 30),
                new TimedEvent(new NoteOffEvent((SevenBitNumber)70, SevenBitNumber.MinValue), 40),
                new TimedEvent(new NoteOnEvent((SevenBitNumber)90, Note.DefaultVelocity), 0),
                new TimedEvent(new TextEvent("D"), 30),
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50, 30),
                    new Note((SevenBitNumber)70, 40))
            });

        [Test]
        public void GetObjects_Chords_FromTimedEvents_SameTime_AllNotesUncompleted() => GetObjects_Chords(
            inputObjects: new ITimedObject[]
            {
                new TimedEvent(new NoteOnEvent((SevenBitNumber)50, Note.DefaultVelocity), 0),
                new TimedEvent(new TextEvent("A"), 10),
                new TimedEvent(new NoteOnEvent((SevenBitNumber)70, Note.DefaultVelocity), 0),
                new TimedEvent(new NoteOnEvent((SevenBitNumber)90, Note.DefaultVelocity), 0),
                new TimedEvent(new TextEvent("B"), 0),
                new TimedEvent(new TextEvent("C"), 30),
            },
            outputObjects: new ITimedObject[]
            {
            });

        [Test]
        public void EnumerateObjects_Chords_FromNotes_SingleNote() => EnumerateObjects_Chords(
            inputEvents: new MidiEvent[]
            {
                new NoteOnEvent((SevenBitNumber)50, Note.DefaultVelocity),
                new NoteOffEvent((SevenBitNumber)50, Note.DefaultOffVelocity),
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50)),
            });

        [Test]
        public void EnumerateObjects_Chords_FromNotesAndTimedEvents_SingleNote() => EnumerateObjects_Chords(
            inputEvents: new MidiEvent[]
            {
                new NoteOnEvent((SevenBitNumber)50, Note.DefaultVelocity),
                new NoteOffEvent((SevenBitNumber)50, Note.DefaultOffVelocity),
                new TextEvent("A") { DeltaTime = 40 },
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50)),
            });

        [Test]
        public void EnumerateObjects_Chords_FromTimedEvents_SameTime() => EnumerateObjects_Chords(
            inputEvents: new MidiEvent[]
            {
                new NoteOnEvent((SevenBitNumber)50, Note.DefaultVelocity),
                new NoteOnEvent((SevenBitNumber)70, Note.DefaultVelocity),
                new TextEvent("B"),
                new NoteOnEvent((SevenBitNumber)90, Note.DefaultVelocity),
                new TextEvent("A") { DeltaTime = 10 },
                new NoteOffEvent((SevenBitNumber)50, SevenBitNumber.MinValue) { DeltaTime = 20 },
                new TextEvent("C"),
                new NoteOffEvent((SevenBitNumber)70, SevenBitNumber.MinValue) { DeltaTime = 10 },
                new NoteOffEvent((SevenBitNumber)90, SevenBitNumber.MinValue) { DeltaTime = 60 },
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50, 30, 0),
                    new Note((SevenBitNumber)70, 40, 0),
                    new Note((SevenBitNumber)90, 100, 0)),
            });

        [Test]
        public void EnumerateObjects_Chords_FromTimedEvents_ExceedingNotesTolerance([Values(0, 8)] long notesTolerance) => EnumerateObjects_Chords(
            inputEvents: new MidiEvent[]
            {
                new NoteOnEvent((SevenBitNumber)50, Note.DefaultVelocity),
                new TextEvent("B"),
                new NoteOnEvent((SevenBitNumber)90, Note.DefaultVelocity),
                new NoteOnEvent((SevenBitNumber)70, Note.DefaultVelocity) { DeltaTime = notesTolerance + 1 },
                new TextEvent("A") { DeltaTime = 10 - (notesTolerance + 1) },
                new NoteOffEvent((SevenBitNumber)50, SevenBitNumber.MinValue) { DeltaTime = 20 },
                new TextEvent("C"),
                new NoteOffEvent((SevenBitNumber)70, SevenBitNumber.MinValue) { DeltaTime = notesTolerance + 1 + 40 - 30 },
                new NoteOffEvent((SevenBitNumber)90, SevenBitNumber.MinValue) { DeltaTime = 100 - (notesTolerance + 1 + 40) },
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50, 30, 0),
                    new Note((SevenBitNumber)90, 100, 0)),
                new Chord(
                    new Note((SevenBitNumber)70, 40, notesTolerance + 1)),
            },
            notesTolerance: notesTolerance);

        [Test]
        public void EnumerateObjects_Chords_FromTimedEvents_DifferentChannels() => EnumerateObjects_Chords(
            inputEvents: new MidiEvent[]
            {
                new NoteOnEvent((SevenBitNumber)50, Note.DefaultVelocity),
                new NoteOffEvent((SevenBitNumber)50, Note.DefaultOffVelocity),
                new NoteOnEvent((SevenBitNumber)70, Note.DefaultVelocity),
                new NoteOffEvent((SevenBitNumber)70, Note.DefaultOffVelocity),
                new TextEvent("B"),
                new NoteOnEvent((SevenBitNumber)90, Note.DefaultVelocity) { Channel = (FourBitNumber)1 },
                new TextEvent("A") { DeltaTime = 10 },
                new TextEvent("C") { DeltaTime = 20 },
                new NoteOffEvent((SevenBitNumber)90, SevenBitNumber.MinValue) { Channel = (FourBitNumber)1, DeltaTime = 10 },
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50),
                    new Note((SevenBitNumber)70)),
                new Chord(
                    new Note((SevenBitNumber)90, 40, 0) { Channel = (FourBitNumber)1 }),
            });

        [Test]
        public void EnumerateObjects_Chords_FromTimedEvents_SameTime_UncompletedNote() => EnumerateObjects_Chords(
            inputEvents: new MidiEvent[]
            {
                new NoteOnEvent((SevenBitNumber)50, Note.DefaultVelocity),
                new TextEvent("B"),
                new NoteOnEvent((SevenBitNumber)70, Note.DefaultVelocity),
                new NoteOnEvent((SevenBitNumber)90, Note.DefaultVelocity),
                new TextEvent("A") { DeltaTime = 10 },
                new NoteOffEvent((SevenBitNumber)50, SevenBitNumber.MinValue) { DeltaTime = 20 },
                new TextEvent("C"),
                new TextEvent("D"),
                new NoteOffEvent((SevenBitNumber)70, SevenBitNumber.MinValue) { DeltaTime = 10 },
            },
            outputObjects: new ITimedObject[]
            {
                new Chord(
                    new Note((SevenBitNumber)50, 30),
                    new Note((SevenBitNumber)70, 40))
            });

        [Test]
        public void EnumerateObjects_Chords_FromTimedEvents_SameTime_AllNotesUncompleted() => EnumerateObjects_Chords(
            inputEvents: new MidiEvent[]
            {
                new NoteOnEvent((SevenBitNumber)50, Note.DefaultVelocity),
                new TextEvent("B"),
                new NoteOnEvent((SevenBitNumber)70, Note.DefaultVelocity),
                new NoteOnEvent((SevenBitNumber)90, Note.DefaultVelocity),
                new TextEvent("A") { DeltaTime = 10 },
                new TextEvent("C") { DeltaTime = 20 },
            },
            outputObjects: new ITimedObject[]
            {
            });

        #endregion

        #region Private methods

        private void GetObjects_Chords(
            IEnumerable<ITimedObject> inputObjects,
            IEnumerable<ITimedObject> outputObjects,
            long notesTolerance = 0) => 
            GetObjects(
                inputObjects,
                outputObjects,
                ObjectType.Chord,
                new ObjectDetectionSettings
                {
                    ChordDetectionSettings = new ChordDetectionSettings
                    {
                        NotesTolerance = notesTolerance
                    }
                });

        private void EnumerateObjects_Chords(
            IEnumerable<MidiEvent> inputEvents,
            IEnumerable<ITimedObject> outputObjects,
            long notesTolerance = 0) =>
            EnumerateObjects(
                inputEvents,
                outputObjects,
                ObjectType.Chord,
                new ObjectDetectionSettings
                {
                    ChordDetectionSettings = new ChordDetectionSettings
                    {
                        NotesTolerance = notesTolerance
                    }
                });

        #endregion
    }
}
