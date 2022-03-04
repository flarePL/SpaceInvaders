# SpaceInvaders
Space Invaders - sample project
Project description Create a simplified clone of original Space Invaders game. The task is to recreate simplified gameplay mechanics along with game flow involving additional screens and popups. The project should meet requirements described below. Use built-in Unity features and programming design patterns that will suit you. If something is not specified feel free to decide how to do it.
Gameplay reference https://www.youtube.com/watch?v=MU4psw3ccUI
Scenes and game loop
➢ Main Menu Screen Startup scene. From here you can switch to gameplay. After pressing start game button popup with list of defined enemies waves shows up. After choosing one, gameplay starts with selected wave layout. UI Content:
o Start game button
➢ Gameplay Screen Scene with actual gameplay. After entering this scene simple countdown animation is played (3, 2, 1, Start). After it gameplay starts. At the end of gameplay popup with statistics and main menu button should be displayed. UI Content:
o Killed enemies counter
o Gameplay duration in format that meets requirements: 4m 15s, 3m 09s, 9s, 17s
o Game over popup:
▪ Info about gameplay result (won/defeated)
▪ Killed enemies
▪ Gameplay duration
Gameplay requirements ➢ Enemies waves are defined in configuration assets (e.g. ScriptableObject)
➢ Player can move horizontally and shoot in defined interval
➢ Game ends when player gets hit
➢ Enemy dies after one hit
➢ Bullet is destroyed after hitting something or if it’s out of camera’s view
➢ Enemies and player are shooting each other
o Only one enemy can shoot at a time
o Only enemy on the lowest position in the column can shoot
o Enemies are shooting in random intervals
