using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathTrapDungeon
{
    public class Shop
    {
        private List<Item> inventory;

        public Shop()
        {
            inventory = new List<Item>();

            inventory.Add(new DilutedHealingElixir());
            inventory.Add(new ImpureHealingElixir());
            inventory.Add(new AxeOfFlames());
            inventory.Add(new SwordOfSouls());
        }

        private void PrintStock()
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                Console.WriteLine((i + 1) + ") Buy " + inventory[i].ToString() + " | " + inventory[i].Value + " gold.");
            }
            Console.WriteLine(inventory.Count + 1 + ") Exit");
        }

        public void EnterShop(Hero hero)
        {
            bool shouldContinue = true;
            while (shouldContinue)
            {
                PrintStock();
                Console.WriteLine("Your current gold: " + hero.Gold);
                bool validSelection = false;
                while (!validSelection)
                {
                    validSelection = true;
                    int choice = Program.Valid_Int_Input("Select option: ");
                    if (choice >= inventory.Count)
                    {
                        Console.WriteLine("That item does not exist!");
                    }
                    else
                    {
                        Item item = inventory[choice];
                        if (!hero.Inventory.IsFull && hero.Purchase(item.Value)) // this has to be an && rather than an & otherwise you'll get charged even if your inv is full
                        {
                            hero.Inventory.AddItem(item);
                        }
                        else if (hero.Inventory.IsFull)
                        {
                            Console.WriteLine("You cannot hold any more!");
                        }
                        else
                        {
                            Console.WriteLine("You do not have enough wealth to afford this item.");
                        }
                    }
                }
                Console.Write("\nDo you want to make another purchase? (Y/N) ");
                shouldContinue = Console.ReadLine().ToLower() == "y";
            }
        }
    }
}
