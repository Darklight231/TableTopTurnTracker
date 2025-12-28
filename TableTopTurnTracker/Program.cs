using System;
using System.Collections.Generic;

namespace TableTopTurnTracker
{
    class Program
    {
        static List<Character> characters = new List<Character>();

        public static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===============================");
                Console.WriteLine("1. Add Character");
                Console.WriteLine("2. Remove Character");
                Console.WriteLine("3. Check Character Status");
                Console.WriteLine("4. Change Status");
                Console.WriteLine("5. Start Combat");
                Console.WriteLine("6. End");
                Console.WriteLine("===============================\n");

                if (!int.TryParse(Console.ReadLine(), out int input))
                {
                    Console.WriteLine("Invalid input.\n");
                    continue;
                }

                switch (input)
                {
                    case 1:
                        AddCharacter();
                        break;
                    case 2:
                        RemoveCharacter();
                        break;
                    case 3:
                        CheckStats();
                        break;
                    case 4:
                        ChangeStats();
                        break;
                    case 5:
                        Combat();
                        break;
                    case 6:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Input 1–6 only.\n");
                        break;
                }
            }
        }

        static void AddCharacter()
        {
            Console.Write("\nName: ");
            string name = Console.ReadLine();

            Console.Write("HP: ");
            int hp = int.Parse(Console.ReadLine());

            Console.Write("Initiative: ");
            int initiative = int.Parse(Console.ReadLine());


            Console.Write("Exp (if player put 0): ");
            int exp = int.Parse(Console.ReadLine());

            // Regeneration
            int regen = 0;
            Console.Write($"Does {name} have Regeneration? (y/n): ");
            string regenResponse = Console.ReadLine().Trim().ToLower();
            if (regenResponse == "y")
            {
                Console.Write($"How much does {name} heal per round?: ");
                regen = int.Parse(Console.ReadLine());
            }

            var character = new Character
            {
                Name = name,
                HP = hp,
                Initiative = initiative,
                Regen = regen,
                Exp = exp
            };

            // Add status effects
            while (true)
            {
                Console.Write("\nAdd a status effect? (y/n): ");
                string response = Console.ReadLine().Trim().ToLower();
                if (response == "n") break;

                Console.Write("Status effect name: ");
                string effectName = Console.ReadLine();

                Console.Write("Duration (in rounds): ");
                int duration = int.Parse(Console.ReadLine());

                int damage = 0;
                Console.Write("Does it deal damage? (y/n): ");
                if (Console.ReadLine().Trim().ToLower() == "y")
                {
                    Console.Write("Damage per round: ");
                    damage = int.Parse(Console.ReadLine());
                }

                character.AddStatusEffect(effectName, duration, damage);
            }

            characters.Add(character);
            Console.WriteLine($"\nCharacter {name} added.\n");
        }

        static void RemoveCharacter()
        {
            while (true)
            {
                if (characters.Count == 0)
                {
                    Console.WriteLine("No characters found.\n");
                    return;
                }

                Console.WriteLine("\nCurrent Characters:");
                foreach (var c in characters)
                {
                    Console.WriteLine($"- {c.Name}");
                }
                Console.WriteLine();

                Console.Write("Enter character name to remove or type q to go back: ");
                string name = Console.ReadLine().Trim();

                if (name.ToLower() == "q")
                {
                    Console.WriteLine("Returning to menu.\n");
                    return;
                }

                bool exists = characters.Exists(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
                if (!exists)
                {
                    Console.WriteLine($"{name} is not a character. Input a valid character.\n");
                    continue;
                }

                Console.Write($"Are you sure you want to remove {name}? (y/n): ");
                string response = Console.ReadLine().Trim().ToLower();

                if (response == "n")
                {
                    Console.WriteLine("Removal canceled.\n");
                    return;
                }
                else if (response == "y")
                {
                    int removed = characters.RemoveAll(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
                    Console.WriteLine($"{removed} character(s) removed.\n");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid input. Removal canceled.\n");
                    return;
                }
            }
        }

        static void CheckStats()
        {
            if (characters.Count == 0)
            {
                Console.WriteLine("No characters to display.\n");
                return;
            }

            Console.WriteLine("\nCharacter Status:\n");
            foreach (var c in characters)
            {
                string effects = c.StatusEffects.Count > 0
                    ? string.Join(", ", c.StatusEffects)
                    : "None";

                Console.WriteLine($"{c.Name} | HP: {c.HP} | Initiative: {c.Initiative} | Regeneration: {c.Regen} | Status Effects: {effects}\n");
            }
        }

        static void ChangeStats()
        {
            while (true)
            {
                if (characters.Count == 0)
                {
                    Console.WriteLine("No characters found.\n");
                    return;
                }

                Console.WriteLine("\nCurrent Characters:");
                foreach (var c in characters)
                {
                    Console.WriteLine($"- {c.Name}");
                }
                Console.WriteLine();

                Console.Write("Enter character name to edit or type q to go back: ");
                string name = Console.ReadLine().Trim();

                if (name.ToLower() == "q")
                {
                    Console.WriteLine("Returning to menu.\n");
                    return;
                }

                var character = characters.Find(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));

                if (character == null)
                {
                    Console.WriteLine($"{name} is not a character. Input a valid character.\n");
                    continue;
                }

                bool editing = true;
                while (editing)
                {
                    Console.WriteLine($"\nEditing {character.Name}:\n");
                    Console.WriteLine("1. Change HP");
                    Console.WriteLine("2. Change Initiative");
                    Console.WriteLine("3. Add Status Effect");
                    Console.WriteLine("4. Remove Status Effect");
                    Console.WriteLine("5. Change Exp");
                    Console.WriteLine("6. Back to main menu\n");

                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Invalid input.\n");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            Console.Write("New HP: ");
                            character.HP = int.Parse(Console.ReadLine());
                            Console.WriteLine("HP updated.\n");
                            break;

                        case 2:
                            Console.Write("New Initiative: ");
                            character.Initiative = int.Parse(Console.ReadLine());
                            Console.WriteLine("Initiative updated.\n");
                            break;

                        case 3:
                            Console.Write("Status effect name: ");
                            string effectName = Console.ReadLine();

                            Console.Write("Duration (in rounds): ");
                            int duration = int.Parse(Console.ReadLine());

                            int damage = 0;
                            Console.Write("Does it deal damage? (y/n): ");
                            if (Console.ReadLine().Trim().ToLower() == "y")
                            {
                                Console.Write("Damage per round: ");
                                damage = int.Parse(Console.ReadLine());
                            }

                            character.AddStatusEffect(effectName, duration, damage);
                            Console.WriteLine("Status effect added.\n");
                            break;

                        case 4:
                            if (character.StatusEffects.Count == 0)
                            {
                                Console.WriteLine("No status effects to remove.\n");
                                break;
                            }

                            Console.WriteLine("Current status effects:");
                            for (int i = 0; i < character.StatusEffects.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {character.StatusEffects[i]}");
                            }

                            Console.Write("\nEnter number to remove: ");
                            int index = int.Parse(Console.ReadLine()) - 1;
                            if (index >= 0 && index < character.StatusEffects.Count)
                            {
                                Console.WriteLine($"{character.StatusEffects[index].Name} removed.\n");
                                character.StatusEffects.RemoveAt(index);
                            }
                            else
                            {
                                Console.WriteLine("Invalid selection.\n");
                            }
                            break;

                        case 5:
                            Console.Write("New Exp: ");
                            character.Exp = int.Parse(Console.ReadLine());
                            Console.WriteLine("Exp updated.\n");
                            break;

                        case 6:
                            editing = false;
                            break;
                        default:
                            Console.WriteLine("Invalid option.\n");
                            break;
                    }
                }
                return;
            }
        }

        static void Combat()
        {
            CombatManager.StartCombat(characters);
        }
    }
}
