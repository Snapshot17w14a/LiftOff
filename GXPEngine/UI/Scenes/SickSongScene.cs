namespace GXPEngine.UI.Scenes
{
    internal class SickSongScene : Scene
    {
        public SickSongScene() : base("SickScene") { Initialize(); }

        private void Initialize()
        {
            SetBackground("background.png");
            SetAlignment(Alignment.CENTER, Alignment.CENTER, true); //Set the alignment of the objects in the scene's canvas
            Canvas.TextFont(Utils.LoadFont("Foont.ttf", 48));
            SetAlignment(Alignment.CENTER, Alignment.CENTER); //Set the alignment of the scene
        }

        public override void OnUnload() => DestroyLevel();

        public override void OnLoad() { if (SceneLevel == null) CreateLevel("test.mid"); }
    }
}
