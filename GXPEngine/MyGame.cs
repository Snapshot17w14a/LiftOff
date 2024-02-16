using System;
using GXPEngine;
using GXPEngine.LevelManager;

public class MyGame : Game 
{
	public MyGame() : base(1920, 1080, false)
	{
		_ = DataStorage.Instance;
		_ = Player.Instance;
		_ = Level.Instance;
    }

	static void Main() => new MyGame().Start();
}