using System;
using System.Drawing;

namespace GXPEngine.UI
{
    internal class TextObject : EasyDraw
    {
        private readonly string text;

        public TextObject(string text, CenterMode horizontal, CenterMode vertical, int width, int height, Font font, Color col) : base(width, height)
        {
            this.text = text;
            SetOrigin(width / 2, height / 2);
            TextFont(font);
            Fill(col);
            TextAlign(horizontal, vertical);
        }

        public void Draw()
        {
            Clear(Color.Transparent);
            Text(text, width / 2, height / 2);
        }            
    }
}
