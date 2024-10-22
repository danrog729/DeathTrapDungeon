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
            Hero hero = new Barbarian("name");
            bool validHero = false;
            while (!validHero)
            {
                validHero = true;
                int choice = Valid_Int_Input("");
                switch (choice)
                {
                    case 1:
                        hero = new Barbarian(name);
                        break;
                    case 2:
                        hero = new Wizard(name);
                        break;
                    case 3:
                        hero = new Warlock(name);
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

            Console.WriteLine();

            int victories = 0;
            while (victories < 10 && hero.CurrentHP > 0)
            {
                Random random = new Random();
                int monsterChoice = random.Next(0, 4);
                Monster monster = new Monster("colour");
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
                Combat(hero, weapon, monster);
                if (hero.CurrentHP > 0)
                {
                    // won the fight
                    int gold = monster.Gold;
                    Console.WriteLine("The monster dropped " + gold + " gold coins.");
                    hero.Gold = gold;
                    victories++;
                    Console.WriteLine("You are victorious! Press enter for the next attack!");
                    Console.ReadLine();
                    Console.WriteLine("Gold coins: " + hero.Gold);

                    if (victories < 10)
                    {
                        Console.Write("Do you want to visit Ye Olde Dungeon Shoppe (Y/N) ");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            weapon = Shop(hero, weapon);
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

        static int Valid_Int_Input(string message)
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

        static void Combat(Hero hero, Weapon weapon, Monster monster)
        {
            while (hero.CurrentHP > 0 && monster.HitPoints > 0)
            {
                Console.WriteLine("\n######### Hero: " + hero.CurrentHP + " health #########" +
                    " Monster: " + monster.HitPoints + " health #########");
                bool BaseAttack;
                string Choice;
                if (hero is Barbarian) //Hero Specific actions
                {
                    Console.WriteLine("Do you choose to rage? Y/N (This uses your turn but allows you to attack twice from next turn, however it also increases the damage you take)")
                    Choice = Console.ReadLine();
                    if (Choice.ToLower() == "y")
                    {
                        BaseAttack = false;
                    }
                }
                else if (hero is Wizard)
                {
                    Console.WriteLine("Do you gather magic for a powerful arcane attack? Y/N (This will stun your foe ontop of your regular damage, but you can only use it once a fight)")
                }
                else if (hero is Warlock)
                {
                    Console.WriteLine("Do you sacrifice your lifeforce to obtain boons from your patron? Y/N (You loose 10% of your health but do double the damage")
                    Base
                }
                else
                {
                    BaseAttack = true;
                }
                if (BaseAttack)
                {
                    Console.WriteLine("Press enter to attack!");
                    Console.ReadLine();
                    int heroDamage = (int)Math.Sqrt(weapon.Attack() * hero.Attack());
                    monster.ReceiveDamage(heroDamage);
                }
                if (monster.HitPoints > 0)
                {
                    Console.WriteLine("The " + monster.Species + " attacks...");
                    Console.WriteLine("Press enter to defend!");
                    hero.ReceiveDamage(monster.Attack());
                }
            }
        }

        static Weapon Shop(Hero hero, Weapon weapon)
        {
            Weapon newWeapon = weapon;
            bool shouldContinue = true;
            while (shouldContinue)
            {
                Console.WriteLine("\n############## Welcome to Ye Olde Dungeon Shoppe! ##########\n" +
                    "1) Buy basic health potion (+2-8 health)\t\t 2 gold\n" +
                    "2) Buy standard health potion (+4-12 health)\t\t 4 gold\n" +
                    "3) Buy Axe of Fire (0-8 damage | 80% damage chance)\t\t 8 gold\n" +
                    "4) Buy Sword of Souls (0-10 damage | 95% damage chance)\t\t 12 gold\n" +
                    "5) Exit shoppe");
                Console.WriteLine("Your current gold: " + hero.Gold);
                bool validSelection = false;
                while (!validSelection)
                {
                    validSelection = true;
                    int choice = Valid_Int_Input("Select option: ");
                    switch (choice)
                    {
                        case 1:
                            {
                                Random random = new Random();
                                int healthPotion = random.Next(2, 9);
                                if (hero.Purchase(2))
                                {
                                    Console.WriteLine("\nYou swig a health potion. You recover " + healthPotion + " HP.");
                                    hero.Heal(healthPotion);
                                }
                                else
                                {
                                    Console.WriteLine("\nYou do not have enough gold.");
                                }
                                break;
                            }
                        case 2:
                            {
                                Random random = new Random();
                                int healthPotion = random.Next(4, 13);
                                if (hero.Purchase(4))
                                {
                                    Console.WriteLine("\nYou swig a health potion. You recover " + healthPotion + " HP.");
                                    hero.Heal(healthPotion);
                                }
                                else
                                {
                                    Console.WriteLine("\nYou do not have enough gold.");
                                }
                                break;
                            }
                        case 3:
                            {
                                if (hero.Purchase(8))
                                {
                                    newWeapon = new AxeOfFlames();
                                    Console.WriteLine("You pick up a " + newWeapon.Name);
                                    newWeapon.Inspect();
                                }
                                else
                                {
                                    Console.WriteLine("\nYou do not have enough gold.");
                                }
                                break;
                            }
                        case 4:
                            {
                                if (hero.Purchase(12))
                                {
                                    newWeapon = new SwordOfSouls();
                                    Console.WriteLine("You pick up a " + newWeapon.Name);
                                    newWeapon.Inspect();
                                }
                                else
                                {
                                    Console.WriteLine("\nYou do not have enough gold.");
                                }
                                break;
                            }
                        case 5:
                            return newWeapon;
                        default:
                            Console.WriteLine("This item does not exist!");
                            validSelection = false;
                            break;
                    }
                }
                Console.Write("\nDo you want to make another purchase? (Y/N) ");
                shouldContinue = Console.ReadLine().ToLower() == "y";
            }
            return newWeapon;
        }
    }
}
