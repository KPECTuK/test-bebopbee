Unity3D 5.3.5.p8

  To run the project u shold open the ThreeInLine.sln and build it.
It will build the assebly to  $(SlutionDir)_Unity\Externals.  Unity
project should be runnable then, so you can open it with an Editor.
Im using x86 Editor now, because of some plugins, that im working
with currently, but i beleve its not necessuary to use x86 exectly.

After u'll start the application the game will behave (i beleve) as
described in test topic.

- to focus onto the cell: click over the cell
- to set the piece to the empty cell: double click over the cell focused
- to move one piece: swipe from the cell focused to the naighbor cell

Notes:
- Input implementation is no good and very inplace, but should be
enough, because the channels implementaion is very complex for that
project
- There are no restart or winner screen in game, because it makes fsm
much complex
- Differed rendering was picked because of intending for many lights
for that visual style (or maybe not, i dont understand excectly the
direction of developing)
- The scene is very flexable, because the model asset can be recreated
any time by overwriting the .fbx and set the BoardInputControllerComponent
on the asset root (board node). The naming convenstion is:
	-- piece_# - # is the index of the piece as described in Logic.Board
	calss