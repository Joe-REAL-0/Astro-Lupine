# 🐺 The Astro-Lupine

![Version](https://img.shields.io/badge/version-v1.0.0-blue)
> A custom character mod for **Slay the Spire 2**, built with Godot + C# and the [BaseLib](https://github.com/Alchyr/BaseLib-StS2) modding framework.

**The Astro-Lupine** is a new custom character mod for *Slay the Spire 2*. He is a gray wolf beastman wandering between the sea of stars and the Spire. Designed with a Cyberpunk/Wasteland aesthetic, he crushes enemies through a precise **Register Computing System**.

---

## ✨ Features

- 🔧 **Register System (Registers)** — A brand-new character-exclusive Buff mechanic, with three permanent registers controlling Attack, Defense, and Card Draw.
- 📖 **Read** — Superimposes the register value onto the card's base value, and is immune to Debuffs like Weak/Vulnerable.
- ✏️ **Write** — Forcefully overwrites and assigns values to the registers, allowing for infinite scaling.
- 🎴 **75+ Cards** — Fully implemented 75+ cards, including starter cards and a rich card pool forming multiple synergistic builds.
- 🌐 **Multilingual Support** — Fully localized in Chinese / English.

---

## 🎮 Core Mechanics

### Registers

At the start of combat, the character automatically gains three Register Buffs:

| Register | Starting Value | Function |
| -------- | :----: | -------- |
| Attack Register ⚔️ | 6 | Controls damage output scaling |
| Defense Register 🛡️ | 5 | Controls survivability floor |
| Draw Register 🃏 | 2 | Controls engine efficiency |

> The minimum value for all registers is **1**, and they cannot drop to 0.

### Keywords

| Keyword | Calculation Formula | Description |
| ------- | ------------------- | ----------- |
| **Read** | `Final Value = Base Card Value + Register Stacks` | Applied *after* all other Buffs/Debuffs are calculated. |
| **Write** | `Register Stacks := Specified Value` | Overwrites the register's value instead of adding/subtracting. |
| **Increment/Decrement (Inc/Dec)** | `Register Stacks += Delta` | Relative addition/subtraction based on the current value. |

### Starter Deck (12 Cards)

| Card | × | Cost | Effect Summary |
| ---- | -- | :--: | -------------- |
| Strike | 5 | 1 | 0 Base Damage + **Read** |
| Defend | 5 | 1 | 0 Base Block + **Read** |
| RA++ | 1 | 1 | Attack Register **+1** (Increment) |
| RD++ | 1 | 1 | Defense Register **+1** (Increment) |

---

## 🏗️ Project Structure

```text
Astro Lupine/
├── Scripts/
│   ├── Cards/
│   │   ├── BaseAstroLupineCard.cs      # Base card class (shared Read/Write logic)
│   │   ├── AstroLupineDynamicVars.cs   # Dynamic variables (description value binding)
│   │   ├── AstroLupineKeywords.cs      # Keyword definitions
│   │   ├── Starter/                    # Starter cards (Strike/Defend/RA++/RD++)
│   │   └── Common/                     # Common card pool (18 cards)
│   ├── Characters/
│   │   ├── AstroLupineCharacter.cs     # Character definition (HP/Gold/Deck/Relic Pool)
│   │   ├── AstroLupineCardPool.cs      # Card pool registration
│   │   └── AstroLupineRelicPool.cs     # Relic pool registration
│   ├── Powers/
│   │   ├── BaseRegisterPower.cs        # Base register Buff class
│   │   ├── AttackRegisterPower.cs      # Attack register
│   │   ├── DefenseRegisterPower.cs     # Defense register
│   │   └── DrawRegisterPower.cs        # Draw register
│   └── Relics/
│       └── AstroLupineStarterRelic.cs  # Starter relic
├── localization/
│   ├── zhs/                            # Simplified Chinese localization
│   └── en-US/                          # English localization
├── docs/
│   ├── development_plan.md             # Development roadmap
│   ├── common_card_stack.md            # Common card pool design document
│   └── export_and_run_tutorial.md      # Build and run tutorial
├── AstroLupine.json                    # Mod manifest (BaseLib)
├── AstroLupine.csproj                  # .NET project configuration
└── project.godot                       # Godot project configuration
```

---

## 🛠️ Tech Stack

| Layer | Technology |
| ----- | ---------- |
| Engine | Godot 4.5 (Forward+) |
| Language | C# / .NET 9.0 |
| Mod Framework | BaseLib (NuGet: `Alchyr.Sts2.BaseLib`) |
| Patching | Harmony 2.3.3 |
| Base Game | `sts2.dll` (decompiled reference) |

---

## 🚀 Build & Installation

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Godot 4.5+](https://godotengine.org/) (.NET / C# version)
- *Slay the Spire 2* base game + [BaseLib](https://github.com/Alchyr/BaseLib-StS2) installed

### Step 1: Compile the DLL

```bash
cd "Astro Lupine"
dotnet build -c Release
```

The compiled output will be located at `bin/Release/net9.0/AstroLupine.dll`.

### Step 2: Export the PCK

1. Open the project with Godot 4.5+
2. **Project → Export → Add Windows Desktop Preset**
3. Click **Export PCK/ZIP** and save as `AstroLupine.pck`

### Step 3: Deploy to Game

Copy the following files to your game's Mods directory:

```text
<STS2 Install Directory>/Mods/AstroLupine/
├── AstroLupine.dll
├── AstroLupine.pck
└── AstroLupine.json
```

Launch the game, and you will see the new character on the character selection screen.

---

## 🗺️ Roadmap

- [x] **Phase 1** — Core Framework: Character registration, three register Buffs, base card class.
- [x] **Phase 2** — Initial Test Environment: 12 starter cards running in a combat loop.
- [x] **Phase 3** — Card Pool Expansion: Implementation of the full 70-75 card pool with multiple archetypes.
- [x] **Phase 4** — Art Assets & Polish: Cyberpunk key art, UI updates, combat VFX.

---

## 📁 References

| Directory | Description |
| --------- | ----------- |
| `BaseLib-StS2/` | Source code for the BaseLib framework (git submodule, gitignored) |
| `sts2/` | Decompiled `sts2.dll` source code (API reference only, gitignored) |
| `docs/` | Design documents and tutorials |

---

## 📜 License

This project is a community-made mod for *Slay the Spire 2*, created for educational and entertainment purposes. All game asset copyrights belong to [MegaCrit](https://www.megacrit.com/).

---

<p align="center">
  <em>Using data as a blade, computation as a shield.</em><br>
  <strong>⚡ Compute to Conquer. ⚡</strong>
</p>
