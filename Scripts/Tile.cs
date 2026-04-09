using Godot;
using System;

public partial class Tile : Node2D
{
	[Export] private Vector2I position;
	[Export] private Piece selectedPiece;
	[Export] private PackedScene pieceScene;
	[Export] public TimerManager timerManager;

	public Vector2I getPosition()
	{
		return position;
	}

	public void setPiece(PackedScene pScene)
	{
		pieceScene = pScene;
		instiantatePiece();
	}

	public bool canMove(Vector2I NextPosition)
	{
		GD.Print("Hiiiiiiiiiiiii");
		if((selectedPiece is not null) && (NextPosition != position))
			return selectedPiece.Move(NextPosition, position);
		else
		{
			return false;
		}
	}
	
 private void instantiatePiece()
{
if (selectedPiece == null)
{
if (pieceScene == null)
{
GD.Print("pieceScene is null");
return;
}

Node fry = pieceScene.Instantiate();
AddChild(fry);
selectedPiece = fry as Piece;
GD.Print(selectedPiece);
}

	
	public void ClearPiece()
{
if (selectedPiece != null)
{
selectedPiece.QueueFree();
selectedPiece = null;
}
}

	public bool hasPiece()
	{
		if (selectedPiece is null)
		{
			GD.Print("Oh No");
			return false;
		}
		else
		{
			return true;
		}
	}
}
