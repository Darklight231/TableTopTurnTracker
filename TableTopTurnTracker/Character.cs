
namespace TableTopTurnTracker
{
    public class Character
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Initiative { get; set; }
        public int Regen { get; set; }
        public int Exp { get; set; }

        public List<StatusEffect> StatusEffects { get; set; } = new List<StatusEffect>();

        // Add a status effect
        public void AddStatusEffect(string name, int duration, int damage)
        {
            StatusEffects.Add(new StatusEffect(name, duration, damage));
        }

        // Tick all status effects and reduce their duration
        public void TickStatusEffects()
        {
            for (int i = StatusEffects.Count - 1; i >= 0; i--)
            {
                StatusEffects[i].Duration--;
                if (StatusEffects[i].Duration <= 0)
                    StatusEffects.RemoveAt(i);
            }
        }
    }
}
