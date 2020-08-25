using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Events
{
    public class Popup
    {

        private Sprite Background;
        protected string title, description;

        public Popup(string title, string description ) 
        {
            Background = new Sprite(1920 / 2, 1080 / 2, 0, Textures.Get(Textures.eventBackground));
            this.title = title;
            this.description = description;
        }
        
        public virtual void Activate()
        {

        }

        public virtual void Draw()
        {
            // Draw Popup layout
            Background.DrawLate(1920 / 4, 1080 / 4, false);
            Window.window.DrawTextCentered(title, (int)((1920 / 2)), (int)((1080 / 4) + 15), true);
            Window.window.DrawText(description, (int)((1920 / 4) + 5), (int)((1080 / 4) + 125), true, Globals.buttonFont);
            // Override for specific events/things
        }
    }
}
