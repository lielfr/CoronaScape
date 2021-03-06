# CoronaScape

This is our project in the computer graphics lab, University of Haifa, 2021.

## Gameplay

You are locked inside a third-world hospital floor. Unfortunately, the only way out is by collecting every single key you have, before time runs out.

To make things worse, there's an enemy doctor roaming around the floor, chasing you. Everytime you bump into him, you lose a bit of your life. **Be warned**: If you run out of life, you will lose the game.

You can, however, collect spells which will help you in your journey. Sometimes this might be dangerous - the choice is yours.

Try to finish the game in time, while collecting as much coins and boxes to maximize your score.

Good Luck!

![Game Screenshot #1](Images/Screenshot01.png)
  
![Game Screenshot #2](Images/Screenshot02.png)
  
![Game Screenshot #3](Images/Screenshot03.png)
  
![Game Screenshot #4](Images/Screenshot04.png)

## Time Limit

At the beginning of the game, the player can choose between four different levels of difficulty: Easy, medium, hard and extreme. The choice affects several parameters, as mentioned throughout this README.
![Screen Shot 2022-01-03 at 23 40 34](https://user-images.githubusercontent.com/360928/147983365-da7355d7-e214-47b6-af5c-ebac2ae0fe5c.png)


The time limit for each game is determined by the difficulty, as follows:

* Easy: 20 minutes
* Medium: 15 minutes
* Hard: 10 minutes
* Extreme: 5 minutes

For the exact implementation constants, see the GameplayManager class.

## Procedural Generation

Each time a new game is started, the floor is randomly generated. This is done by combining 2 to 4 primitives, which are rectangles with options for rooms alongside two of their edges.

Here is an example of such a primitive, with all of the possible rooms shown:

![Example of a procedural generation primitive](https://user-images.githubusercontent.com/360928/147848241-907f8b2a-1374-46b6-b2b0-76af612ddbed.png)

Here is the full level generation process:

1. First, a base rectangle is placed on the center of the world, with a random rotation.

2. The remaining rectangles are placed and combined with the base rectangle. To make sure they are connected with the base, their centers are placed alongside the base's diagonals, but relatively far (a fraction of 0.5-1 of the total diagonal distance) from the center, in order to avoid generating anemic shapes.

3. All of the possible room positions (alongside the edges of the original rectangles) are considered. Whenever two or more possible rooms intersect, only one of them is drawn (usually the first one to enter the list, which may vary). All of the rooms that do not intersect with any other room option are drawn to the screen. Finally, all of the rooms are combined into a single mesh.

4. Each room (that has not been removed in the previous step) is populated with a key, potions, coins and boxes. The position of each object is determined by a linear interpolation of the room's coordinates (using random coefficients).

Here are a few examples of generated layouts, viewed from the top:


![Screen Shot 2022-01-03 at 23 52 15](https://user-images.githubusercontent.com/360928/147984358-92296aec-e622-4eef-b836-f92c57d5328c.png)
![Screen Shot 2022-01-03 at 23 53 11](https://user-images.githubusercontent.com/360928/147984452-2505152d-2774-4620-85bf-f98a5500ba09.png)
![Screen Shot 2022-01-03 at 23 54 02](https://user-images.githubusercontent.com/360928/147984515-3103e3d4-3048-4974-989d-f0ea36cfd607.png)



## Score System

There are two ways to get score points:

1. **Coins**: Each coin adds a fixed amount of points, depending on the difficulty:
   - Easy: 15 points
   - Medium: 10 points
   - Hard: 5 points
   - Extreme: 5 points
  
![Coin Screenshot](Images/Coin.png)
  
2. **Boxes**: Each box adds a random score, up to a maximal threshold, which also depends on the difficulty:
   - Easy: Up to 25 points
   - Medium: Up to 15 points
   - Hard: Up to 15 points
   - Extreme: Up to 10 points
  
![Box Screenshot](Images/Box.png)
  
## Keys
  
![Key Screenshot](Images/Key.png)
  
There is one key in each room. If the player manages to collect all of them before time runs out, the game ends and the player wins.

Note that it is possible to win regardless of the score. However, it is better to collect as many items as possible to get the highest score possible.

## Combat, Life and Spells

At the beginning of each game, the player has 100 points of life.

Each time the distance between the player and an enemy is less than the danger radius, a warning message is shown on screen and the enemy chases the player.

The danger radius depends on the difficulty, as follows:

* Easy: 200 pixels
* Medium: 250 pixels
* Hard: 300 pixels
* Extreme: 400 pixels

Whenever the enemy collides with the player, the latter takes a health damage, whose amount is chosen at random, from 0 to the maximal damage, which also depends on the difficulty:

* Easy: Up to 10 points
* Medium: Up to 10 points
* Hard: Up to 15 points
* Extreme: Up to 15 points

When the health reaches 0 points, or time runs out, the player loses the game.

The player can collect three different types of spells during the game:

| Time Spell | Healing Spell | Power Strike |
| ---------- | ------------- | ------------ |
| ![Red Potion Screenshot](Images/RedPotion.png) | ![Green Potion Screenshot](Images/GreenPotion.png) | ![Blue Potion Screenshot](Images/BluePotion.png) |

* **Time Spell (red):**
Adds a specified amount of time to the game. The exact amount depends on the difficulty:
  - Easy: 120 seconds
  - Medium: 60 seconds
  - Hard: 30 seconds
  - Extreme: 10 seconds

* **Healing Spell (green):** Adds a random amount of health to the player, up to a certain threshold. Again, this depends on the difficulty:
  - Easy: Up to 15 points
  - Medium: Up to 10 points
  - Hard: Up to 10 points
  - Extreme: Up to 5 points

* **Power Strike (blue):** Not implemented, but supposed to give the player a fighting chance against enemies

## Running The Game

CoronaScape is built using Unity. At this stage, running the game is only possible inside the editor. You can, however, build it to your selected platform.

Please do not upgrade the Unity version yourself, as this may lead ot inconsistencies.

The game is tested on Windows 11 and macOS 12.1. It should be possible to run on Linux as well (and maybe even on mobile platforms), but we don't test these options.

