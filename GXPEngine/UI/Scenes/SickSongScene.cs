namespace GXPEngine.UI.Scenes
{
    internal class SickSongScene : Scene
    {
        public SickSongScene() : base("SickScene") { Initialize(); }

        private void Initialize()
        {
            SetBackground("background.png");
            Canvas.TextFont(Utils.LoadFont("Foont.ttf", 48));
            SetAlignment(Alignment.CENTER, Alignment.CENTER);
        }

        public override void OnUnload()
        {
            base.OnUnload();
            ClearOverlay();
            DestroyLevel();
        }

        public override void OnLoad() 
        { 
            base.OnLoad();
            if (SceneLevel == null) CreateLevel("untitled.mid"); 
        }
    }
}