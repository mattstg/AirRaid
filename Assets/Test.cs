using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test
{

    /*
     * AIR RAID
     * 
     Game Controls
     WASD to move
     Left Shift to increase speed

    HJKL-YUI are the different ability keys
    H - Shoot
    J - Drop Bomb

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
    belief that you understand the code.

    This exam will be impossible without using breakpoints, Ctrl+F, and Find All references. To see everywhere in the code where a variable or class is used, you can use "Find All References"

    Research Questions
    0) This code uses Top Down Architecture, and therefore has a class which initializes and updates all the Managers, which class is that?
    1) Describe how the buildings are all added to the building manager
    2) Is the input for the player calculated in PlayerController? If yes, where, if not, where and how does player recieve it?
    3) Where are the places that the player's death are called? How can a player die?
    4) What are the two ways (not including the error way) of spawning an enemy egg? (where in code are they called)
    5) When an enemy dies, are they destroyed right away, or put on a stack to be removed later?
    6) Where is the players max energy, max speed, max hp, and all the player variables stored and set?
    7) Draw out the enemy heirarchy, Which enemies inherit from which?
    8) Taking a look at the projectile class, I dont see any logic for "if enemy, Enemy.HitByBullet, if building, Building.HitByBullet..." so how are bullets damaging everything?
    9) The enemy creature known as "AATurret" shoots a projectile at players. What is the name of that projectile (That class name that does the projectile logic)

    Bug Fix
    0)
    There is a system of roots that grows out from certain types of enemies, such as the spitter or egg. These roots branch and spread across the land, the larger the root system, the more
    powerful the creatures become, This is all controled in the RootSystem class. Although it is a neat feature, unfortunatly at this moment it is very unperformant and lacks some features we want.
    	
    You have permission from the developer who created it to temporarily disable the system. Find a safe way using a static bool in an intelligent place to disable the system. Have it that when
    the bool is set to True, the system is disabled and no roots are created for any enemy. Try to be as least intrusive to the code as possible.
    
	1)
	The energy bar is graphically glitched, it is always a full orange bar, it should be filled based on the amount of energy stored. Similar to a health bar in a 2D fighting game.
   
    Balances
    0) An enemy's energy has an affect on the size. Increase the ratio of size to energy by 33%    (for example if value was 1, set to 1.33)
    1) Change the rate of fire of the basic machine gun ability to .1f   (so it shoots 10x a second)
    2) Change the energy cost of the bomb drop ability to 35


    Features
    Add only ONE of the following features of your choice

    UI Feature
    On the HUD,there is a white square, it represents the abilities cooldown, however it is not active or completed. Build a system that shows the cooldown of your abilities with a League Of Legends
    or RPG style cooldown circle. For the image, you can just show the key that corresponds to the ability or put a string name

    Ability Feature
    Add a new Ability, Fire a rocket that travels foward slowly and then explodes with a large AOE

    Enemy Feature
    Add a new type of enemy, a circle blimp that floats into the sky until it reaches a specific height, and then wanders randomly. 
    After a long random peroid of time (30~45 seconds) it will split, creating another one of itself.
    If it crashes into the player or a building, it should kill them











     */

}
