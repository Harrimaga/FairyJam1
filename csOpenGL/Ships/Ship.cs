using FairyJam.Equipment;
using FairyJam.Equipment.SpecialEquipment;
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
    public abstract class Ship
    {
        //Ship basic stats
        public string Name { get; set; }
        public int HealthPoints { get; set; }
        public int DamageBonus { get; set; }
        public int Evasiveness { get; set; }
        public int Speed { get; set; }

        public int MaxSlots { get; set; }

        //Equipment of ship
        public Dictionary<Enums.WeaponType, bool> AllowedWeaponTypes { get; set; }
        public List<Weapon> WeaponList { get; set; }
        public SpecialEquipment Special { get; set; }

        //Load Capacity for resources and people
        public int ResourceLoad { get; set; }
        public int MaxResourceLoad { get; set; }
        List<Person> PeopleLoad;
        public int MaxPeopleLoad { get; set; }
        

        protected Ship(string name, int health, int damageBonus, int evasiveness, int speed, int maxWeaponSlots, int maxPeopleAmount, int maxResourceAmount)
        {
            Name = name;
            HealthPoints = health;
            DamageBonus = damageBonus;
            Evasiveness = evasiveness;
            Speed = speed;
            MaxSlots = maxWeaponSlots;

            WeaponList = new List<Weapon>();
            AllowedWeaponTypes = new Dictionary<Enums.WeaponType, bool>();

            MaxPeopleLoad = maxPeopleAmount;
            MaxResourceLoad = maxResourceAmount;

            PeopleLoad = new List<Person>();
        }

        protected abstract void AddWeaponTypes();

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
        public void AttackShip(Ship enemy, Weapon weapon)
        {
            bool hit = Globals.random.Next(0, weapon.Accuracy) >= Evasiveness;

            if (hit)
                enemy.HealthPoints -= Globals.random.Next(weapon.MinDamage, weapon.MaxDamage);
        }

        /// <summary>
        ///     Add weapon to ship arsenal
        /// </summary>
        /// <param name="weapon">Weapon to be added</param>
        public void AddWeapon(Weapon weapon)
        {
            WeaponList.Add(weapon);
        }

        /// <summary>
        ///     Remove weapon from ship arsenal
        /// </summary>
        /// <param name="weapon">Weapon to be removed</param>
        public void RemoveWeapon(Weapon weapon)
        {
            WeaponList.Remove(weapon);
        }

        /// <summary>
        ///     Add Special Equipment Weapon to the ship arsenal
        /// </summary>
        /// <param name="special">The Special Equipment to be added</param>
        public void AddSpecialEquipment(SpecialEquipment special)
        {
            Special = special;
        }
    }
}
