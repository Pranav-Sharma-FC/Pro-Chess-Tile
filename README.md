# ⚠️ ==Warning== ⚠️

If you are seeing this text and are about to submit your project, don't.

The reason you are seeing this text is because you haven't updated the README file for your repository yet. That means there are points on the project rubric that you are giving up. Make the time to update this file before you call your project "done". If you are looking at the README.md file directly instead of in a browser, you can delete everything above the line says "# Project Description".


# Project Description
Pro-Chess-Tile is a fast-paced, competitive, modern spin on the classic game of chess. It takes the basics of chess, and turns it into a chaotic, yet intellectually challenging strategy game. It combines chess and real-time tower defense. Use coins gained from taking pieces or their spawnables to upgrade your pieces offensive and defensive capabilities.

# Development Team
Pranav Sharma - Core Mechanics
Isaac Smith - Tower Defense Mechanics
Averi Noor - Chess Mechanics
Jasrose Kaur - UI and Sound

# Game Play Features
## Towers
Queen
The Queen is the most powerful piece in Chess and in this game. Its spawnables do 9 damage, correlating to its piece value in chess. It acts normally in this game, while also spawning its spawnables in horizontal, vertical, and diagonal directions. Its ability is Pawn Execution, where once it has gotten enough kills and then it will be able to execute a pawn anywhere on the board. The queen has 900 Health.
King
The King is the main objective of the game for both players, whether it be protecting their king or attacking the opposing one. The King has 2000 hitpoints. The king's ability creates decoy kings that can't deal damage and have a tenth of the actual king's hitpoints, but it creates a distraction for the opposing player. This can be costly however, as neither of the players will know which king is the real one.
Bishop
In a normal game of chess, the bishop is going to go diagonally, with an ability where the “bullets”. The bullets/spawnables will explode on contact with anything it hits, dealing some (but smaller) damage to the pieces in a 1 square radius. Its spawnables deal 3 damage every hit.
Knight 
The knight will move as normal chess, except one of the abilities is based off of a stampede, where the knight can deal damage to the pieces in its path as its spawnables goes to its target square. 
Rook
The rook will move as normal per normal chess gameplay. Its ability is to create a 1 square wall in whatever direction the rook is going 
Pawn 
Apart from moving as normal, the pawn has the option to deal damage in a 1 square radius before being turned into mana/coins that the player committing the act will gain. 

## Enemies
The enemies will be the opposing players pieces.

## Additional Features
Spawnables
The spawnables are essentially going to look like a smaller version of its corresponding piece, and will consistently be dealing the same amount of damage as its “parent” piece chess value. These spawnables will be present from the beginning of the game, and any upgrade, with an exception to the bishop, will affect only the parent piece. Spawnables spawn from the opposing side while the game waits for you to move. 
Timer
There will be a basic timer that will be going down for each player as they are making their upgrades and moves. This will work like a regular chess timer. 
Mana
Abilities need Mana to be able to be spawned. Mana is given every 30 seconds of playtime, on a 10 minute chess timer. The Queen's and King’s ability needs 4 mana. The Rooks need 3. The Bishops and Knights need 2. The Pawn’s ability needs 1. A piece can only use an ability once.
Coins & Upgrades
Coins are given when a piece is damaged or killed. Taken pieces give 100 coins, every hit gives 1 coin, while pieces killed by spawnables give 50 coins. Upgrades are offensive and defensive, with offensive upgrades for all pieces including a x20%, x40%, and x60% (relative to start) damage boost, speed increase by x20%, x40%, and x60% (relative to start), and spawn rate x20%, x40%, and x60% (relative to start). These upgrades will be grouped into bundles decided later in playtesting for balance. For Defensive, you can gain defense to take x90%,x80%, and x70% only against one of the pieces. You can also have a x96%, x93%, and x90% defensive boost against every spawnable. The upgrades will be 10, 20, and 30 coins 


# Programming Concepts
## C# and OOP
### Enums
[ENUMS]

## UI
The UI will be mostly drop-down menus for piece upgrades, along with movement options for the selected piece.
### HUD
The HUD will include the timer, piece health, Mana, and Coins.

## Godot
### Custom Signals
[CUSTOM SIGNALS]
