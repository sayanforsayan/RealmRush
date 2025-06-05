# Realm Rush

## Project Structure

- Assets/
|-- Data / Json file will create here by default, you can change path also
|-- Material/ required material for different color
├── Scripts/
│ ├── Player/ # PlayerController
│ ├── Combat/ # Shooter
| |-- Editor/ # QuestEditorWindow, AdvancedQuestEditorWindow, DynamicQuestJsonCreator
│ ├── Interactions/ # Enemy, Collectible, GoalZone
│ ├── Quests/ # BaseQuest, QuestManager , ( Explore , Fetch , Kill ) SO
│ ├── Events/ # GameEvent + Listener
│ └── UI/ # UIManager
├── ScriptableObjects/
│ ├── Quests/ # Quest SOs (Kill, Fetch, Explore)
│ └── Events/ # GameEvents (AreaReached, EnemyKilledEvent, ItemCollectedEvent )
├── Prefabs/ # Enemies, Items, Zone.
├── Scenes/
│ └── Main.unity
├── UI/
└── README.md

## Quest System
- Uses ScriptableObjects with inheritance for quest types.
- Easily extensible for more quests.

## ScriptableObject Usage
- Defines all quest data for flexibility.

## Notes
- Used AI for guidence
- Used Unity's CharacterController.

# Add Editor ( Update )
## Quest Editor Tool

A custom Unity Editor Window found in `Tools > Quest Creator`. Designed for non-programmers.

### Features

- Create new quests (`Fetch`, `Kill`, `Explore`) with fields like title, description, goal count, and reward
- Load and update existing quest assets
- Auto-instantiates correct ScriptableObject type
- Validates required fields (title, goal count ≥ 1)
- Saves assets to: `Assets/ScriptableObjects/Quests/`
- Inline help messages and confirmation popups

### How to Use

1. Go to `Tools > Quest Creator` and `Advanced Quest Creator`
2. Fill in quest details or load an existing quest
3. Click **Create New Quest** or **Update Quest**
4. Asset appears in project and can be used in QuestManager

# Dynamic Quest JSON Creator (Unity Editor Tool)

This Unity Editor extension allows you to **create multiple quests manually** and export them into a structured `.json` file — directly from the Unity Editor interface.

## Features

- Create multiple quests with fields like title, description, goal count, reward, and type.
- Customizable export path and filename (must be inside the `Assets/` folder).
- Validates required fields before export (title, description, goal count, reward).
- Warns if a file with the same name already exists in the path.
- Auto-handles `.json` extension — just enter the file name, no need to type `.json`.

## Quest Data Format

Each quest includes the following fields:

- `title`: Name of the quest.
- `description`: Details or objective of the quest.
- `goalCount`: How many units or actions to complete.
- `reward`: Reward for completing the quest.
- `questType`: Type of quest (e.g., Fetch, Kill, Explore).

## How to Use

1. Open Unity.
2. Go to `Tools > Dynamic Quest JSON Creator`.
3. Set how many quests to create.
4. Fill in the quest fields.
5. Enter a **filename only** (e.g., `MyQuestData`) — the tool will handle the `.json` extension.
6. Choose or confirm a folder under `Assets/` where the file will be saved.
7. Click **Export to JSON**.

If a file already exists with that name in the target folder, a warning tooltip will notify you.


