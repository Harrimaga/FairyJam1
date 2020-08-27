using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    class psUI : UI
    {
        private Sprite backGround = new Sprite(400, 601, 0, Textures.Get(1));

        public psUI()
        {

        }

        public override void Draw()
        {
            backGround.DrawLate(1920 / 2 - 200, 1080 / 2 - 300, false, 0, 1, 1, 1, 0.8f);
        }

        public override void MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            if(!Globals.checkCol(mx, my, 0, 0, 1920 / 2 - 200, 1080 / 2 - 300, 400, 600)) 
            {
                Globals.currentUI = null;
                Globals.activeButtons = new List<DrawnButton>();
            }
        }
    }
}
