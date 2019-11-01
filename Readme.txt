#########################
	AIR RAID
#########################


We are adding two new features in the game
1) spawning a turret.
2) Teleporting the airplane back.

In the first ability a player can drop a turret
by Pressing K button, which shoots nearest target within range.
Turret also rotates in both the directions ( Horizontaly or vertically )
to find the enemy.


In the Second Ability a player can teleport back to the 
position to where they were 3 seconds ago while completely following
the exact motion.
This ability can be used while HOLDING L button, and uses the ability
power quickly.
The player becomes blue, transparent and triggers a sound while this ability is in use

Below are the locations of the files which we have added:

For turret
Assets\Scripts\Projectiles\Turret.cs

For Player Rewind
Assets\Scripts\Ability\Ab_Rewind.cs

3D Model used for Turret
Repos\AirRaid\AirRaid\Assets\Turret.blend