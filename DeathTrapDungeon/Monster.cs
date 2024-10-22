using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DeathTrapDungeon
{
    public class Monster
    {
        private string _colour;
        public string Colour
        {
            get => _colour;
            set
            {
                _colour = value;
            }
        }

        private string _species;
        public string Species
        {
            get => _species;
            set
            {
                _species = value;
            }
        }

        private int _hitPoints;
        public int HitPoints
        {
            get => _hitPoints;
            set
            {
                _hitPoints = value;
            }
        }

        private int _maxDamage;
        public int MaxDamage
        {
            get => _maxDamage;
            set
            {
                _maxDamage = value;
            }
        }

        private string _attackMessage;
        public string AttackMessage
        {
            get => _attackMessage;
            set
            {
                _attackMessage = value;
            }
        }

        private int _gold;
        public int Gold
        {
            get => _gold;
            set
            {
                _gold = value;
            }
        }

        public Monster(string newColour)
        {
            _colour = newColour;
            _species = "monster";
            _hitPoints = 10;
            _maxDamage = 5;
            _attackMessage = "It slashes with its razor sharp claws.";
            Random random = new Random();
            _gold = random.Next(4, 7);
        }

        public int Attack()
        {
            Random random = new Random();
            int damage = random.Next(0, _maxDamage + 1);
            switch (damage)
            {
                case 0:
                    Console.WriteLine("You successfully dodge the blow!");
                    break;
                case 1:
                    Console.WriteLine("A glancing blow! You take 1 damage.");
                    break;
                default:
                    Console.WriteLine(_attackMessage + " You take " + damage + " damage.");
                    break;
            }
            return damage;
        }

        public void ReceiveDamage(int damage)
        {
            switch (damage)
            {
                case 0:
                    Console.WriteLine("Miss! The " + _species + " is too fast for you.");
                    break;
                case 1:
                    Console.WriteLine("You strike a glancing blow for 1 point of damage.");
                    break;
                default:
                    Console.WriteLine("The " + _species + " takes " + damage + " damage.");
                    break;
            }
            _hitPoints -= damage;
        }

        public virtual void Talk()
        {
            Console.WriteLine("ROAR!! I'm going to eat you!");
        }
    }

    public class Goblin : Monster
    {
        public Goblin(string newColour) : base(newColour)
        {
            Species = "goblin";
            HitPoints = 12;
            MaxDamage = 6;
            AttackMessage = "It hits you with a club.";
            Random random = new Random();
            Gold = random.Next(4, 13);
        }

        public override void Talk()
        {
            Console.WriteLine("Hee! Hee! I'm going to kill you and steal all your gold!");
        }
    }

    public class Vampire : Monster
    {
        public Vampire(string newColour) : base(newColour)
        {
            Species = "vampire";
            HitPoints = 20;
            MaxDamage = 7;
            AttackMessage = "It sinks its fangs into your neck.";
            Random random = new Random();
            Gold = random.Next(3, 11);
        }

        public override void Talk()
        {
            Console.WriteLine("I vant to drink your blood!");
        }
    }

    public class Slime : Monster
    {
        public Slime(string newColour) : base(newColour)
        {
            Species = "slime";
            HitPoints = 8;
            MaxDamage = 4;
            AttackMessage = "It tries to engulf you.";
            Random random = new Random();
            Gold = random.Next(0, 7);
        }

        public override void Talk()
        {
            Console.WriteLine("Boooiiinnnggg!");
        }
    }
}
