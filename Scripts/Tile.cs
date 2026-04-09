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

	public PackedScene getPieceScene()
	{
		return pieceScene;
	}

	public void setPiece(PackedScene pScene, bool isBlack = false)
	{
		if(pScene is not null)
		{
			ClearPiece();
			pieceScene = pScene;
			Node fry = pieceScene.Instantiate();
			AddChild(fry);
			selectedPiece = fry as Piece;
			if(isBlack)
			{
				selectedPiece.blackPiece();
			}
		}
	}

	public void block(Tile[,] tiles)
	{
		selectedPiece.PieceBlocking(getPosition(), tiles);
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
		if(selectedPiece is not null)
		{
			selectedPiece.QueueFree();
			selectedPiece = null;
		}
	}

	public Piece.PieceType getSelectedPiece()
	{
		if(selectedPiece is not null)
			return selectedPiece.returnType();
		else
			return Piece.PieceType.Nothing;
	}

	public bool hasPieceNot(Piece.PieceType pieceType = Piece.PieceType.Nothing)
	{
		if (selectedPiece is null)
		{
			return true;
		}
		else
		{
			//GD.Print("ples");
			Piece.PieceType current = getSelectedPiece();
			if (pieceType == Piece.PieceType.Nothing)
			{
				return false;
			}
			else if (pieceType == current)
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
