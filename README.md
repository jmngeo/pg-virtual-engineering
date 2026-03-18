# VirtEng — Serious Game for Model-Based Systems Engineering

A multiplayer serious game developed in Unity to build awareness of **Model-Based Systems Engineering (MBSE)** principles. Players take on engineering roles, apply SE methods across multiple phases, and make investment decisions — all in a collaborative, networked environment.

Developed as a Master's Project at **Fraunhofer IEM / Paderborn University, Germany**
*(April 2022 – April 2023)*

---

## Overview

VirtEng is a role-based business simulation game where players:

- Select engineering roles (Systems Engineer, Software Engineer, etc.)
- Work through **4 phases**, each covering different aspects of MBSE
- Apply **9 SE methods** (Stakeholder Analysis, Environment Model, Functions Hierarchy, Logical Architecture, FMEA, and more)
- Make investment decisions that affect project outcomes
- Compete and collaborate in real-time over a network

The game is designed for educational use — to give engineering students and professionals hands-on intuition for MBSE workflows in a low-stakes, engaging environment.

---

## Technologies

| Category | Technology |
|---|---|
| Game Engine | Unity 2021.3.2f1 |
| Language | C#, PHP |
| Networking | Photon PUN 2 (Photon Unity Networking) |
| Database | MySQL (via XAMPP) |
| Local Server | XAMPP (Apache + MySQL) |
| API Testing | Postman |

---

## Project Structure

```
PG VirtEng/
├── Assets/
│   ├── Scripts/              # C# game logic
│   │   ├── GameManager.cs            # Core game flow and role management
│   │   ├── NetworkManager.cs         # Photon network setup
│   │   ├── InvestmentManager.cs      # Investment phase mechanics
│   │   ├── GlobalVariables.cs        # Shared state and utilities
│   │   ├── DBScripts/                # PHP backend communication
│   │   │   ├── ConnectToDB.cs        # Player registration
│   │   │   ├── InvestmentDB.cs       # Investment data
│   │   │   ├── GameEndReportDB.cs    # Final results
│   │   │   └── PhaseEndEventDB.cs    # Phase event logging
│   │   └── [NetCode] LobbyUI/        # Lobby and room management UI
│   ├── Scenes/               # Unity scenes
│   │   ├── Menu.unity
│   │   ├── GameIntroduction.unity
│   │   ├── RoleSelectionScreen.unity
│   │   ├── Phase 1/ to Phase 4/      # 5 scenes per phase
│   │   └── GameEndReport.unity
│   ├── Prefabs/              # Reusable game objects
│   ├── Resources/            # Runtime-loaded assets
│   ├── Sprites/              # UI and game artwork
│   └── Photon/               # Photon PUN 2 SDK
├── Packages/                 # Unity package manifest
└── ProjectSettings/          # Unity project configuration
```

---

## Game Flow

```
Menu
 └── Game Introduction
      └── Role Selection
           └── Phase 1
           │    ├── Introduction
           │    ├── Methods Introduction
           │    ├── Developing Methods
           │    ├── Investments
           │    └── Phase End Event
           ├── Phase 2  (same structure)
           ├── Phase 3  (same structure)
           ├── Phase 4  (same structure)
           └── Game End Report
```

---

## SE Methods Tracked

The game tracks player engagement with 9 core MBSE methods:

1. Stakeholder Analyses
2. Environment Model
3. Applications Scenario
4. Functions Hierarchy
5. Activity Diagram
6. Morphological Box
7. Utility Analysis
8. Logical Architecture
9. FMEA

---

## Setup & Running Locally

### Prerequisites

- [Unity Hub](https://unity.com/download) with **Unity 2021.3.2f1**
- [XAMPP](https://www.apachefriends.org/) (Apache + MySQL)
- [Photon PUN 2](https://www.photonengine.com/pun) account and App ID
- [Postman](https://www.postman.com/) (optional, for API testing)

### 1. Clone the Repository

```bash
git clone https://github.com/<your-username>/VirtEng.git
cd VirtEng
```

### 2. Open in Unity

- Open **Unity Hub**
- Click **Add** and select the cloned project folder
- Open with Unity **2021.3.2f1** (install via Unity Hub if not available)

### 3. Configure Photon

- Create a free account at [photonengine.com](https://www.photonengine.com/)
- Create a new **PUN** application and copy the **App ID**
- In Unity: `Window > Photon Unity Networking > PUN Wizard`
- Paste your App ID and click **Setup Project**

### 4. Configure the Database Backend

- Install and start **XAMPP** (start Apache and MySQL)
- Open **phpMyAdmin** at `http://localhost/phpmyadmin`
- Create a database and import the provided SQL schema (if included)
- Place the PHP scripts in your XAMPP `htdocs` folder
- Update the server URL in `ConnectToDB.cs` if needed:
  ```csharp
  // Default local URL
  string url = "http://localhost/<your-php-script>.php";
  ```

### 5. Run the Game

- Open the `Menu` scene in Unity (`Assets/Scenes/Menu.unity`)
- Press **Play** in the Unity Editor, or build the project via `File > Build Settings`

---

## Multiplayer

The game uses **Photon PUN 2** for real-time multiplayer:

- Players create or join rooms via the lobby
- Roles are assigned in the Role Selection screen
- Game state is synchronized across all connected clients
- Supports multiple simultaneous players with role-based gameplay

---

## Database Integration

Player data, investment decisions, phase events, and final reports are persisted via HTTP POST requests to a **PHP/MySQL** backend:

| Script | Purpose |
|---|---|
| `ConnectToDB.cs` | Player registration and authentication |
| `InvestmentDB.cs` | Investment phase data storage |
| `PhaseEndEventDB.cs` | Phase transition event logging |
| `GameEndReportDB.cs` | Final game results |

---

## License

This project was developed as part of a Master's thesis at Paderborn University in collaboration with Fraunhofer IEM. Please contact the author before reuse or distribution.
