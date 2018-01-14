##### Tools
Unity3D 5.3.5.p8
Visual Studio 2015

You shold open the ThreeInLine.sln and build it, using Visual Studio, prior to be able to run the project. It will build the assembly to $(SolutionDir)_Unity\Externals. Unity project should be runnable then, so you will be able to open it with the Editor.

##### The game will behave like described below:
- to focus onto the cell: one should click over the cell
- to set the piece to the empty cell: one should double click over the cell, been focused onto
- to move one piece: one should swipe from the cell, been focused onto, to the naighboring cell

##### Notes:
- Input implementation is no good and very inplace, but it should be
enough for this demo, because the channels implementaion is very complex for that
project
- There are no restart or winner screen in the game, because it makes fsm
much more complex
- Differed rendering was picked as a solution because of intending for many lights
for that visual style (or maybe not, i dont understand excectly the
direction of developing)
- The scene is very flexable. The asset model can be recreated any time by overwriting the .fbx. One must setup the BoardInputControllerComponent on the root (board node). 

##### The naming convenstion is:
- piece_# - # is the index of the piece as described in Logic.Board calss