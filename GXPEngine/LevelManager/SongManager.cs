namespace GXPEngine.LevelManager
{
    internal class SongManager
    {
        private static SongManager _instance;
        public static SongManager Instance
        {
            get
            {
                if (_instance == null) { _instance = new SongManager(); }
                return _instance;
            }
        }
    }
}
