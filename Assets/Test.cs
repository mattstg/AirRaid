﻿using System.Collections;
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
    so explain to them so they could easily continue from where you left off, or find the answers they are looking for. 
    

    This exam will be impossible without using breakpoints, Debug->Window->Call stack, Ctrl+F, Ctrl+; and Find All references. To see everywhere in the code where a variable or class is used, you can use "Find All References"

    =====Research Questions, 30%, 40 minutes =====
    0) This code uses Top Down Architecture, and therefore has a class which initializes and updates all the Managers, which class is that?
    1) Describe how the buildings are all added to the building manager
    2) Is the polling of the input from the hardware (Ex: Input.Get or equivalant call) for the player calculated in PlayerController? If yes, where, if not, where and how does player recieve it?
    3) Where are the places that the player's death are called? How can a player die?  
    4) Where is the players max energy, max speed, max hp, and all the player variables stored and set?
    6) Taking a look at the projectile class, I dont see any logic for "if enemy, Enemy.HitByBullet, if building, Building.HitByBullet..." so how are bullets damaging everything?
    

    ======Bug Fix, 10%, 20 minutes=====
       
	0)
	The energy bar for the player is graphically glitched, it is always a full orange bar, it should be filled based on the amount of energy stored. Similar to a health bar in a 2D fighting game.
    Fix it.
   


    =====Balances, 30%, 1.5 hours =====   
    1) Change the rate of fire of the basic machine gun ability to .1f   (so it shoots 10x a second)
    2) Change the energy cost of the bomb drop ability to 35
    3) At the start of the round, 5 sky eggs are created in 5 different locations. Make a 6th location for the sky eggs to spawn

   

    =====Feature, 30%, 1.5 hours===
    Ability Feature
    Add a new Ability, Fire a rocket that travels foward slowly and then explodes with a large AOE












     */

}
