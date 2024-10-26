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
                "\t1) Sword (0-6 damage | 90% hit chance)");
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
                    default:
                        validWeapon = false;
                        break;
                }
            }
            weapon.Randomise_Modifier(1);
            Console.WriteLine("You pick up a " + weapon.Name);
            weapon.Inspect();
            hero.Inventory = new PlayerInventory(hero.Inventory.Size);
            hero.Inventory.AddItem(weapon);
            hero.Inventory.ActiveWeaponSlot = 0;

            Console.WriteLine();

            int victories = 0;
            while (victories < 10 && hero.CurrentHP > 0)
            {
                Random random = new Random();
                int monsterChoice = random.Next(0, 4);
                Enemy monster = new Monster("colour", victories);
                switch (monsterChoice)
                {
                    case 0:
                        monster = new Monster(Random_Colour(), victories);
                        break;
                    case 1:
                        monster = new Goblin(Random_Colour(), victories);
                        break;
                    case 2:
                        monster = new Vampire(Random_Colour(), victories);
                        break;
                    case 3:
                        monster = new Slime(Random_Colour(), victories);
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
                        Console.Write("Do you want to visit Ye Olde Dungeon Shoppe? (Y/N) ");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            Shop shop = new Shop();
                            shop.EnterShop(hero);
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

        static bool AbilityChoiceInput(string Message)
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

        static void AbilityDecision(Hero hero, out bool HeroAttack, out bool MonsterAttack)
        {
            HeroAttack = true;
            MonsterAttack = false;
            if (hero is Barbarian & hero.Cooldown == 0)
            {
                if (AbilityChoiceInput("Do you choose to rage? Y/N\nThis uses your turn but allows you to attack twice from next turn, however it also increases the damage you take\nThis ability lasts 3 turns | 1 turn cooldown"))
                {
                    hero.Special = true;
                    HeroAttack = false;
                    hero.Cooldown = 4;
                }
            }
            else if (hero is Wizard & hero.Cooldown == 0)
            {
                if (AbilityChoiceInput("Do you gather magic for a powerful arcane attack? Y/N \nThis will stun your foe and triple your regular damage\nYou can only use it once a fight"))
                {
                    hero.Special = true;
                    MonsterAttack = false;
                    hero.Cooldown = -1; // Will only be available once per fight
                }
            }
            else if (hero is Warlock & hero.Cooldown == 0 & hero.CurrentHP > 1)
            {
                if (AbilityChoiceInput("Do you sacrifice your lifeforce to obtain boons from your patron? Y/N\nYou loose 10% of your health but do double damage\nThis ability has a 2 turn cooldown"))
                {
                    hero.Special = true;
                    hero.ReceiveDamage(Convert.ToInt32(Math.Ceiling(hero.CurrentHP * 0.1)));
                    hero.Cooldown = 3;
                }
            }
        }
        static int HeroAttacks(Hero hero)
        {
            int heroDamage;
            if (hero.Special == true & hero is Wizard)
            {
                heroDamage = 3 * (int)Math.Sqrt(hero.Inventory.GetActiveWeapon().Attack().Damage * hero.Attack());
            }
            else if (hero.Special == true & hero is Warlock)
            {
                heroDamage = 2 * (int)Math.Sqrt(hero.Inventory.GetActiveWeapon().Attack().Damage * hero.Attack());
            }
            else
            {
                heroDamage = (int)Math.Sqrt(hero.Inventory.GetActiveWeapon().Attack().Damage * hero.Attack());
            }
            return heroDamage;
        }

        static void Combat(Hero hero, Enemy monster)
        {

            while (hero.CurrentHP > 0 && monster.HitPoints > 0)
            {
                Console.WriteLine("\n######### Hero: " + hero.CurrentHP + " health #########" +
                    " Monster: " + monster.HitPoints + " health #########");
                //Hero specific actions
                AbilityDecision(hero, out bool HeroAttack, out bool MonsterAttack); //Inputs players choice if to use Hero specific actions
                //Attacking
                if (HeroAttack)
                {
                    int heroDamage;
                    Console.WriteLine("Press enter to attack!");
                    Console.ReadLine();
                    heroDamage = HeroAttacks(hero);
                    monster.ReceiveDamage(heroDamage);
                    //Special actions
                    if (monster.HitPoints > 0 & hero is Barbarian & hero.Cooldown > 0)
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
                    hero.ReceiveDamage(monster.Attack());
                }
                //Ability refresh
                if (hero.Cooldown > 0)
                {
                    hero.Cooldown--;
                }
            }
        }


    }
}
