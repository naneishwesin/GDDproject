# Survival Shooter Upgraded

Team: PIX  
Team Member: Nan Ei Shwe Sin Hlaing (6530231)  
Game Type: Single-player survival shooter

Unity-based survival shooter built on Unity’s Survival Shooter tutorial. Educational project from the Academy of Interactive Entertainment (AIE) Seattle.

## Game Overview

Genre:survival shooter (single-player)  
Objective: Survive waves of enemies  
Core Loop: Fight enemies, survive rounds, progress through increasing difficulty

## Features

### Core Gameplay
- **Player System**
  - Health: 100 HP with damage feedback
  - Movement: WASD with mouse look
  - Shooting: Raycast-based with muzzle flash, particles, and audio
  - Input: Unity Input System (keyboard/mouse and gamepad)

- **Enemy System**
  - Types: Zombunny, Zombear, Hellephant
  - AI: NavMesh pathfinding with chase and attack behaviors
  - Spawning: Wave-based with increasing difficulty

### New Upgrades

#### Health & Speed Boost System
- **Health Boost**: +10% per round (configurable)
- **Speed Boost**: +5% per round (configurable)
- **Max Caps**: 300% health, 200% speed (configurable)
- **Auto-Application**: Boosts apply on round start
- **Healing**: Player heals when health increases

#### Enhanced Player Systems
- **Dynamic Max Health**: Adjustable maximum health
- **Heal Method**: Restore health programmatically
- **Health Percentage**: Calculate current health percentage
- **Events**: Health change notifications

#### UI Enhancements
- **ProgressionUI**: Shows boost percentages
- **Animated Notifications**: Boost announcements
- **Color-Coded Icons**: Visual feedback
- **Scaling Display**: Icons scale with boost level

## How It Works

### Round Progression
- **Round 1**: Base stats (100 HP, 6 speed)
- **Round 2**: +10% health, +5% speed
- **Round 3**: +20% health, +10% speed
- **Round 4**: +30% health, +15% speed
- **And so on...**

### Technical Implementation
- **PlayerProgression Component**: Manages stat scaling
- **PlayerHealth Enhancement**: Dynamic health system
- **GameStateManager Integration**: Round-based progression
- **Event System**: UI updates and notifications

## Controls

- **WASD**: Move
- **Mouse**: Look
- **Left Click**: Shoot
- **ESC**: Pause/Menu


## Requirements

- **Unity 2022.3+**
- **Universal Render Pipeline (URP)**
- **Input System Package**
- **TextMesh Pro**

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/naneishwesin/GDDproject.git
   ```

2. Open in Unity 2022.3+
3. Let Unity import assets
4. Open `Assets/SurvivalShooter/Scenes/Menu.unity`
5. Press Play

## Configuration

### Boost Settings
Edit `PlayerProgression.cs` to adjust:
- `healthBoostPerRound`: Health increase per round (default: 0.1 = 10%)
- `speedBoostPerRound`: Speed increase per round (default: 0.05 = 5%)
- `maxHealthMultiplier`: Maximum health cap (default: 3.0 = 300%)
- `maxSpeedMultiplier`: Maximum speed cap (default: 2.0 = 200%)

## Credits

- **Base Game**: Unity Technologies (Survival Shooter Tutorial)
- **Educational Institution**: Academy of Interactive Entertainment (AIE) Seattle
- **Character Assets**: KayKit
- **UI Assets**: KenneyNL
- **Fonts**: Oswald, Rubik, LuckiestGuy

## License

This project is for educational purposes. See individual asset licenses for specific terms.

## Future Enhancements

- Multiple weapon types
- Boss enemies with unique mechanics
- Player progression system
- Power-ups and collectibles
- Multiple difficulty modes
- Achievement system
- Leaderboards

---

**Repository**: [https://github.com/naneishwesin/GDDproject.git](https://github.com/naneishwesin/GDDproject.git)
