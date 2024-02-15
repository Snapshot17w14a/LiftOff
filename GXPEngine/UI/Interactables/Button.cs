using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GXPEngine.Scene;

namespace GXPEngine.UI.Interactables
{
    internal class Button : Sprite
    {
        public Button(string filename, int x, int y, Scene.Alignment horizontal, Scene.Alignment vertical) : base(filename, false, false)
        {
            SetXY(x, y);
        }

        

        public void OnClick()
        {
            Console.WriteLine("Button clicked");
        }
    }
}
