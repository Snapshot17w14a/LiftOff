using GXPEngine.Core;

namespace GXPEngine.LevelManager
{
    internal class Pie : Sprite
    {
        private Level _parentLevel;

        public Pie(string filename, Level parentLevel) : base(filename)
        {
            SetOrigin(width / 2, height / 2);
            _parentLevel = parentLevel;
            _parentLevel.AddChild(this);
        }

        public void SetPieSprite(int lives) => _texture = Texture2D.GetInstance($"Pies/pie{lives}.png");
    }
}
