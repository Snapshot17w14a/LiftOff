using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class MyGame : Game {

	public MyGame() : base(1920, 1080, false)
	{
		_ = DataStorage.Instance;
        _ = Player.Instance;
    }

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}