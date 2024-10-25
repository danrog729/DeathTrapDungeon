using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DeathTrapDungeon
{
    public class PlayerInventory
    {
        private int size;
        public int Size
        {
            get => size;
            set
            {
                return;
            }
        }

        private int _activeWeaponSlot;
        public int ActiveWeaponSlot
        {
            get => _activeWeaponSlot;
            set
            {
                if (value < size)
                {
                    _activeWeaponSlot = value;
                }
            }
        }

        private List<Item> items;

        private bool _isFull;
        public bool IsFull
        {
            get => _isFull;
            set
            {
                return;
            }
        }

        public PlayerInventory(int maxSize)
        {
            size = maxSize;
            items = new List<Item>();
        }

        public bool AddItem(Item item)
        {
            if (_isFull)
            {
                return false;
            }
            items.Add(item);
            if (items.Count >= size)
            {
                _isFull = true;
            }
            return true;
        }

        public bool RemoveItem(Item item)
        {
            foreach (Item candidateItem in items)
            {
                if (candidateItem.Equals(item))
                {
                    items.Remove(candidateItem);
                    return true;
                }
            }
            return false;
        }

        private void Print()
        {
            string output = "";
            for (int index = 0; index < items.Count; index++)
            {
                output += "\t1. " + items[index].ToString();
            }
            Console.WriteLine(output);
        }

        public Weapon GetActiveWeapon()
        {
            if (items[_activeWeaponSlot] is Weapon)
            {
                return (Weapon)items[_activeWeaponSlot];
            }
            else
            {
                return new Fisticuffs();
            }
        }

        public void AccessInventory()
        {

        }
    }
}
