# Realm Rush

## Project Structure

- Assets/
|-- Material/ required material for different color
├── Scripts/
│ ├── Player/ # PlayerController
│ ├── Combat/ # Shooter
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
