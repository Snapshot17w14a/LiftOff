using System;

namespace GXPEngine.UI.Scenes
{
    internal class InitialScene : Scene
    {
        public InitialScene() : base("InitialScene") { Initialize(); }

        private void Initialize()
        {
            Canvas.Clear(255);
            Canvas.TextSize(96);
            Canvas.Fill(255, 0, 128);
            SetAlignment(Alignment.CENTER, Alignment.CENTER, true); //Set the alignment of the scene
            Canvas.TextFont(Utils.LoadFont("Foont.ttf", 48));
            CreateButton("square.png", 400, 300, "SickScene"); //Create a button with the given filename and position
            SceneUpdate += Draw;
        }

        protected override void Draw()
        {
            Canvas.Text("Sicc Ducc Gameeeeee", Game.main.width / 2, 96);
            Canvas.Text("Press any key to play", Game.main.width / 2, 700);
        }
    }
}
