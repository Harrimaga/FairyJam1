using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    class UI
    {
        public List<DrawnButton> buttons;

        public UI() {
            buttons = new List<DrawnButton>();
            Globals.activeButtons = buttons;
            Globals.currentUI = this;
        }

        public void reSelect()
        {
            Globals.activeButtons = buttons;
            Globals.currentUI = this;
        }
        
        public virtual void Draw()
        {

        }

        public virtual bool MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            return false;
        }

    }
}
