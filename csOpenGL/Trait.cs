using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    class Trait
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Action> Actions { get; set; } // All effects
        public delegate void Action();

        public Sprite sprite;

        public Trait()
        {
            Actions = new List<Action>();
        }

        public Trait(string name, string description, List<Action> actions)
        {
            Name = name;
            Description = description;
            Actions = actions;
        }

        public override string ToString()
        {
            return Name;
        }

        public void Update()
        {
            foreach(Action action in Actions)
            {
                action();
            }
        }
    }
}
