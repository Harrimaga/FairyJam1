using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Events
{
    public class Event : Popup
    {

        private Sprite optionBar;

        public Event() : base("Test", "Event")
        {
            optionBar = new Sprite(720, 60, 0, Textures.Get(1));
        }

        public override void Draw()
        {
            base.Draw();
            optionBar.DrawLate(1920/2-360, 1080/2 + 30, false);
            optionBar.DrawLate(1920/2-360, 1080/2 + 100, false);
            optionBar.DrawLate(1920/2-360, 1080/2 + 170, false);
            // Draw Options
        }

        public virtual void OptionA() 
        {
            
        }

        public virtual void OptionB() 
        {
            
        }


        public virtual void OptionC() 
        {
            
        }



    }
}
