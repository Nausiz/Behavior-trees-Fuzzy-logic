# Behavior-trees-Fuzzy-logic
 The project was created for the purpose of the master's thesis on the comparison of behavior trees with fuzzy logic

## Introduction
The goal of the project was to create a system that would allow for intelligent decision-making by an NPC in the game. To achieve this goal, it was decided to implement two different approaches: behavior tree and fuzzy logic.

## Overview
The project is based on the implementation of behavior trees and fuzzy logic for NPCs and collecting data from their duels. In the case of fuzzy logic, this algorithm is used to make decisions based on imprecise input data. Behavior trees, on the other hand, allow you to define a hierarchy of actions that NPCs will perform depending on the situation.
The project also includes three scenarios where NPCs will fight each other. These scenarios include duels between behavior trees, between behavior trees and fuzzy logic, and between fuzzy logic and fuzzy logic. In each of these scenarios, NPCs (marked in red and blue) fight on a specially prepared map (Fig. 1), which contains obstacles in the form of walls and first-aid kits (shown as green orbs) and power-ups that increase attack power (shown as yellow orbs) appearing in random places, but so that two first aid kits and two buffs always appear on the side of each NPC. Respawn points after the end of the round are always the same. 

*Figure 1. Map in the prepared project.*
![alt text](https://github.com/Nausiz/Behavior-trees-Fuzzy-logic/blob/main/Decision%20Trees/Img/screenshot1.png)

The presented graphic also shows the data that is collected during duels:
• Round - the number of completed rounds,
• Time - total time of all rounds,
• Wins - the number of won rounds of a given NPC,
• Buffs - the number of buffs collected for a given NPC over all rounds,
• Healing - the number of health kits collected by a given NPC over all rounds.
The project also uses the NPC class, which contains all the necessary parameters and methods for NPCs to move, shoot and make decisions. Both algorithms (fuzzy logic and behavior trees) inherit from the NPC class, which allows you to compare their results.
All of these design elements have been implemented to collect data on duels between NPCs. These data will then be used in the experimental part of the project.

## Behavior tree implementation
Behavior trees are one of the design concepts that have been implemented for NPCs. In order to present this idea, pseudocode was presented (fig. 2), which allows creating a hierarchy of actions for NPCs used for the purposes of the project.

*Figure 2. Pseudocode of the behavior tree used in the project.*
![alt text](https://github.com/Nausiz/Behavior-trees-Fuzzy-logic/blob/main/Decision%20Trees/Img/screenshot2.png)

The "CanUseHeal" condition used returns true if there are still any health kits on the map, the NPC's current health is less than or equal to the limit specified in the parameters (50 in this case), and the distance from the enemy is greater than or equal to 5. The "CanUseGunPower" condition returns true when there are still some buffs on the map and the distance to buff is shorter than to the enemy. If neither of the above two conditions are met, the NPC will go on the attack. The "UseHeal" and "UseGunPower" actions involve reaching the target (healthkit or buff) and using it. The "Attack" action also includes reaching an enemy within a certain distance, but also checks if the enemy is visible from the NPC's perspective, and if so, starts shooting.
The behavior tree for NPCs has been defined in a graphical form (Fig. 3), where individual nodes indicate specific actions that NPCs can perform. The graphical representation of the behavior tree is very intuitive and allows you to quickly understand the hierarchy of actions for NPCs.

*Figure 3. Visualization of the behavior tree used in the project.*
![alt text](https://github.com/Nausiz/Behavior-trees-Fuzzy-logic/blob/main/Decision%20Trees/Img/screenshot3.png)

At the very beginning, using the selector, we check the first conditional node, which is "CanUseHeal", if it returns true, it will go to the "UseHeal" sequence, if it returns false, the selector will go to the next conditional node, "CanUseGunPower", and the same as before - in the case of true the node will go to the UseGunPower sequence, when false the selector will go to the attack sequence.
In a decision tree implementation, you first define the tree structure and specify the conditions under which subsequent decisions should be made. Then, at each step of the game, the tree is reviewed and decisions are made based on the current state of the game and the results of the condition analysis. Decision tree results can be returned as specific actions taken by a character in the game, such as attacking. Thanks to this, decision trees allow for the automation of character control in the game, which increases its credibility and realism.

## Behavior tree implementation
The second approach to implementing artificial intelligence for NPCs in the project was fuzzy logic. It allows decisions to be made based on continuous values such as distance, health, etc., which are represented by membership functions. For this design, fuzzy logic was used to choose the best action for the NPC to perform: use a health pack, use a buff, or attack an enemy. The pseudocode of the implemented solution is presented below (Fig. 4).

*Figure 4. Fuzzy logic pseudocode used in the project.*
![alt text](https://github.com/Nausiz/Behavior-trees-Fuzzy-logic/blob/main/Decision%20Trees/Img/screenshot4.png)

In the presented code, the values of belonging [0-1] to the data sets: "CalculateCanUseGunPowerValue" and "CalculateCanUseHealValue" are compared. To proceed to the action, the set must also have a value greater than the "ATTACK_THRESHOLD" parameter, which in this case is 0.2. The method of calculating the value of belonging to the set responsible for using the first aid kit takes into account such factors as the NPC's current health and distance from the enemy. Both factors have two thresholds, their values given in the project are given in parentheses:
• Health status: LOW_HEALTH_THRESHOLD (25), MID_HEALTH_THRESHOLD (50);
• Distance to enemy: CLOSE_CAN_USE_HEAL_DISTANCE_THRESHOLD (5), FAR_CAN_USE_HEAL_DISTANCE_THRESHOLD (10).
The graphical visualization responsible for this part of the fuzzy logic is shown in Figure 5

*Figure 5. Visualization of the value of belonging to the set responsible for using the first aid kit.*
![alt text](https://github.com/Nausiz/Behavior-trees-Fuzzy-logic/blob/main/Decision%20Trees/Img/screenshot5.png)

For example, if the health value of the NPC is below the LOW_HEALTH_THRESHOLD value, the affiliation function will return the value 1. If the health value is 35, the returned value will already be 0.6, and if the NPC health value is greater than MID_HEALTH_THRESHOLD, it will return 0. The situation is the opposite when we take into account the distance to the enemy - if this distance is 4, then the affiliation value will be equal to 0, when the distance increases to 9, the value will change to 0.8, and when the distance increases again, it will be 15 and thus is greater than FAR_CAN_USE_HEAL_DISTANCE_THRESHOLD, the affiliation value will be equal to 1. Thanks to such rules, the NPC will sooner give up using the first aid kit and focus on attacking if the enemy is practically at his side.
In the case of the method that calculates the value of belonging to the set responsible for using the buff, the distance to the enemy and the distance to the nearest buff are taken into account. When determining the thresholds for these distances, the same parameters were used: CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD (15), FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD (20). A graphical way of presenting the value of belonging to this set is shown in Figure 6

*Figure 6. Visualization of the value of belonging to the set responsible for the use of reinforcement.*
![alt text](https://github.com/Nausiz/Behavior-trees-Fuzzy-logic/blob/main/Decision%20Trees/Img/screenshot6.png)

As in the previous function - if the distance to the reinforcement is below the indicated value CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD, the value of 1 will be returned, while when this distance is 18, then the value of belonging to the set will be equal to 0.4, when this distance increases to 24, the value of belonging will be 0. When calculating the degree of affiliation at the distance from the enemy, the situation is reversed again. Values below CLOSE_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD will be 0, an example distance of 16 will return 0.2 and the rest of the value greater than FAR_CAN_USE_GUNPOWER_DISTANCE_THRESHOLD will return 1.
When several factors are taken into account (e.g. health status and distance to the enemy), their affiliation values are aggregated according to the rules of fuzzy logic, and their minimum is returned as the value of the "CalculateCanUseHealValue" and "CalculateCanUseGunPowerValue" functions.
The previously mentioned implementation of fuzzy logic in the project is one of the examples of using this method in game programming. Thanks to the use of concepts from the fuzzy set theory, it is possible to introduce greater flexibility and precision to decision-making by the algorithms that control the characters in the game. In the implementation shown above, the values that determine the use of a health kit or the use of a buff are calculated based on various factors. Then, based on these values, a decision is made which ability will be used at any given time.
