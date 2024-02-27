using GXPEngine;

public class MyGame : Game 
{
	public MyGame() : base(1920, 1080, false, false, -1, -1, true)
	{
		_ = DataStorage.Instance;
	}

	static void Main() => new MyGame().Start();
}