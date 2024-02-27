namespace GXPEngine.UI.Scenes
{
    internal class InitialScene : Scene
    {
        public InitialScene(string sceneName) : base(sceneName) { Initialize(); }

        private void Initialize()
        {
            SetBackground("background.png");
            //SetAlignment(Scene.Alignment.CENTER, Scene.Alignment.CENTER); //Set the alignment of the scene
            //CreateButton("square.png", 400, 300); //Create a button with the given filename and position
            //CreateButton("square.png", 400, 400, new Scene()); //Create a button with the given filename and position
            SetAlignment(Scene.Alignment.CENTER, Scene.Alignment.CENTER, true); //Set the alignment of the scene
            Canvas.TextFont(Utils.LoadFont("Foont.ttf", 48));
            SetAlignment(Scene.Alignment.CENTER, Scene.Alignment.CENTER); //Set the alignment of the scene
            CreatePlayer("circle.png", Game.main.width / 2, Game.main.height / 2); //Create a player with the given filename and position
            CreateLevel("test.mid", this); //Create a level with the given filename
            CreateButton("square.png", 300, 300, "TestScene"); //Create a button with the given filename and position
        }
    }
}
