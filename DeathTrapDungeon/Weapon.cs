using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathTrapDungeon
{
    public class WeaponModifiers
    {
        public readonly Modifier[] modifiers = {
            new Modifier("legendary", 5),
            new Modifier("godly", 4),
            new Modifier("mighty", 2),
            new Modifier("rare", 1),
            new Modifier("common", 0),
            new Modifier("shoddy", -1),
            new Modifier("awful", -2),
            new Modifier("broken", -3)
        };

        public WeaponModifiers()
        {
            return;
        }
    }

    public abstract class Weapon
    {
        protected string _name;
        public string Name
        {
            get
            {
                return modifier.name + " " + _name;
            }
            set
            {
                return;
            }
        }

        protected Modifier modifier;
        protected int damage;
        protected int damageChance;
        protected string hitMessage;
        protected string missMessage;
        protected string inspectMessage;

        public Weapon()
        {
            
        }

        public void Randomise_Modifier(int luck)
        {
            WeaponModifiers modifiersList = new WeaponModifiers();

            // luck of 1 is normal luck
            luck *= 20; // fudge factor

            int modifierIndex = modifiersList.modifiers.Length - 1;
            Random random = new Random();
            for (int luckLeft = luck; luckLeft > 0; luckLeft--)
            {
                int choice = random.Next(0, luckLeft);
                if (choice == 0 && modifierIndex > 0)
                {
                    modifierIndex -= 1;
                }
            }
            Modifier newModifier = modifiersList.modifiers[modifierIndex];
            modifier = new Modifier(newModifier.name, newModifier.damageModifier);
        }

        public int Attack()
        {
            Random random = new Random();
            if (random.Next(1,101) <= damageChance)
            {
                Console.WriteLine(hitMessage);
                return damage + modifier.damageModifier;
            }
            else
            {
                Console.WriteLine(missMessage);
                return 0;
            }
        }

        public void Inspect()
        {
            Console.WriteLine(inspectMessage);
        }
    }

    public class Modifier
    {
        public readonly string name;
        public readonly int damageModifier;

        public Modifier(string Name, int DamageModifier)
        {
            name = Name;
            damageModifier = DamageModifier;
        }
    }
    public class Sword : Weapon
    {
        public Sword() : base()
        {
            _name = "Sword";
            modifier = new Modifier("common", 0);
            damage = 6;
            damageChance = 90;
            hitMessage = "You slash the monster with your sword-like sword.";
            missMessage = "You slash the air with your sword-like sword.";
            inspectMessage = "Your sword feels solid in your hands.";
        }
    }

    public class AxeOfFlames : Weapon
    {
        public AxeOfFlames() : base()
        {
            _name = "Axe of Flames";
            modifier = new Modifier("common", 0);
            damage = 8;
            damageChance = 80;
            hitMessage = "The cold metal hacks the enemy, while the fire burns their wound.";
            missMessage = "Your enemy is intimidated by a wall of flames.";
            inspectMessage = "You lift the flaming axe. Your enemies will surely know fear.";
        }
    }

    public class SwordOfSouls : Weapon
    {
        public SwordOfSouls() : base()
        {
            _name = "Sword of Souls";
            modifier = new Modifier("common", 0);
            damage = 10;
            damageChance = 95;
            hitMessage = "Your enemy is struck with the blood of thousands.";
            missMessage = "Your enemy hears the souls trapped within the blade.";
            inspectMessage = "You unsheathe the sword. A threatening aura surrounds it.";
        }
    }
}
