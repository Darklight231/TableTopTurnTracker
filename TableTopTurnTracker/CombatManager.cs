using System;
using System.Collections.Generic;
using System.Linq;

namespace TableTopTurnTracker
{
    public static class CombatManager
    {
        public static void StartCombat(List<Character> characters)
        {
            int totalExp = 0;
            if (characters.Count == 0)
            {
                Console.WriteLine("No characters to fight.\n");
                return;
            }

            Console.WriteLine("\n=== Combat Started ===\n");

            // Sort by initiative
            characters.Sort((a, b) => b.Initiative.CompareTo(a.Initiative));

            bool combatRunning = true;
            while (combatRunning && characters.Count > 0)
            {
                List<Character> deadThisRound = new List<Character>();

                foreach (var character in characters.ToList())
                {
                    if (character.HP <= 0) continue;


                    // Apply status effects
                    foreach (var effect in character.StatusEffects.ToList())
                    {
                        if (effect.Damage != 0)
                        {
                            character.HP -= effect.Damage;
                            Console.WriteLine(effect.Damage > 0
                                ? $"{character.Name} takes {effect.Damage} damage from {effect.Name}! HP: {character.HP}"
                                : $"{character.Name} heals {-effect.Damage} from {effect.Name}! HP: {character.HP}");
                        }
                    }
                    character.TickStatusEffects();

                    if (character.HP <= 0)
                    {
                        deadThisRound.Add(character);
                        Console.WriteLine($"{character.Name} has died!");
                        continue;
                    }

                    if (character.Regen > 0)
                    {
                        character.HP += character.Regen;
                        Console.WriteLine($"{character.Name} regenerates {character.Regen} HP! Current HP: {character.HP}");
                    }

                    bool turnOver = false;
                    while (!turnOver)
                    {
                        Console.WriteLine($"\nIt is {character.Name}'s turn! (HP: {character.HP})\n");
                        Console.WriteLine("===========================================");
                        Console.WriteLine("1. Attack");
                        Console.WriteLine("2. Spell/Ability/Debuff");
                        Console.WriteLine("3. Heal/Buff");
                        Console.WriteLine("4. End Turn");
                        Console.WriteLine("5. Exit Combat");
                        Console.WriteLine("===========================================");

                        Console.Write("Choose an action: ");
                        if (!int.TryParse(Console.ReadLine(), out int choice))
                        {
                            Console.WriteLine("Invalid input.\n");
                            continue;
                        }

                        switch (choice)
                        {
                            case 1:
                                Attack(character, characters);
                                break;

                            case 2:
                                Spell(character, characters);
                                break;

                            case 3:
                                Heal(character, characters);
                                break;

                            case 4:
                                turnOver = true;
                                Console.WriteLine($"{character.Name} ends their turn.");
                                break;

                            case 5:
                                combatRunning = false;
                                turnOver = true;
                                break;

                            default:
                                Console.WriteLine("Invalid choice.\n");
                                break;
                        }
                    }

                    deadThisRound.AddRange(
                        characters.Where(c => c.HP <= 0 && !deadThisRound.Contains(c))
                    );
                }

                foreach (var dead in deadThisRound)
                {
                    Console.WriteLine($"{dead.Name} is dead!!!");
                    totalExp += dead.Exp;
                    characters.Remove(dead);
                }

                if (characters.Count == 0)
                {
                    combatRunning = false;
                    Console.WriteLine("\nAll characters have died. Combat ended.\n");
                }
            }

            Console.WriteLine("\n=== Combat Ended ===\n");
            Console.WriteLine("Total Exp Gained: " + totalExp);
        }

        public static void Attack(Character attacker, List<Character> characters)
        {
            Console.WriteLine($"\nWho does {attacker.Name} attack?:");
            foreach (var c in characters.Where(c => c != attacker && c.HP > 0))
                Console.WriteLine($"- {c.Name}");

            Console.Write("Enter target name: ");
            string targetName = Console.ReadLine().Trim();
            var target = characters.Find(c =>
                string.Equals(c.Name, targetName, StringComparison.OrdinalIgnoreCase) && c.HP > 0);

            if (target != null)
            {
                Console.Write("Damage amount: ");
                if (int.TryParse(Console.ReadLine(), out int damage))
                {
                    Damage(damage, target);
                }
                else
                {
                    Console.WriteLine("Invalid number. Attack cancelled.\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid target.\n");
            }
        }

        public static void Spell(Character caster, List<Character> characters)
        {
            Console.WriteLine("\nWho does the spell affect? (comma-separated names example Bob,Jim)");
            foreach (var c in characters.Where(c => c.HP > 0))
                Console.WriteLine($"- {c.Name}");

            Console.Write("Enter target name(s): ");
            string[] targetNames = Console.ReadLine().Split(',');

            Console.Write("Spell damage amount: ");
            if (!int.TryParse(Console.ReadLine(), out int damage))
            {
                Console.WriteLine("Invalid number, spell cancelled.\n");
                return;
            }

            foreach (var name in targetNames)
            {
                var target = characters.Find(c =>
                    string.Equals(c.Name, name.Trim(), StringComparison.OrdinalIgnoreCase) && c.HP > 0);

                if (target != null)
                    Damage(damage, target);
                else
                    Console.WriteLine($"Target {name.Trim()} not found.\n");
            }
        }

        public static void Heal(Character caster, List<Character> characters)
        {
            Console.WriteLine("\nWho does the heal affect? (comma-separated names example Bob,Jim)");
            foreach (var c in characters.Where(c => c.HP > 0))
                Console.WriteLine($"- {c.Name}");

            Console.Write("Enter target name(s): ");
            string[] targetNames = Console.ReadLine().Split(',');

            Console.Write("Healing amount: ");
            if (!int.TryParse(Console.ReadLine(), out int healAmount))
            {
                Console.WriteLine("Invalid number, heal cancelled.\n");
                return;
            }

            foreach (var name in targetNames)
            {
                var target = characters.Find(c =>
                    string.Equals(c.Name, name.Trim(), StringComparison.OrdinalIgnoreCase) && c.HP > 0);

                if (target != null)
                {
                    target.HP += healAmount;
                    Console.WriteLine($"{target.Name} heals {healAmount} HP. Current HP: {target.HP}");
                }
                else
                {
                    Console.WriteLine($"Target {name.Trim()} not found.\n");
                }
            }
        }

        public static void Damage(int damage, Character target)
        {
            target.HP -= damage;
            if (target.HP < 0) target.HP = 0;

            Console.WriteLine($"{target.Name} takes {damage} damage! Remaining HP: {target.HP}\n");
        }
    }
}
