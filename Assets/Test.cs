using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test
{

    /*
     * AIR RAID
     * 
     Game Controls
     W-S to look up and down
     A-D to roll
     E-Q to move along the Yaw
     Left Shift to increase speed
     Left control to decrease it? I don't really remember.
     

    HJK are the different ability keys
    H - Shoot frontal guns
    J - Drop Bomb
    K - Unassigned

    You have an energy meter, boost and abilities cost energy
    When you run out of energy, you will tailspin until energy replensihes

    Enemies spawn as eggs which drop into the scene
    The eggs will hatch into one of several enemy types, including the "Egg Launcher" which will create more eggs

    The goal of the game is to destroy all the enemies before they overwhelm and destroy the city




    ====================================================================================
                             EEEE  X   X  AAAA  MM   MM                                                                             
                             E      X X   A  A  M M M M                                                                                         
                             EEE     X    AAAA  M  M  M                                                                                 
                             E      X X   A  A  M     M                                                  
                             EEEE  X   X  A  A  M     M                                                  
    ====================================================================================

    The Final examination is the extension, research and fixes of the game Air Raid included

    For the research questions, imagine being asked an interview question, or leaving documentation behind for the next developer to inherit this code. Write enough details to satisfy someone's
    belief that you understand the code. Assume the reader understands programing very well, but doesn't know the project,
    so explain to them so they could easily continue from where you left off, or find the answers they are looking for. (Include class names & functions)
    

    This exam will be impossible without using breakpoints, Debug->Window->Call stack, Ctrl+F, Ctrl+; and Find All references. To see everywhere in the code where a variable or class is used, you can use "Find All References"

    =====Research Questions, 40%, 1.5 hours =====
    0) This code uses Top Down Architecture, and therefore has a class which initializes and updates all the Managers, which class is that?
    1) Describe how the buildings are all added to the building manager. How could we add more buildings to the game?
    2) Explain the process of how the input is read, and ultimatly used by the player
    3) Where are the places that the player's death are called? What actions can cause a player to die?  
    4) Taking a look at the projectile class, I dont see any logic for "if enemy, Enemy.HitByBullet, if building, Building.HitByBullet..." so how are bullets damaging everything?
    5) Where is the code for the eggs hatching and how does it decide which type of enemy to spawn?
    6) What determines the number of eggs, the egg-spitting alien produces?
    7) What are "body parts" in the player and how are they set
    8) Which enemies are considered RootedEnemy types, How does the root system affect the enemy? (Don't need super specific detail about the root system, just the result of how the root system affects the enemy)

    ======Bug Fix, 10%, 30 minutes=====
       
	0) The energy bar for the player is graphically glitched, it is always a full orange bar, it should be filled based on the amount of energy stored. Similar to a health bar in a 2D fighting game.
    Fix it.
   

    =====Balances, 25%, 1.5 hours =====   
    1) Change the rate of fire of the basic machine gun ability so it shoots 10x a second
    2) Change the energy cost of the bomb drop ability to cost 35 energy
    3) At the start of the round, 5 sky-eggs are created in 5 different locations. Make a 6th location for the sky eggs to spawn

   

    =====Feature, 25%, 1.5 hours===
    Ability Feature
    Add a new Ability, Fire a rocket that travels foward slowly and then explodes with a large AOE
    K key to fire
    The ability has a 5 second cooldown. 
    It cost 50 energy to shoot
    It has forward velocity, and ignores gravity
    It moves at 3 m/s + the players speed at launch.  (if player is travelling 10 m/s, and fires it, then it travels 13m/s)
    It explodes with a diameter of 8
    It does 500 dmg.
    For the graphics, the missle can just be a simple capsule with a box tip
    The forward allignment of the missle is in the direction it was shot



    Good luck everyone. Time management is essential. Keep in mind how much points each section is worth. If you are taking longer than the recommended time, do other questions.
    It is not expected you will have the time to get everything done, so be strategic with your time. 








     */

}
