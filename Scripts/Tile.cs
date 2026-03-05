using Godot;
using System;

public partial class Tile : Node2D
{
	[Export] private Vector2I position;
	[Export] private Piece selectedPiece;

	public Vector2I getPosition()
	{
		return position;
	}

	public void setPiece(Piece p)
	{
		selectedPiece = p;
	}
}
