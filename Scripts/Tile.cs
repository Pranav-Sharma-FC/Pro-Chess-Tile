using Godot;
using System;

public partial class Tile : Node2D
{
	[Export] private Vector2I position;
	[Export] private Piece selectedPiece;
	[Export] private PackedScene pieceScene;

	public Vector2I getPosition()
	{
		return position;
	}

	public void setPiece(PackedScene pScene)
	{
		pieceScene = pScene;
		instiantatePiece();
	}

	public bool canMove(Vector2I NextPosition, Vector2I CurrentPosition)
	{
		if(selectedPiece is not null)
			return selectedPiece.Move(NextPosition, CurrentPosition);
		else
		{
			return false;
		}
	}

	private void instiantatePiece()
	{
		if(selectedPiece is null)
		{
			selectedPiece = null;
			Node fry = pieceScene.Instantiate();
			AddChild(fry);
			selectedPiece = fry as Piece;
		}
	}
	
	public void ClearPiece()
	{
		selectedPiece.QueueFree();
		selectedPiece = null;
	}

	public bool hasPiece()
	{
		if (selectedPiece is null)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
}
