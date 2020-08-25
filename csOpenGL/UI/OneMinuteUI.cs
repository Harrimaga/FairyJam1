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
            buttons.Add(new DrawnButton("Leaders", 1920 / 2 - 700, 10, 400, 50, () => { Globals.leaderUI.reSelect(); }, 0.5f, 0.5f, 0.5f));

            buttons.Add(new DrawnButton("Scientists", 1920 / 2 - 300, 10, 400, 50, () => { Globals.scientistUI.reSelect(); }, 0.5f, 0.5f, 0.5f));

            buttons.Add(new DrawnButton("Supplies", 1920 / 2 + 100, 10, 400, 50, () => { Globals.suppliesUI.reSelect(); }, 0.5f, 0.5f, 0.5f));

            buttons.Add(new DrawnButton("Embark!", 1920 - 200, 1080 - 100, 200, 100, () => { Globals.currentState = GameState.MAPVIEW; Globals.activeButtons = new List<DrawnButton>(); }, 0.5f, 0.5f, 0.5f));

            for(int i = 0; i < 35; i++)
            {
                int k = i;
                scrollListButtons.Add(new DrawnButton("", 100, 60 + 25 * i, 300, 25, () => { SelectFromList(k + scroll); }, Textures.Get(Textures.personSelectionBox), 1, 1, 1));
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

            Window.window.DrawText("Leaders:", 1500, 25, Globals.buttonFont);
            if (Globals.PlayerNation.leaders.Count == 0)
            {
                Window.window.DrawText("None", 1525, 60, Globals.buttonFont);
            }
            for(int i = 0; i < Globals.PlayerNation.leaders.Count; i++)
            {
                Window.window.DrawText(Globals.PlayerNation.leaders[i].ToStringShort(), 1525, 60 + i * 25, Globals.buttonFont);
                int x = 0;
                foreach(Trait t in Globals.PlayerNation.leaders[i].Traits)
                {
                    t.sprite.Draw(1798 + 32*x, 60 + 25 * i);
                    x++;
                }
            }

            int offset = Globals.PlayerNation.leaders.Count;
            if(offset == 0) {
                offset = 1;
            }

            Window.window.DrawText("Scientists:", 1500, 85 + offset * 25, Globals.buttonFont);
            if (Globals.PlayerNation.scientists.Count == 0)
            {
                Window.window.DrawText("None", 1525, 120 + offset * 25, Globals.buttonFont);
            }
            for (int i = 0; i < Globals.PlayerNation.scientists.Count; i++)
            {
                Window.window.DrawText(Globals.PlayerNation.scientists[i].ToStringShort(), 1525, 120 + (i + offset) * 25, Globals.buttonFont);
                int x = 0;
                foreach (Trait t in Globals.PlayerNation.scientists[i].Traits)
                {
                    t.sprite.Draw(1798 + 32 * x, 120 + 25 * (i + offset));
                    x++;
                }
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
