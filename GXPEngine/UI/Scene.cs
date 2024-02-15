using System.Collections.Generic;

namespace GXPEngine
{
    internal class Scene
    {
        private Alignment _verticalAlignment;
        private Alignment _horizontalAlignment;
        private List<GameObject> _buttons = new List<GameObject>();
        public List<GameObject> Buttons => _buttons;

        public enum Alignment
        {
            Min,
            Center,
            Max
        }
        public Scene() { }

        /// <summary>
        /// Creates a button with the given filename and position
        /// </summary>
        /// <param name="filename">
        /// The filename of the button texture in the bin/debug folder
        /// </param>
        /// <param name="x">
        /// The x position of the button
        /// </param>
        /// <param name="y">
        /// The y position of the button
        /// </param>
        public void CreateButton(string filename, int x, int y)
        {
            Sprite sprite = new Sprite(filename);
            SetOrigin(sprite);
            _buttons.Add(sprite);
        }

        private void SetOrigin(Sprite obj)
        {
            int anchorx = 0; 
            int anchory = 0;
            switch (_horizontalAlignment)
            {
                case Alignment.Min:
                    anchorx = 0;
                    break;
                case Alignment.Center:
                    anchorx = obj.width / 2;
                    break;
                case Alignment.Max:
                    anchorx = obj.width;
                    break;
            }
            switch (_verticalAlignment)
            {
                case Alignment.Min:
                    anchory = 0;
                    break;
                case Alignment.Center:
                    anchory = obj.height / 2;
                    break;
                case Alignment.Max:
                    anchorx = obj.height;
                    break;
            }
            obj.SetOrigin(anchorx, anchory);
        }

        public void SetAlignment(Alignment horizontal, Alignment vertical)
        {
            _horizontalAlignment = horizontal;
            _verticalAlignment = vertical;
        }
    }
}
