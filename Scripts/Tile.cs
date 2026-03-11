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
		selectedPiece = null;
		pieceScene = pScene;
		instiantatePiece();
	}

	private void instiantatePiece()
	{
		Node fry = pieceScene.Instantiate();
		AddChild(fry);
	}
}
