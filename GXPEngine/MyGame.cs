using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class MyGame : Game {
	MidiFile midiFile = MidiFile.Read("list.mid");
	Note[] allNotes;
	Player player;
	public MyGame() : base(1920, 1080, false)
	{
		_ = DataStorage.Instance;
        Player player = new Player();
        //var outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth");
        //var allNotes = midiFile.GetNotes();
        //foreach(var note in allNotes) Console.WriteLine(note);
        //midiFile.Play(outputDevice);
    }

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}