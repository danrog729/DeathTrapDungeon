using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathTrapDungeon
{
    public class DamageType
    {
        private int _damage;
        public int Damage 
        { 
            get => _damage; 
            set => _damage = value; 
        }

        public DamageType(int damage)
        {
            _damage = damage;
        }
    }

    public class PierceDamage : DamageType
    {
        public PierceDamage(int damage) : base(damage)
        {

        }
    }

    public class SlashDamage : DamageType
    {
        public SlashDamage(int damage) : base(damage)
        {

        }
    }

    public class BluntDamage : DamageType
    {
        public BluntDamage(int damage) : base(damage)
        {

        }
    }

    public class SpellDamage : DamageType
    {
        public SpellDamage(int damage) : base(damage)
        {

        }
    }
}
