using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FairyJam.Events;

namespace FairyJam.UI
{
    class OneMinuteUI : UI
    {

        public List<ListEntry> scrolledButtons = new List<ListEntry>();
        public List<DrawnButton> scrollListButtons = new List<DrawnButton>();
        int scroll = 0;
        Sprite bg;

        public OneMinuteUI()
        {
            // Tabs
            // - Leaders
            // - Scientists
            // - Supplies

            bg = new Sprite(1920, 1080, 0, Textures.Get(Textures.minuteBG));

            // Finish Button -> events if time left
            buttons.Add(new DrawnButton("Leaders", 1920 / 2 - 700, 10, 400, 50, () => { Globals.leaderUI.reSelect(); }, 0.5f, 0.5f, 0.5f));

            buttons.Add(new DrawnButton("Scientists", 1920 / 2 - 300, 10, 400, 50, () => { Globals.scientistUI.reSelect(); }, 0.5f, 0.5f, 0.5f));

            buttons.Add(new DrawnButton("Supplies", 1920 / 2 + 100, 10, 400, 50, () => { Globals.suppliesUI.reSelect(); }, 0.5f, 0.5f, 0.5f));

            buttons.Add(new DrawnButton("Embark!", 1920 - 200, 1080 - 100, 200, 100, () => { Globals.eventHandler.SpawnEvent(new Event());/*Globals.currentState = GameState.MAPVIEW; Globals.activeButtons = new List<DrawnButton>();*/ }, 0.5f, 0.5f, 0.5f));

            for(int i = 0; i < 35; i++)
            {
                int k = i;
                scrollListButtons.Add(new DrawnButton("", 100, 60 + 25 * i, 300, 25, () => { SelectFromList(k + scroll); }, Textures.Get(Textures.personSelectionBox), 1, 1, 1));
                buttons.Add(scrollListButtons[scrollListButtons.Count-1]);
            }

        }

        public void Update()
        {

        }

        public override void Draw()
        {
            bg.Draw(0, 0, false);
            Window.window.DrawText(Globals.timer.ToString(true), 0, 0);
            for(int i = 0; i < 35; i++)
            {
                if (scroll + i >= scrolledButtons.Count) break;
                scrolledButtons[scroll+i].Draw(i);
            }

            Window.window.DrawText("Leaders: (" + Globals.PlayerNation.leaders.Count + "/10)", 1500, 25, false, Globals.buttonFont);
            if (Globals.PlayerNation.leaders.Count == 0)
            {
                Window.window.DrawText("None", 1525, 60, false, Globals.buttonFont);
            }
            for(int i = 0; i < Globals.PlayerNation.leaders.Count; i++)
            {
                Window.window.DrawText(Globals.PlayerNation.leaders[i].ToStringShort(), 1525, 60 + i * 25, false, Globals.buttonFont);
                int x = 0;
                foreach(Trait t in Globals.PlayerNation.leaders[i].Traits)
                {
                    t.sprite.Draw(1798 + 32*x, 60 + 25 * i, false);
                    x++;
                }
            }

            int offset = Globals.PlayerNation.leaders.Count;
            if(offset == 0) {
                offset = 1;
            }

            Window.window.DrawText("Scientists: (" + Globals.PlayerNation.scientists.Count + "/10)", 1500, 85 + offset * 25, false, Globals.buttonFont);
            if (Globals.PlayerNation.scientists.Count == 0)
            {
                Window.window.DrawText("None", 1525, 120 + offset * 25, false, Globals.buttonFont);
            }
            for (int i = 0; i < Globals.PlayerNation.scientists.Count; i++)
            {
                Window.window.DrawText(Globals.PlayerNation.scientists[i].ToStringShort(), 1525, 120 + (i + offset) * 25, false, Globals.buttonFont);
                int x = 0;
                foreach (Trait t in Globals.PlayerNation.scientists[i].Traits)
                {
                    t.sprite.Draw(1798 + 32 * x, 120 + 25 * (i + offset), false);
                    x++;
                }
            }

            if (Globals.PlayerNation.scientists.Count == 0)
            {
                offset++;
            }
            offset += Globals.PlayerNation.scientists.Count;

            Window.window.DrawText("Supplies: (" + Globals.PlayerNation.supplyCompanies.Count + "/10)", 1500, 145 + offset * 25, false, Globals.buttonFont);
            Window.window.DrawText("Materials: " + Globals.PlayerNation.Materials, 1525, 180 + (0 + offset) * 25, false, Globals.buttonFont);
            Window.window.DrawText("Food: " + Globals.PlayerNation.Food, 1525, 180 + (1 + offset) * 25, false, Globals.buttonFont);
            Window.window.DrawText("Fuel: " + Globals.PlayerNation.Fuel, 1525, 180 + (2 + offset) * 25, false, Globals.buttonFont);
            Window.window.DrawText("Population: " + Globals.PlayerNation.Population, 1525, 180 + (3 + offset) * 25, false, Globals.buttonFont);
            Window.window.DrawText("Money: " + Globals.PlayerNation.Money, 1525, 180 + (4 + offset) * 25, false, Globals.buttonFont);


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
