using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Events
{
    public class EventHandler
    {
        List<Event> events;
        public EventHandler()
        {
            events = new List<Event>();
        }

        public void SpawnEvent(Event e)
        {
            events.Add(e);
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

    }
}
