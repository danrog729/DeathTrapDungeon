using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathTrapDungeon
{
    public abstract class Item
    {
        private int _value;
        public int Value
        {
            get => _value; 
            set => _value = value;
        }

        private readonly string _inspectMessage;

        public Item(int value, string inspectMessage)
        {
            _value = value;
            _inspectMessage = inspectMessage;
        }

        public void Inspect()
        {
            Console.WriteLine(_inspectMessage);
        }
    }





    public class HealthPotion : Item
    {
        private int _healAmountMin;
        public int HealAmountMin
        {
            get => _healAmountMin;
            set
            {
                return;
            }
        }

        private int _healAmountMax;
        public int HealAmountMax
        {
            get => _healAmountMax;
            set
            {
                return;
            }
        }

        private string _useMessage;

        public HealthPotion(int healAmountMin, int healAmountMax, string useMessage, int value, string inspectMessage) : base(value, inspectMessage)
        {
            _healAmountMin = healAmountMin;
            _healAmountMax = healAmountMax;
            _useMessage = useMessage;
        }

        public int Use()
        {
            Console.WriteLine(_useMessage);
            Random random = new Random();
            return random.Next(_healAmountMin, _healAmountMax);
        }
    }





    public class DilutedHealingElixir : HealthPotion
    {
        public DilutedHealingElixir() : base(
            3,
            8,
            "The weak elixir eases you slightly, but isn't the most pleasant...",
            4,
            "This vial contains a semi-red liquid. There are a few glittery particles inside.")
        {

        }
    }

    public class ImpureHealingElixir : HealthPotion
    {
        public ImpureHealingElixir() : base(
            5,
            11,
            "Your wounds feel somewhat better, and you feel full of a new vigour.",
            10,
            "This conical flash contains a red liquid. There are a few glittery particles inside.")
        {

        }
    }

    public class DistilledHealingElixir : HealthPotion
    {
        public DistilledHealingElixir() : base(
            15,
            21,
            "As the formulation pours down your throat, you feel better in body and soul, ready to take on the next fight!",
            20,
            "This boiling flash contains a deep crimson liquid. There is a whirlpool of glistening particles inside."
            )
        {

        }
    }
}
