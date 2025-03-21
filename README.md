### README

# Take-Home Test: Multi-State Character + Debug Overlay

This project implements the game concept outlined in Take-Home Test: Multi-State Character + Debug Overlay by BUFFALO BUFFALO.
The primary objective is to create a small prototype for an isometric 3D game using Unity.

## Key Features
* Character with multiple states 
  * States are manager by a State Machine that references `ScriptableObjects`
    * The character employs a State Machine to manage and notify its states. Designers can conveniently add more states and modify them throughout the Debug overlay. Each state simply refers to a ScriptableObject that holds all the necessary data to configure the character.
* Debug overlay for displaying information and executing debug commands.
* UI with Model-View-Controller architecture
  * Game is decoupled from UI
* Projectiles utilizing ObjectPool

## Design Decisions
To facilitate the character’s reuse, I divided the player script into separate components that can be used independently. Additionally, the enemy prefab variant can be customized with minimal modifications. A crucial aspect of this implementation is the Interface IPlayerInputProcess, which enables the replacement of the traditional player control with an AIPlayerInputProcess. The PlayerInputProcess then converts mouse coordinates into appropriate 3D space. Most components utilize UnityEvents to expose events to the Inspector, while for events that are not required (since they will only be used in code), I opted for Actions due to their improved performance.

The protagonist fires projectiles, which utilize `UnityEngine.Pool.ObjectPool<T>` to prevent memory accumulation during garbage collection. A spread shot is also available.

## Architecture and Patterns used
For the Dummy AI, I employed the pattern Blackboard, a widely used technique in AI for gaming. This allows AI Agents to access required information in a central location. I created a rudimentary AISpawner for debugging and testing purposes.

For the User Interface (UI), I utilized the Model-View-Controller (MVC) architecture. The main character serves as the model, while Controller components within the Panel facilitate communication between the model and the view. I refrained from using the Observer Pattern due to limited time constraints, although it is a valid approach to decouple the UI from the models.

## Furure Improvements
For future enhancements, the UI could benefit from the use of prefabs and scripting to automate the creation of components that are already configured for time-saving purposes. Additionally, a Window Manager could be implemented to manage the creation and navigation of windows. The UI also requires localization support if this code were to be deployed in production.

Furthermore, the Player could adopt a more modular system to handle various types of locomotion or gameplay, providing Designers with even greater control. 
