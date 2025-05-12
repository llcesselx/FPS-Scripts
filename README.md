# 🎯 Unity First-Person Shooter Project

Welcome to the First-Person Shooter (FPS) game project built with Unity! This project is a basic FPS prototype that includes player movement, enemy interactions, shooting mechanics, animations, key inventory, level transitions, and a countdown timer.

---

## 🕹️ Features

- **Player Controller**: Smooth walking, sprinting, jumping, crouching, prone, and stance transitions.
- **Camera Control**: Mouse-driven view with Y-axis clamping and sensitivity adjustment.
- **Shooting Mechanic**: Fire bullets with muzzle flash, sound, and impact on enemies.
- **Weapon System**: Weapon sway, aiming down sights, and animation triggers for jumping/falling.
- **Enemies**: Simple health-based enemies that die when shot.
- **Key Inventory**: Collectable keys that update a UI counter.
- **Timer System**: Countdown to trigger Game Over and automatic level restart.
- **Level System**: Scene transitions triggered by player actions.
- **UI Feedback**: Displays time, keys collected, and end screens.

---

## 📁 Project Structure

| Script                 | Description                                      |
|------------------------|--------------------------------------------------|
| `scr_CharacterController.cs` | Handles player input, movement, gravity, stance, and shooting. |
| `scr_WeaponController.cs`   | Manages weapon sway, animations, and aiming mechanics. |
| `scr_Bullet.cs`             | Controls bullet lifetime, movement, and collision with enemies. |
| `scr_Enemy.cs`              | Simple enemy logic with health and death. |
| `scr_Key.cs`                | Key collectible with trigger-based pickup. |
| `scr_KeyInventory.cs`       | Tracks number of keys and updates UI text. |
| `scr_Timer.cs`              | Handles a countdown timer and Game Over condition. |
| `scr_EndOfLevel.cs`         | Transitions to the next level when the player enters a trigger. |
| `scr_EndScreen.cs`          | Displays end screen UI and loads the next level. |
| `scr_Models.cs`             | Stores player and weapon settings as serializable models. |

---

## 🚀 Getting Started

### Prerequisites

- Unity 2021 or newer
- A PC or Mac with basic game development tools
- *(Optional)* A game controller for input testing

### How to Play

1. **Run the game in Unity.**
2. Use `WASD` to move, `Mouse` to aim.
3. `Left Click` to fire, `Right Click` to aim.
4. Press `Space` to jump, `Left Ctrl` to crouch, and `Z` to go prone.
5. Collect keys, shoot enemies, and reach the level end.

---

## 🎮 Controls

| Action   | Key                |
|----------|-------------------|
| Move     | `WASD`             |
| Jump     | `Space`            |
| Crouch   | `Left Ctrl`        |
| Prone    | `Z`                |
| Sprint   | `Shift`            |
| Fire     | `Left Mouse Button`|
| Aim      | `Right Mouse Button`|

---

## ⚙️ Customization

All settings such as movement speed, gravity, bullet lifetime, and sway effects can be tweaked in the **Inspector** using the `PlayerSettingsModel` and `WeaponSettingsModel`.

---

## 📸 Screenshots

> *(Add gameplay screenshots here once available)*

---

## 🛠️ Known Issues / TODOs

- Enemies do not currently pursue or attack the player.
- No health system or UI for player status.
- Add scoring system or UI feedback on kills.
- Improve weapon recoil and animation transitions.
- Add game menu or pause system.

---

## 📜 License

This project is for educational and portfolio use. Feel free to fork, modify, and expand!
