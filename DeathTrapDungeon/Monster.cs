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
    public abstract class Enemy
    {
        protected string _colour;
        public string Colour
        {
            get => _colour;
            set
            {
                _colour = value;
            }
        }

        protected string _species;
        public string Species
        {
            get => _species;
            set
            {
                _species = value;
            }
        }

        protected int _hitPoints;
        public int HitPoints
        {
            get => _hitPoints;
            set
            {
                _hitPoints = value;
            }
        }

        protected int _maxDamage;
        public int MaxDamage
        {
            get => _maxDamage;
            set
            {
                _maxDamage = value;
            }
        }

        protected string _attackMessage;
        public string AttackMessage
        {
            get => _attackMessage;
            set
            {
                _attackMessage = value;
            }
        }

        protected int _gold;
        public int Gold
        {
            get => _gold;
            set
            {
                _gold = value;
            }
        }

        public Enemy(string newColour, int newLevel, string newSpecies, int newHP, int newMaxDamage, string newAttackMessage, int newGoldLowerBound, int newGoldUpperBound)
        {
            _colour = newColour;
            _species = newSpecies;
            _hitPoints = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(newHP)*(1+newLevel/10)));
            _maxDamage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(newMaxDamage) * (1 + newLevel / 10)));
            _attackMessage = newAttackMessage;
            Random random = new Random();
            _gold = random.Next(newGoldLowerBound, newGoldUpperBound);
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

        }
    }

    public class Monster : Enemy
    {
        public Monster(string newColour, int newLevel) : base(newColour, newLevel, "monster", 10, 5, "It slashes with its razor sharp claws.", 4, 7)
        {

        }

        public override void Talk()
        {
            Console.WriteLine("ROAR!! I'm going to eat you!");
        }
    }

    public class Goblin : Enemy
    {
        public Goblin(string newColour, int newLevel) : base(newColour, newLevel, "goblin", 12, 6, "It hits you with a club.", 4, 13)
        {

        }

        public override void Talk()
        {
            Console.WriteLine("Hee! Hee! I'm going to kill you and steal all your gold!");
        }
    }

    public class Vampire : Enemy
    {
        public Vampire(string newColour, int newLevel) : base(newColour, newLevel, "vampire", 20, 7, "It sinks its fangs into your neck.", 3, 11)
        {

        }

        public override void Talk()
        {
            Console.WriteLine("I vant to drink your blood!");
        }
    }

    public class Slime : Enemy
    {
        public Slime(string newColour, int newLevel) : base(newColour, newLevel, "slime", 8, 4, "It tries to engulf you.", 0, 7)
        {

        }

        public override void Talk()
        {
            Console.WriteLine("Boooiiinnnggg!");
        }
    }
}
