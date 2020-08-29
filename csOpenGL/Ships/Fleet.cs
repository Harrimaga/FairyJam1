﻿using FairyJam.Orbitals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyJam.Ships
{
    public class Fleet
    {
        public List<Ship> ships;
        private PlanetarySystem destination, next, origin, current;
        private int turnsTillDestination;
        private Queue<PlanetarySystem> queue;
        public Nation owner;
        public string Name { get; set; }

        public Fleet(Nation owner, List<Ship> ships)
        {
            this.ships = ships;
            this.owner = owner;
            owner.AddFleet(this);
            Name = "Fleet " + Globals.random.Next(100, 999);
        }

        public void AddToFleet(Ship s)
        {
            ships.Add(s);
        }

        public void RemoveFromFleet(Ship s)
        {
            ships.Remove(s);
        }

        public void SetDestination(PlanetarySystem destination, PlanetarySystem origin)
        {
            this.destination = destination;
            this.origin = origin;
            current = origin;
            next = origin;

            // TODO: Calculate Turn Time
            

            queue = BFS(origin, destination);
            SetNextNode();
        }

        public void SetNextNode()
        {
            // Find next node from destination and current location

            // Find path from current -> origin
            // Return next node in path

            if(queue != null && turnsTillDestination == 0 && next != null) 
            {
                current.fleets.Remove(this);
                current = next;
                current.fleets.Add(this);
                if (queue.Count > 0)
                {
                    next = queue.Dequeue();
                    turnsTillDestination = Globals.getTurnDistance(current, next, GetSpeed());
                }
                else 
                {
                    next = null;
                    if (current.Owner != owner && !current.EnemyFleetsPresent(owner))
                    {
                        current.Owner = owner;
                    }
                }
            }
        }

        public void Turn()
        {
            if (turnsTillDestination >= 1)
            {
                turnsTillDestination--;
            }
            SetNextNode();
        }

        private Queue<PlanetarySystem> BFS(PlanetarySystem from, PlanetarySystem to)
        {
            MinHeap<PsNode> priorQueue = new MinHeap<PsNode>(Globals.map.mapWidth*Globals.map.mapHeight);
            priorQueue.Add(new PsNode(from, 0));
            int r = Globals.random.Next();
            double speed = GetSpeed();
            while(priorQueue.Count() > 0) 
            {
                PsNode psn = priorQueue.GetMin();
                foreach(PlanetarySystem ps in psn.ps.neighbours) 
                {
                    if(ps == to) 
                    {
                        psn.path.Enqueue(ps);
                        psn.TotalDistance += Globals.getTurnDistance(ps, psn.ps, speed);
                        if(owner.Fuel < psn.TotalDistance*ships.Count*owner.getMod(Enums.Modifier.FuelEfficiency)) {
                            return null;
                        }
                        owner.Fuel -= psn.TotalDistance*ships.Count*owner.getMod(Enums.Modifier.FuelEfficiency);
                        return psn.path;
                    }
                    if(ps.bfsVisited != r) 
                    {
                        ps.bfsVisited = r;
                        priorQueue.Insert(new PsNode(ps, psn.TotalDistance + Globals.getTurnDistance(ps, psn.ps, speed), psn));
                    }
                }
            }

            return null;
        }


        public int GetSpeed()
        {
            int lowest = int.MaxValue;
            foreach (Ship ship in ships)
            {
                if (ship.Speed < lowest)
                {
                    lowest = ship.Speed;
                }
            }
            return lowest;
        }

        public double DamageTotal() 
        {
            double damage = 0;
            foreach(Ship s in ships) 
            {
                foreach (Weapon weapon in s.WeaponList)
                {
                    damage += (weapon.MaxDamage + weapon.MinDamage) / 2;
                }
            }
            return damage;
        }

        public double GetEvasiveness()
        {
            double e = 0;
            foreach (Ship ship in ships)
            {
                e += ship.Evasiveness;
            }
            return e / ships.Count;
        }

        public double GetMaxHull()
        {
            double e = 0;
            foreach (Ship ship in ships)
            {
                e += ship.MaxHealth;
            }
            return e;
        }

        public double GetCurrentHull()
        {
            double e = 0;
            foreach (Ship ship in ships)
            {
                e += ship.HealthPoints;
            }
            return e;
        }

        public double GetTransportCap()
        {
            double e = 0;
            foreach (Ship ship in ships)
            {
                e += ship.MaxPeopleLoad;
            }
            return e;
        }

        public double GetResourceCap()
        {
            double e = 0;
            foreach (Ship ship in ships)
            {
                e += ship.MaxResourceLoad;
            }
            return e;
        }

    }

    public class PsNode : IHeapItem, IComparable<PsNode> 
    {

        public PlanetarySystem ps;
        public int TotalDistance;
        public long heapIndex;
        public Queue<PlanetarySystem> path;

        public PsNode(PlanetarySystem ps, int TotalDistance, PsNode prev) 
        {
            this.ps = ps;
            this.TotalDistance = TotalDistance;
            path = new Queue<PlanetarySystem>(prev.path);
            path.Enqueue(ps);
        }

        public PsNode(PlanetarySystem ps, int TotalDistance) 
        {
            this.ps = ps;
            this.TotalDistance = TotalDistance;
            path = new Queue<PlanetarySystem>();
        }

        public void SetHeapIndex(long index) 
        {
            heapIndex = index;
        }

        public int CompareTo(PsNode p) 
        {
            return TotalDistance.CompareTo(p.TotalDistance);
        }

    }

    public interface IHeapItem { void SetHeapIndex(long index); }

    public class MinHeap<T> where T : IComparable<T>, IHeapItem
    {

        private long maxSize, size;
        private T[] data;

        public MinHeap(long maxSize)
        {
            this.maxSize = maxSize;
            data = new T[maxSize + 1];
            size = 0;
        }

        public void Add(T t)
        {
            size++;
            data[size] = t;
            t.SetHeapIndex(size);
        }

        private void Swap(long i, long j)
        {
            T t = data[i];
            data[i] = data[j];
            data[j] = t;
            data[i].SetHeapIndex(i);
            data[j].SetHeapIndex(j);
        }

        public void Heapify(long index)
        {
            long j = index;
            if (index * 2 <= size && data[j].CompareTo(data[index * 2]) > 0)
            {
                j = index * 2;
            }
            if (index * 2 + 1 <= size && data[j].CompareTo(data[index * 2 + 1]) > 0)
            {
                j = index * 2 + 1;
            }
            if (j != index)
            {
                Swap(j, index);
                Heapify(j);
            }
        }

        public void Rootify(long index)
        {
            if (index > 1 && data[index].CompareTo(data[index / 2]) < 0)
            {
                Swap(index, index / 2);
                Rootify(index / 2);
            }
        }

        public void BuildHeap()
        {
            for (long i = size / 2; i > 0; i--)
            {
                Heapify(i);
            }
        }

        public void Insert(T t)
        {
            Add(t);
            Rootify(size);
        }

        public T GetMin()
        {
            T t = data[1];
            data[1] = data[size];
            size--;
            Heapify(1);
            return t;
        }

        public T this[long i]
        {
            get
            {
                return data[i];
            }
            set
            {
                data[i] = value;
            }
        }

        public long Count()
        {
            return size;
        }

    }

}
