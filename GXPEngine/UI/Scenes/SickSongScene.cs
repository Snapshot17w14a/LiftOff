using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.UI.Scenes
{
    internal class SickSongScene : Scene
    {
        public SickSongScene() : base("SickScene") { Initialize(); }

        private void Initialize()
        {
            SetBackground("background.png");
            //SetAlignment(Scene.Alignment.CENTER, Scene.Alignment.CENTER); //Set the alignment of the scene
            //CreateButton("square.png", 400, 300); //Create a button with the given filename and position
            //CreateButton("square.png", 400, 400, new Scene()); //Create a button with the given filename and position
            SetAlignment(Alignment.CENTER, Alignment.CENTER, true); //Set the alignment of the objects in the scene's canvas
            Canvas.TextFont(Utils.LoadFont("Foont.ttf", 48));
            SetAlignment(Alignment.CENTER, Alignment.CENTER); //Set the alignment of the scene
            CreatePlayer("triangle.png", Game.main.width / 2, Game.main.height / 2); //Create a player with the given filename and position
            CreateButton("square.png", 300, 300, "InitialScene"); //Create a button with the given filename and position
        }

        public override void OnUnload()
        {
            SceneLevel.LateDestroy();
            SceneLevel = null;
        }

        public override void OnLoad()
        {
            if(SceneLevel == null) CreateLevel("test.mid");
        }
    }
}
