using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.UI
{
    class OneMinuteUI : UI
    {

        public List<ListEntry> scrolledButtons = new List<ListEntry>();
        public List<DrawnButton> scrollListButtons = new List<DrawnButton>();
        int scroll = 0;

        public OneMinuteUI()
        {
            // Tabs
            // - Leaders
            // - Scientists
            // - Supplies

            // Finish Button -> events if time left
            buttons.Add(new DrawnButton("Leaders", 1920 / 2 - 600, 10, 400, 50, () => { Globals.leaderUI.reSelect(); }, 0.5f, 0.5f, 0.5f));

            buttons.Add(new DrawnButton("Scientists", 1920 / 2 - 200, 10, 400, 50, () => { Globals.scientistUI.reSelect(); }, 0.5f, 0.5f, 0.5f));

            buttons.Add(new DrawnButton("Supplies", 1920 / 2 + 200, 10, 400, 50, () => { Globals.suppliesUI.reSelect(); }, 0.5f, 0.5f, 0.5f));

            buttons.Add(new DrawnButton("Embark!", 1920 - 200, 1080 - 100, 200, 100, () => { Globals.currentState = GameState.MAPVIEW; Globals.activeButtons = new List<DrawnButton>(); }, 0.5f, 0.5f, 0.5f));

            for(int i = 0; i < 35; i++)
            {
                int k = i;
                scrollListButtons.Add(new DrawnButton("", 100, 60 + 25 * i, 300, 25, () => { SelectFromList(k + scroll); }, 0, 0, 0, false));
                buttons.Add(scrollListButtons[scrollListButtons.Count-1]);
            }

        }

        public override void Draw()
        {
            Window.window.DrawText("5", 0, 0);
            for(int i = 0; i < 35; i++)
            {
                if (scroll + i >= scrolledButtons.Count) break;
                scrolledButtons[scroll+i].Draw(i);
            }
        }

        public void Scroll(int val)
        {
            scroll += val;
            if(scroll > scrolledButtons.Count-35)
            {
                scroll = scrolledButtons.Count - 35;
            }
            if(scroll < 0)
            {
                scroll = 0;
            }
        }

        public virtual void SelectFromList(int i)
        {

        }


    }

    abstract class ListEntry
    {

        public abstract void Draw(int y);

    }

}
