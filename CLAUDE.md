# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Codename-Noir** is a 2D top-down noir shooter built in Unity 6 (6000.4.2f1). The player navigates a tilemap-based world, fights enemies with a multi-weapon system, and progresses through wave-based combat.

- **Scenes:** StartMenu → SampleScene (gameplay) → GameOver
- **Language:** C# with Unity's component-based architecture
- **Rendering:** 2D sprite-based with TextMesh Pro for UI

## Development Commands

This is a Unity project — there is no CLI build or test runner. All development happens inside the Unity Editor. Use Unity 6000.4.2f1 to open the project.

- **Play/Test:** Open `Assets/Scenes/SampleScene.unity` and press Play in the Editor
- **Run tests:** Window → General → Test Runner (Unity Test Framework is installed)

## Code Architecture

All game scripts live under `Assets/Scripts/` organized into four folders:

### PlayerScripts/
- **PlayerMovement.cs** — WASD movement, mouse-aimed firepoint, dash mechanic (Shift key, 0.5s cooldown, 0.2s iframes via `Physics2D.IgnoreLayerCollision` on layers 6+8)
- **Health.cs** — Player health with a static `Action OnPlayerDamaged` event. Death sets `isDead = true` and loads the GameOver scene
- **WeaponShooting.cs** — Bridges input to weapons via two static events: `shootInput` (LMB) and `reloadInput` (R key). Nothing fires unless subscribed
- **WeaponSwitching.cs** — Mouse scroll cycles active weapon children; one active at a time
- **CameraController.cs** — `Vector3.SmoothDamp` follow with configurable offset
- **HealthHeartBar.cs / HealthHeart.cs** — Subscribes to `Health.OnPlayerDamaged`; redraws a heart-based HP bar (1 heart = 2 HP, half-heart supported)

### WeaponScripts/
- **Gun.cs** — Subscribes to `WeaponShooting.shootInput` and `reloadInput`. Pulls bullets from the `ObjectPool` singleton. Fire rate formula: `timeSinceLastShot > 1f / (fireRate / 60f)`. Ammo and reload state live in the ScriptableObject
- **WeaponScriptableObject.cs** — Per-weapon data: damage, bulletForce, fireRate, magSize, reloadTime, currentAmmo, reloading flag

### EnemyScript/
- **AIChase.cs** — Finds player via `GameObject.Find("Noir")`. Uses `maxChaseDist`/`minChaseDist` for chase range; sets `isWalking` animation bool
- **EnemyHealth.cs** — Destroys GameObject on death; hardcoded 20 damage per bullet hit
- **EnemyWeaponShoot.cs** — Timer-based firing; reads `AIChase.distance` to only shoot within range; checks `hp.isDead` before shooting. Enemy bullets are **directly instantiated** (not pooled)
- **EnemyBullet.cs** — Destroys on any non-bullet collision; ignores layers 7, 8, 9 against layer 8
- **EnemyScriptableObject.cs** — Per-enemy data: maxHealth, speed, maxChaseDist, minChaseDist, fireRate, bulletForce

### GameManagement/
- **ObjectPool.cs** — Singleton (`ObjectPool.instance`) for player bullets only. Pre-allocates a pool; expands if `canExpand` is true. Clients call `GetPooledObject()` then `SetActive(true)`
- **SpawnManager.cs** — Continuous spawner: waits 2s between spawns, caps at `MaxEnemy` (default 10). Counts live enemies via `FindObjectsOfType<EnemyWeaponShoot>()`
- **WaveSpawner.cs** — Alternative wave-based spawner with cost-budget system (`waveValue = Mathf.Max(currWave, 1) * 30`). Both systems can coexist in a scene — only enable one spawner at a time
- **StartMenu.cs / GameOver.cs** — Scene navigation via `SceneManager.LoadScene()`
- **CursorManager.cs** — Applies a custom cursor texture at startup

## Key Patterns & Conventions

**Static event decoupling:** `WeaponShooting` publishes `static Action` events; `Gun` subscribes in `Start()` and unsubscribes in `OnDestroy()`. Follow this pattern for input→system communication.

**ScriptableObjects for config:** Weapons and enemies use SOs (`WeaponScriptableObject`, `EnemyScriptableObject`) for stats. Assign them in the Inspector; don't hardcode values in scripts.

**Object pooling (player only):** Player bullets use `ObjectPool`. Enemy bullets use `Instantiate`. If adding new projectile types, extend the pool before using direct instantiation.

**Collision layers:**
| Layer | Purpose |
|-------|---------|
| 6 | Player |
| 7 | Player bullets |
| 8 | Enemy |
| 9 | Enemy bullets |

Layer collision masks are set at runtime in `Awake()` via `Physics2D.IgnoreLayerCollision()`.

**Player lookup:** Enemies find the player with `GameObject.Find("Noir")`. The player GameObject must keep the name `"Noir"`.

## Known Issues to Be Aware Of

- `PlayerMovement.cs` logs the aim angle every frame — remove `Debug.Log` calls before profiling
- Both `SpawnManager` and `WaveSpawner` exist; having both active in a scene will cause double-spawning
- Enemy bullet damage is hardcoded to 20 in `EnemyHealth.cs` regardless of the enemy's ScriptableObject
- `com.unity.ai.navigation` (NavMesh) is installed but unused — enemies currently use direct vector steering
