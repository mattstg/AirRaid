					AirRaid README


				2 features added by Sebastien Sohy



--------------------------------------------------------------------------------------------

	-	Rewind

	A three second time warp that makes you travel back in time
	
	When activated, the player will fade to blue and start travelling backwards(Sound activates then).
	It stops when it reaches the position from 3 seconds ago.
	The player's energy, velocity, speed and rotation also get reset to their orignal 3 second ago values.

	Activation key : R
	Cooldown 7 seconds
	Energy Cost 35

	files created/edited for this feature

	-Scripts/Abilities/Ab_Rewind.cs

	-Resources/Sounds/rewindWarp.wav

---------------------------------------------------------------------------------------------

	-	Droppable turret

	The player can now drop an unlimited(for now) ammount of turrets to help fight off attackers

	The turrets AI will allow it to find a target once it lands and start shooting it.
	If the target dies, goes out of range OR out of sight, the turret will scan and target the new closest target.

	The turret also has a "Lifetime timer"  at the end of wich it will self destruct(Coded, but commented out because Untested).


	Activation key : T
	Cooldown 5 seconds
	Energy Cost 20

	files created/edited for this feature

	-Scripts/Abilities/Ab_Turret.cs

	-Scripts/Abilities/TurretBehavior.cs

	-Resources/Sounds/turretDropPing.wav

	-Scripts/Abilities/Projectiles.cs (Edited)
		- Had to add the "Map" layer in the projectile Raycast for ANY collisions to take effect in my scene