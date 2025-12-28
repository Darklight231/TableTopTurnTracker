namespace TableTopTurnTracker
{
    public class StatusEffect
    {
        public string Name { get; set; }
        public int Duration { get; set; } // in rounds
        public int Damage { get; set; } // can be negative for healing

        public StatusEffect(string name, int duration, int damage)
        {
            Name = name;
            Duration = duration;
            Damage = damage;
        }

        public override string ToString()
        {
            if (Damage == 0)
                return $"{Name} lasts for {Duration} rounds";
            else if (Damage > 0)
                return $"{Name} does {Damage} damage for {Duration} rounds";
            else
                return $"{Name} heals {-Damage} for {Duration} rounds";
        }
    }
}
