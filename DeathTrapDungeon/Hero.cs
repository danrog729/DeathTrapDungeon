using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathTrapDungeon
{
    public abstract class Hero
    {
        protected string _name;
        public string Name
        {
            get => _name;
            set
            {
                return;
            }
        }

        protected int _originalHP;

        protected int _currentHP;
        public int CurrentHP
        {
            get => _currentHP;
            set
            {
                return;
            }
        }

        protected int _maxDamage;

        protected int _gold;
        public int Gold
        {
            get => _gold;
            set
            {
                _gold = value;
            }
        }

        public Hero(string name, int minHP, int maxHP, int maxDamage)
        {
            _name = name;
            Random random = new Random();
            _originalHP = random.Next(minHP, maxHP);
            _currentHP = _originalHP;
            _maxDamage = maxDamage;
            Gold = 0;
        }

        public int Attack()
        {
            Random random = new Random();
            return random.Next(0, _maxDamage + 1);
        }

        public void ReceiveDamage(int damage)
        {
            _currentHP -= damage;
        }

        public bool Purchase(int cost)
        {
            if (cost <= Gold)
            {
                Gold -= cost;
                return true;
            }
            return false;
        }

        public void Heal(int health)
        {
            if (CurrentHP + health <= _originalHP)
            {
                _currentHP += health;
            }
            else
            {
                _currentHP = _originalHP;
            }
        }
    }

    public class Barbarian : Hero
    {
        public Barbarian(string name) : base(name, 20, 31, 8)
        {

        }
    }

    public class Wizard : Hero
    {
        public Wizard(string name) : base(name, 15, 26, 12)
        {

        }
    }

    public class Warlock : Hero
    {
        public Warlock(string name) : base(name, 17, 28, 10)
        {

        }
    }
}
