using QuickFont;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    class DrawnButton
    {
        public delegate void EventAction();

        private Sprite Sprite { get; set; }
        public string Text { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        private float Width { get; set; }
        private float Height { get; set; }
        private EventAction OnClickAction { get; set; }
        public float r { get; set; }
        public float g { get; set; }
        public float b { get; set; }
        public float a { get; set; }

        public bool drawed { get; set; }

        public DrawnButton(string text, float x, float y, float width, float height, EventAction onClickAction, float r, float g, float b, bool drawed = true)
        {
            if (height < 25)
            {
                height = 25;
            }
            Text = text;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            OnClickAction = onClickAction;
            Sprite = new Sprite((int)width, (int)height, 0, Textures.Get(Textures.pixel));
            this.r = r;
            this.g = g;
            this.b = b;
            a = 1;
            this.drawed = drawed;
        }

        public DrawnButton(string text, float x, float y, float width, float height, EventAction onClickAction, Texture tex = null, float r = 1, float g = 1, float b = 1)
        {
            if (height < 25)
            {
                height = 25;
            }
            Text = text;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            OnClickAction = onClickAction;
            if (tex == null)
            {
                Sprite = new Sprite((int)width, (int)height, 0, Textures.Get(Textures.pixel));
            }
            else
            {
                Sprite = new Sprite((int)width, (int)height, 0, tex);
            }
            this.r = r;
            this.g = g;
            this.b = b;
            a = 1;
            drawed = true;
        }

        public bool IsInButton(float x, float y)
        {
            return x >= X && x <= X + Width && y >= Y && y <= Y + Height;
        }

        public void OnClick()
        {
            OnClickAction();
        }

        public void SetSprite(Texture tex)
        {
            this.Sprite = new Sprite((int)Width, (int)Height, 0, tex);
        }

        public void Draw()
        {
            if (!drawed) return;
            Sprite.DrawLate(X, Y, false, 0, r, g, b, a);
            Window.window.DrawTextCentered(Text, (int)(X + (Width / 2)), (int)(Y + (Height / 2) - 12), true, Globals.buttonFont);
        }
    }
}
