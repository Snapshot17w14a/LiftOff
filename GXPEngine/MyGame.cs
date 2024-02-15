using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;

public class MyGame : Game {
	MidiFile midiFile = MidiFile.Read("list.mid");
	Note[] allNotes;
	Player player;
	public MyGame() : base(1920, 1080, false)
	{
		Sprite background = new Sprite("background.png", false, false);
		background.SetOrigin(background.width / 2, background.height / 2);
		background.SetXY(width / 2, height / 2);
		background.width = width;
		background.height = height;
		AddChild(background);
		Player player = new Player();
		var outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth");
        var allNotes = midiFile.GetNotes();
		foreach(var note in allNotes) Console.WriteLine(note);
		midiFile.Play(outputDevice);
	}

	void Update()                               // Update is called every frame
	{
        //if(Input.GetMouseButtonDown(0)) Console.WriteLine($"MouseX: {Input.mouseX}, MouseY: {Input.mouseY}");
    }

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}