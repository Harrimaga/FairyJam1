using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Technology
{
    class Technology
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Enums.TechType TechType { get; set; }
        public List<Technology> LockedBy { get; set; }
        public List<Technology> Unlocks { get; set; }
        public bool Researchable { get => LockedBy.Count == 0 && !Researched; }
        public bool Researched { get; set; }
        private List<Behaviour> TechEffects { get; set; }

        public Technology(string name, string description, Enums.TechType techType, bool researched = false)
        {
            Name = name;
            Description = description;
            TechType = techType;
            Researched = researched;
            LockedBy = new List<Technology>();
            Unlocks = new List<Technology>();
            TechEffects = new List<Behaviour>();
        }

        public Technology(string name, string description, Enums.TechType techType, List<Technology> lockedBy, List<Technology> unlocks, List<Behaviour> techEffects, bool researched = false)
        {
            Name = name;
            Description = description;
            TechType = techType;
            LockedBy = lockedBy;
            Unlocks = unlocks;
            Researched = researched;
            TechEffects = techEffects;
        }

        public bool Research(Nation nation)
        {
            if(Researched || !Researchable)
            {
                return false; // You aren't allowed to research this
            }

            foreach(Behaviour effect in TechEffects)
            {
                effect.Execute(nation);
            }

            return true; // It worked
        }

        public class Behaviour
        {
            public Enums.Resource Resource { get; set; }
            public double Amount { get; set; }

            public Behaviour(Enums.Resource resource, double amount)
            {
                Resource = resource;
                Amount = amount;
            }

            public void Execute(Nation nation)
            {
                switch(Resource)
                {
                    case Enums.Resource.POPULATION_GROWTH:
                        nation.Population += Amount;
                        break;
                    case Enums.Resource.TECH_POINTS:
                        nation.TechPoints += Amount;
                        break;
                    default:
                        Globals.logger.Log("Tech couldn't be processed because it changes a resource that doesn't exist", Secretary.LogLevel.ERROR);
                        break;
                }
            }
        }
    }
}
