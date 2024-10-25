using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DeathTrapDungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("############# Welcome to Death Trap Dungeon! ############\n\n" +
                "What is your name, mighty warrior? ");
            string name = Console.ReadLine();

            Console.WriteLine("What manner of warrior are you?\n" +
                "\t1) Barbarian (20-30 health | 0-8 damage)\n" +
                "\t2) Wizard (15-25 health | 0-12 damage)\n" +
                "\t3) Warlock (17-27 health | 0-10 damage)");
            Hero hero = new Barbarian("name", new Sword());
            bool validHero = false;
            while (!validHero)
            {
                validHero = true;
                int choice = Valid_Int_Input("");
                switch (choice)
                {
                    case 1:
                        hero = new Barbarian(name, new Sword());
                        break;
                    case 2:
                        hero = new Wizard(name, new Sword());
                        break;
                    case 3:
                        hero = new Warlock(name, new Sword());
                        break;
                    default:
                        validHero = false;
                        break;
                }
            }

            Console.WriteLine("Which weapon is of your choosing?\n" +
                "\t1) Sword (0-6 damage | 90% hit chance)\n" +
                "\t2) Axe (0-8 damage | 70% hit chance)\n" +
                "\t3) Dagger (0-5 damage | 100% hit chance)\n" +
                "\t4) Scimitar (0-6 damage | 90% hit chance)\n" +
                "\t5) Mace (0-6 damage | 90% hit chance)\n" +
                "\t6) Hammer (0-9 damage | 60% hit chance)\n" +
                "\t7) Fisticuffs (0-2 damage | 40% hit chance)\n");
            Weapon weapon = new Sword();
            bool validWeapon = false;
            while (!validWeapon)
            {
                validWeapon = true;
                int choice = Valid_Int_Input("");
                switch (choice)
                {
                    case 1:
                        weapon = new Sword();
                        break;
                    case 2:
                        weapon = new Axe();
                        break;
                    case 3:
                        weapon = new Dagger();
                        break;
                    case 4:
                        weapon = new Scimitar();
                        break;
                    case 5:
                        weapon = new Mace();
                        break;
                    case 6:
                        weapon = new Hammer();
                        break;
                    case 7:
                        weapon = new Fisticuffs();
                        break;
                    default:
                        validWeapon = false;
                        break;
                }
            }
            hero.Inventory = new PlayerInventory(10);
            hero.Inventory.AddItem(weapon);
            hero.Inventory.ActiveWeaponSlot = 0;
            weapon.Randomise_Modifier(1);
            Console.WriteLine("You pick up a " + weapon.Name);
            weapon.Inspect();

            Console.WriteLine();

            int victories = 0;
            while (victories < 10 && hero.CurrentHP > 0)
            {
                Random random = new Random();
                int monsterChoice = random.Next(0, 4);
                Enemy monster = new Monster("colour");
                switch (monsterChoice)
                {
                    case 0:
                        monster = new Monster(Random_Colour());
                        break;
                    case 1:
                        monster = new Goblin(Random_Colour());
                        break;
                    case 2:
                        monster = new Vampire(Random_Colour());
                        break;
                    case 3:
                        monster = new Slime(Random_Colour());
                        break;
                }

                Console.WriteLine("You are attacked by a " + monster.Colour + " " + monster.Species + ".");
                monster.Talk();
                Combat(hero, monster);
                if (hero.CurrentHP > 0)
                {
                    // won the fight
                    int gold = monster.Gold;
                    Console.WriteLine("The monster dropped " + gold + " gold coins.");
                    hero.Gold += gold;
                    victories++;
                    Console.WriteLine("You are victorious! Press enter for the next battle!");
                    Console.ReadLine();
                    Console.WriteLine("Gold coins: " + hero.Gold);

                    if (victories < 10)
                    {
                        Console.Write("Do you want to visit Ye Olde Dungeon Shoppe (Y/N) ");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            Shop shop = new Shop();
                            shop.EnterShop(hero);
                        }
                        Console.Write("Do you want to enter your inventory (Y/N) ");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            hero.Inventory.AccessInventory();
                        }
                    }
                }
                else
                {
                    Console.WriteLine(hero.Name + ", you are dead. Death Trap Dungeon claims another victim.");
                }
            }

            if (victories == 10)
            {
                Console.WriteLine(hero.Name + ", you are the Champion of Champions! Fame and fortune are yours!\n" +
                    "You leave the dungeon with " + hero.Gold + " gold coins!");
            }
            Console.WriteLine("######## GAME OVER ########");

            Console.ReadLine();
        }

        static bool AbilityChoice(string Message)
        {
            Console.WriteLine(Message);
            string Choice = Console.ReadLine();
            if (Choice.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int Valid_Int_Input(string message)
        {
            int output;
            do
            {
                Console.Write(message);
            }
            while (!Int32.TryParse(Console.ReadLine(), out output));
            return output;
        }

        static string Random_Colour()
        {
            Random random = new Random();
            int colour = random.Next(0, 5);
            switch (colour)
            {
                case 0:
                    return "green";
                case 1:
                    return "yellow";
                case 2:
                    return "red";
                case 3:
                    return "purple";
                case 4:
                    return "black";
                default:
                    return "not possible";
            }
        }

        static void Combat(Hero hero, Enemy monster)
        {
            int Cooldown= 0; // Cooldown includes cast turn, so "3 turns of cooldown after cast turn" - Cooldown = 4
            while (hero.CurrentHP > 0 && monster.HitPoints > 0)
            {
                Console.WriteLine("\n######### Hero: " + hero.CurrentHP + " health #########" +
                    " Monster: " + monster.HitPoints + " health #########");
                //Hero specific actions
                bool HeroAttack = true; //Basic hero attack
                bool MonsterAttack = true; 
                bool Special = false; //Each Hero types special ability
                
                if (hero is Barbarian & Cooldown == 0) 
                {
                    if (AbilityChoice("Do you choose to rage? Y/N\nThis uses your turn but allows you to attack twice from next turn, however it also increases the damage you take\nThis ability lasts 3 turns | 1 turn cooldown"))
                    {
                        Special = true;
                        HeroAttack = false;
                        Cooldown = 4;
                    }
                }
                else if (hero is Wizard & Cooldown == 0)   
                {
                    if (AbilityChoice("Do you gather magic for a powerful arcane attack? Y/N \nThis will stun your foe and triple your regular damage\nYou can only use it once a fight"))
                    {
                        Special = true;
                        MonsterAttack = false;
                        Cooldown = -1; // Will only be available once per fight
                    }
                }
                else if (hero is Warlock & Cooldown == 0 & hero.CurrentHP > 1)
                {
                    if (AbilityChoice("Do you sacrifice your lifeforce to obtain boons from your patron? Y/N\nYou loose 10% of your health but do double damage\nThis ability has a 2 turn cooldown"))
                    {
                        Special = true;
                        hero.ReceiveDamage(Convert.ToInt32(Math.Ceiling(hero.CurrentHP * 0.1)));
                        Cooldown = 3;
                    }
                }
                //Attacking
                if (HeroAttack)
                {
                    Console.WriteLine("Press enter to attack!");
                    int heroDamage;
                    Console.ReadLine();
                    if (Special & hero is Wizard)
                    {
                        heroDamage = 3 * (int)Math.Sqrt(hero.Inventory.GetActiveWeapon().Attack().Damage * hero.Attack());
                    }
                    else if (Special & hero is Warlock)
                    {
                        heroDamage = 2 * (int)Math.Sqrt(hero.Inventory.GetActiveWeapon().Attack().Damage * hero.Attack());
                    }
                    else
                    {
                        heroDamage = (int)Math.Sqrt(hero.Inventory.GetActiveWeapon().Attack().Damage * hero.Attack());
                    }
                    monster.ReceiveDamage(heroDamage);
                    //Special actions
                    if (monster.HitPoints > 0 & hero is Barbarian & Cooldown > 0)
                    {
                        Console.WriteLine("Press enter to attack again");
                        Console.ReadLine();
                        heroDamage = (int)Math.Sqrt(hero.Inventory.GetActiveWeapon().Attack().Damage * hero.Attack());
                        monster.ReceiveDamage(heroDamage);
                    }
                }
                //Defending
                if (monster.HitPoints > 0 & MonsterAttack)
                {
                    Console.WriteLine("The " + monster.Species + " attacks...");
                    Console.WriteLine("Press enter to defend!");
                    Console.ReadLine();
                    if (hero is Barbarian & Cooldown > 0) //Barbarians take 20% more damage whilst raging rounding up
                    {
                        hero.ReceiveDamage(Convert.ToInt32(Math.Ceiling(1.2*monster.Attack())));
                    }
                    else
                    {
                        hero.ReceiveDamage(monster.Attack());
                    }
                }
                //Ability refresh
                if (Cooldown > 0)
                {
                    Cooldown --;
                }
            }
        }
    }
}
