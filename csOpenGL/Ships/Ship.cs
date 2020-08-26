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

        /// <summary>
        ///     Add an amount of resource to the ship
        /// </summary>
        /// <param name="resourceAmount"></param>
        public void AddResource(int resourceAmount)
        {
            if (ResourceLoad + resourceAmount <= MaxResourceLoad)
                ResourceLoad += resourceAmount;
        }
        /// <summary>
        ///     Drop an amount of resource from ship
        /// </summary>
        /// <param name="resourceAmount"></param>
        public void DropResource(int resourceAmount)
        {
            ResourceLoad -= resourceAmount;
        }

        /// <summary>
        ///     Drop all resources from the ship
        /// </summary>
        public void DropAllResources()
        {
            ResourceLoad = 0;
        }

        /// <summary>
        ///     Method which handles attacks on other ships
        /// </summary>
        /// <param name="enemy"></param>
        public void AttackShip(Ship enemy)
        {
            bool hit = Globals.random.Next(0, 100) >= Evasiveness;

            if (hit)
                enemy.HealthPoints -= Damage;
            
        }

    }
}
