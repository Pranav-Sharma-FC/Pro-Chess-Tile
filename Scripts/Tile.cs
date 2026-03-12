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

	public bool canMove(NextPosition)
	{
		selectedPiece.Move(pos, NextPosition)
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
}
