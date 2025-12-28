Here’s a clean, beginner-friendly **README.md** you can use for this project. You can copy-paste it directly into a `README.md` file.

---

# TableTop Turn Tracker

A **C# console-based turn tracker and combat manager** designed for tabletop RPGs (such as D&D-style games).
This tool helps you manage characters, initiative order, HP, regeneration, status effects, and combat rounds in a simple text interface.

---

## Features

### Character Management

* Add characters with:

  * Name
  * HP
  * Initiative
  * Regeneration
  * Experience value
* Remove characters safely with confirmation
* View all character stats at once
* Edit character stats during play

### Status Effects System

* Add multiple status effects to characters
* Each status effect can:

  * Last for a set number of rounds
  * Deal damage over time
  * Heal over time (negative damage)
* Status effects automatically tick down each round

### Combat System

* Initiative-based turn order
* Turn-by-turn combat loop
* Available actions per turn:

  * Attack
  * Spell / Ability / Debuff (multi-target supported)
  * Heal / Buff (multi-target supported)
  * End turn
  * Exit combat early
* Automatic handling of:

  * Damage
  * Healing
  * Regeneration
  * Death
  * Experience gain

---

## Project Structure

```
TableTopTurnTracker/
│
├── Program.cs          // Main menu and character management
├── CombatManager.cs    // Combat loop and combat actions
├── Character.cs        // Character data and logic
├── StatusEffect.cs     // Status effect system
```

---

## How to Run

### Requirements

* .NET SDK (6.0 or newer recommended)
* A terminal or command prompt

### Steps

1. Clone or download the repository
2. Open a terminal in the project folder
3. Run:

   ```bash
   dotnet run
   ```
4. Follow the on-screen menu prompts

---

## Main Menu Options

```
1. Add Character
2. Remove Character
3. Check Character Status
4. Change Status
5. Start Combat
6. End
```

---

## Combat Rules Overview

* Characters act in **initiative order (highest first)**
* Each round:

  * Status effects apply
  * Status durations tick down
  * Regeneration heals
* Characters with **0 HP** are removed from combat
* Total EXP is calculated from defeated characters

---

## Design Notes

* This is a **manual-input system** by design
  (perfect for a DM running a session at the table)
* Damage, healing, and spells are intentionally flexible
* Status effects are generic so they can represent:

  * Poison
  * Burning
  * Bleeding
  * Regeneration
  * Buffs / Debuffs

---

## Possible Future Improvements

* Saving/loading characters
* Team or faction support
* Max HP tracking

---

## License

This project is provided as-is for educational and personal tabletop use.
You are free to modify and expand it.


