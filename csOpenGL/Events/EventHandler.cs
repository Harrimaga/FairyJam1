using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Events
{
    public class EventHandler
    {
        public List<Event> events;
        DrawnButton optionA, optionB, optionC;
        List<DrawnButton> buttons = new List<DrawnButton>();

        public EventHandler()
        {
            events = new List<Event>();
            optionA = new DrawnButton("", 1920/2-360, 1080/2+30, 720, 60, () => {if(events.Count > 0) { events[events.Count-1].OptionA();RemoveEvent(events[events.Count-1]); }}, 0, 0, 0, false);
            optionB = new DrawnButton("", 1920 / 2 - 360, 1080 / 2 + 100, 720, 60, () => { if (events.Count > 0) { events[events.Count - 1].OptionB(); RemoveEvent(events[events.Count - 1]); } }, 0, 0, 0, false);
            optionC = new DrawnButton("", 1920 / 2 - 360, 1080 / 2 + 170, 720, 60, () => { if (events.Count > 0) { events[events.Count - 1].OptionC(); RemoveEvent(events[events.Count - 1]); } }, 0, 0, 0, false);
            buttons.Add(optionA);
            buttons.Add(optionB);
            buttons.Add(optionC);
        }

        public void SpawnEvent(Event e)
        {
            events.Add(e);
        }

        public void RemoveEvent(Event e)
        {
            events.Remove(e);
        }

        public void Draw()
        {
            if(events.Count == 0) return;
            events[events.Count-1].Draw();
        }

        public void SpawnRandomEvent() 
        {
            SpawnEvent(new Event()); // TODO dit moet random event zijn
        }

        public void TurnTick() 
        {
            if(Globals.random.Next(100) < 75) {
                SpawnRandomEvent();
            }
        }

        public void MouseDown(MouseButtonEventArgs e, int mx, int my)
        {
            if (e.Button == MouseButton.Left)
            {
                foreach (DrawnButton button in buttons)
                {
                    if (button != null && button.IsInButton(mx + Window.camX, my + Window.camY))
                    {
                        button.OnClick();
                        break;
                    }
                }
            }
        }
    }
}
