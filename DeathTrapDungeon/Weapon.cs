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

    public abstract class Weapon : Item
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

        public Weapon(string newName, int newDamage, int newDamageChance, string newHitMessage, string newMissMessage, string newInspectMessage, int value) : base(value, newInspectMessage)
        {
            _name = newName;
            modifier = new Modifier("common", 0);
            damage = newDamage;
            damageChance = newDamageChance;
            hitMessage = newHitMessage;
            missMessage = newMissMessage;
            inspectMessage = newInspectMessage;
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

        protected int GenerateDamageValue()
        {
            Random random = new Random();
            if (random.Next(1, 101) <= damageChance)
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

        public virtual DamageType Attack()
        {
            return new DamageType(GenerateDamageValue());
        }

        public override string ToString()
        {
            return Name;
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
        public Sword() : base(
            "Sword",
            6,
            90,
            "You slash the monster with your sword-like sword.",
            "You slash the air with your sword-like sword.",
            "Your sword feels solid in your hands.",
            1)
        {

        }

        public override DamageType Attack()
        {
            return new PierceDamage(GenerateDamageValue());
        }
    }

    public class Axe : Weapon
    {
        public Axe() : base (
            "Axe",
            8,
            70,
            "The axe embeds into the enemy with your strong swing.",
            "You are sent staggering by the momentum of your missed attack.",
            "A small inscription lies on the handle, reading \"M.K\". Probably the blacksmith.",
            1)
        {

        }

        public override DamageType Attack()
        {
            return new SlashDamage(GenerateDamageValue());
        }
    }

    public class Dagger : Weapon
    {
        public Dagger() : base (
            "Dagger",
            5,
            100,
            "You manage to drive your dagger hilt-deep into the enemy.",
            "Your jab misses entirely, only piercing the air.",
            "Flipping it over your hand, you can't help but remark on the lightness of this dagger.",
            1)
        {

        }

        public override DamageType Attack()
        {
            return new PierceDamage(GenerateDamageValue());
        }
    }

    public class Scimitar : Weapon
    {
        public Scimitar() : base(
            "Scimitar",
            6,
            90,
            "The curve of the blade allows for a wider slash at the enemy, which you take advantage of.",
            "The unorthodox shape of the scimitar doesn't lend itself to your skill, missing entirely.",
            "Running your finger along the curved blade, you recognise what a formidable weapon it truly is.",
            1)
        {

        }

        public override DamageType Attack()
        {
            return new SlashDamage(GenerateDamageValue());
        }
    }

    public class Mace : Weapon
    {
        public Mace() : base(
            "Mace",
            6,
            90,
            "The mace crashes down into the enemy, with audibly broken bones.",
            "The mace crashes into the ground, missing the enemy by inches.",
            "The heavy implement almost has a mind of its own with its inertia.",
            1)
        {

        }

        public override DamageType Attack()
        {
            return new BluntDamage(GenerateDamageValue());
        }
    }

    public class Hammer : Weapon
    {
        public Hammer() : base(
            "Hammer",
            9,
            60,
            "The heavy head of the hammer ploughs into their side.",
            "The weight of the weapon slows it too much to hit the enemy.",
            "Looks like an overgrown builder's hammer. Great for applying a huge force to targets.",
            1)
        {

        }

        public override DamageType Attack()
        {
            return new SlashDamage(GenerateDamageValue());
        }
    }

    public class Fisticuffs : Weapon
    {
        public Fisticuffs() : base(
            "Fisticuffs",
            2,
            40,
            "You punch the enemy right in the guts.",
            "You swing your fist at the enemy.",
            "You look at your bloodied and clothed fists.",
            1
            )
        {

        }

        public override DamageType Attack()
        {
            return new BluntDamage(GenerateDamageValue());
        }
    }

    public class AxeOfFlames : Weapon
    {
        public AxeOfFlames() : base(
            "Axe of Flames",
            8,
            80,
            "The cold metal hacks the enemy, while the fire burns their wound.",
            "Your enemy is intimidated by a wall of flames.",
            "You lift the flaming axe. Your enemies will surely know fear.",
            8)
        {

        }

        public override DamageType Attack()
        {
            return new SlashDamage(GenerateDamageValue());
        }
    }

    public class SwordOfSouls : Weapon
    {
        public SwordOfSouls() : base(
            "Sword of Souls",
            10,
            95,
            "Your enemy is struck with the blood of thousands.",
            "Your enemy hears the souls trapped within the blade.",
            "You unsheathe the sword. A threatening aura surrounds it.",
            12)
        {

        }

        public override DamageType Attack()
        {
            return new PierceDamage(GenerateDamageValue());
        }
    }
}
