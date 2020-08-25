using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam
{
    /// <summary>
    ///     Abstract Class which contains the main elements of a ship
    /// </summary>
    abstract class Ship
    {
        public string Name { get; set; }
        public int HealthPoints { get; set; }
        public int Damage { get; set; }
        public int ResourceLoad { get; set; }
        public int MaxResourceLoad { get; set; }
        List<Person> PeopleLoad;
        public int MaxPeopleLoad { get; set; }
        public int Evasiveness { get; set; }
        public int Speed { get; set; }
        
        //public int weaponAmount, speed, evasiveness, carryPeopleAmount;

        protected Ship(int health, int damage, int evasiveness, int speed, int maxPeopleAmount, int maxResourceAmount)
        {
            HealthPoints = health;
            Damage = damage;
            Evasiveness = evasiveness;
            Speed = speed;

            MaxPeopleLoad = maxPeopleAmount;
            MaxResourceLoad = maxResourceAmount;

            PeopleLoad = new List<Person>();
        }

        /// <summary>
        ///     Add person to the ship
        /// </summary>
        /// <param name="person">Person to be added to the ship</param>
        public void AddPerson(Person person)
        {
            if (PeopleLoad.Count < MaxPeopleLoad && !PeopleLoad.Contains(person))
                PeopleLoad.Add(person);
        }
        /// <summary>
        ///     Drop person from the ship
        /// </summary>
        /// <param name="person">Person to be dropped from the ship</param>
        public void DropPerson(Person person)
        {
            if (PeopleLoad.Contains(person))
                PeopleLoad.Remove(person);
        }

        public void AddResource(int resourceAmount)
        {
            if (ResourceLoad + resourceAmount <= MaxResourceLoad)
                ResourceLoad += resourceAmount;
        }

        public void DropAllResources()
        {
            ResourceLoad = 0;
        }

        public void AttackShip(Ship enemy)
        {

        }
    }
}
