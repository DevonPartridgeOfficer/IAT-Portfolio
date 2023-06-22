
-----

<div align="center">
  <img src="https://user-images.githubusercontent.com/90590089/222048453-5768da70-cc84-4949-895c-fa07f21d1807.gif"/>
  <h1>Portfolio</h1>
  <p>ID630151: Introduction to Algorithmic Problem Solving<br>Devon Partridge-Officer<br>Student ID: 1000103794</p>
</div>

-----

## Information
This repository contains code that I have worked on as a part of my Bachelors of Information Technology Course. Any external sources of information/content will be referenced at the end of this ReadMe.

There are four games that have been developed using C# and Unity.
| Module | Title                           | Game               | Lecture Notes | Assessment Tasks  | Advanced Tasks |
|--------|---------------------------------|--------------------|---------------|-------------------|----------------|
| 01     | Introduction to Unity Scripting | [Sheep Saving](https://github.com/DevonPartridgeOfficer/IAT-Portfolio/tree/main/Game_1_Sheep_Saving)       | :heavy_check_mark: :heavy_check_mark: :heavy_check_mark: :heavy_check_mark: | :heavy_check_mark: :heavy_check_mark: :heavy_check_mark: | :heavy_check_mark: :heavy_check_mark: :heavy_check_mark: | 
| 02     | Game Mechanics                  | [Tower Defense](https://github.com/DevonPartridgeOfficer/IAT-Portfolio/tree/main/Game_2_Tower_Defence)      | :heavy_check_mark: :heavy_check_mark: :heavy_check_mark: | :heavy_check_mark: | &cross; &cross; |
| 03     | Maze Generation                 | [3D Dungeon Crawler](https://github.com/DevonPartridgeOfficer/IAT-Portfolio/tree/main/Game_3_3D_Dungeon_Crawler) | :heavy_check_mark: :heavy_check_mark: :heavy_check_mark: | :heavy_check_mark: :heavy_check_mark: | &cross; &cross; |
| 04     | AI Strategy                     | [Chess](https://github.com/DevonPartridgeOfficer/IAT-Portfolio/tree/main/Game_4_Chess) | :heavy_check_mark: :heavy_check_mark: | :heavy_check_mark: :heavy_check_mark: | &cross; &cross; |

## Game Notes
**Game 01 - Sheep Saving**

For the advanced tasks I have done the following:
1. Increase the sheep movement speed over time to increase the difficulty
   - Sheep will speed up individually rather than as a group. This means that once the sheep hits the bridge it will speed forwards, as well as shooting off the cliff at the end. The speed is changed in the Sheep.cs script rather than in SheepSpawner.cs so that I could get this effect. I found with the increased spawn rate in task 2 it sufficently increased the difficulty.
2. Make the sheep spawn faster over time to increase the difficulty
   - Time will get shorter after every 10 sheep spawned. Sheep will spawn faster and faster until they reach a threshold for a minimum possible spawn time (0.5s). This was to reduce the spawn time getting increasingly smaller and smaller, potentially spawning hundreds of sheep at a time.
3. Implement a high score system that persists between scenes
   - No highscore is shown on the titlescreen to keep the UI clear. During gameplay the highscore will update dynamically as the player exceeds the previous highscore. A second check is down at the end of the game to update the score for the gameover screen if necessary

**Game 02 - Tower Defense**
1. Enemy 2 not implemented
2. Only the one enemy in later waves, didnt implement multiple enemy types in later waves

**Game 03 - 3D Dungeon Crawler**

Game only shows 'You Win' in console to indicate game end
1. No logic added for when enemy reaches player
2. No pathfinding feature

**Game 04 - Chess**
1. Tic Tac Toe project not created, only AI chess game

## References
### ReadMe
Tromeur, J. (2022, August 22). Cartoon 3D World: Header Image. Pixabay. https://pixabay.com/gifs/cartoon-3d-world-rockets-earth-480/

### Code and Lecture Notes
Moskal, A., & Orr, G. (n.d.). Intro to Algorithmic Problem Solving. Github. Retrieved February 20, 2023, from https://github.com/otago-polytechnic-bit-courses/ID623001-introduction-to-algorithmic-problem-solving
