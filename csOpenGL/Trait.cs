using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    public class Trait
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Action> Actions { get; set; } // All effects
        public delegate void Action(Nation n);

        public Sprite sprite { get; set; }

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

        public void Turn(Nation n)
        {
            foreach(Action action in Actions)
            {
                action(n);
            }
        }
    }
}
