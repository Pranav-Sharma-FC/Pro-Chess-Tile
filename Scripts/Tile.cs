using Godot;
using System;
using UIProject.Scripts;

public partial class Tile : Node2D
{
	[Signal]
	public delegate void GameOverEventHandler(int pieceType);

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
			Piece fry = pieceScene.Instantiate<Piece>();
			selectedPiece = fry;
			if(isBlack)
			{
				selectedPiece.blackPiece();
			}
			AddChild(fry);
			
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
			if (selectedPiece is King king)
			{
				GD.Print("Shaurya");
				EmitSignal(SignalName.GameOver, (int)selectedPiece.returnType());
			}
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
		else if (selectedPiece is King king)
		{
			if (king.canCapture())
				return true; 
			else
				return false; 
			
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
