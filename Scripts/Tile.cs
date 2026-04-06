using Godot;
using System;

public partial class Tile : Node2D
{
	[Export] private Vector2I position;
	[Export] private Piece selectedPiece;
	[Export] private PackedScene pieceScene;
	private Tile[,] tiless;

	public Vector2I getPosition()
	{
		return position;
	}

	public void setPiece(PackedScene pScene, Tile[,] tiles)
	{
		pieceScene = pScene;
		selectedPiece = null;
		Node fry = pieceScene.Instantiate();
		AddChild(fry);
		selectedPiece = fry as Piece;
		this.tiless = tiles;
	}

	public void block()
	{
		selectedPiece.PieceBlocking(getPosition(), tiless);
	}

	public bool canMove(Vector2I NextPosition)
	{
		if((selectedPiece is not null) && (NextPosition != position))
			return selectedPiece.Move(NextPosition, position);
		else
		{
			return false;
		}
	}
	
	public void ClearPiece()
	{
		selectedPiece.QueueFree();
		selectedPiece = null;
	}

	public Piece.PieceType getSelectedPiece()
	{
		return selectedPiece.returnType();
	}

	public bool hasPieceNot(Piece.PieceType pieceType = Piece.PieceType.Nothing)
	{
		if (selectedPiece is null)
		{
			return true;
		}
		else
		{
			Piece.PieceType current = getSelectedPiece();
			if (pieceType == current)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
	
	
}
