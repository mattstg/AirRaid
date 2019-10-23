Feature created.
Defenders.

Spawner script can be included to any building. 

Defender will spawn overtime. They will move as a group and kill ground enemies.

There is 4 different type of defenders with unique stats. Guard, Archer, Support and Leader.

Sounds and animations were added.


Leader of the group use a NavMesh system and the group itself uses Flocking AI.


How to use Using Flocking AI ( Script/Defenders/Flock ) :
Add Flock script to any Object where you want to manage the flocking.
NOTE: FlockAgents are instanciated in Leader. The instanciated prefab needs FlockAgent Script.

Hierarchy:
Filter goes into Behavior. Behavior goes into Composite Object. The Composite goes into the Flock Component.

To Create any Flock object:
Right click in Project, Create, Flock, Behavior or Filter.



Folders: Script/Defenders, Resources/Characters, Resources/Prefabs/Defenders
