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

	public void setPiece(PackedScene pScene, Tile[,] tiles)
	{
		pieceScene = pScene;
		selectedPiece = null;
		Node fry = pieceScene.Instantiate();
		AddChild(fry);
		GD.Print("WHy");
		selectedPiece = fry as Piece;
		GD.Print(selectedPiece);
		selectedPiece.PieceBlocking(getPosition(), tiles);
	
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
			GD.Print("Oh No");
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
