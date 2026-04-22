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
	private Tile[,] gridTile;

	public Vector2I getPosition()
	{
		return position;
	}

	public PackedScene getPieceScene()
	{
		return pieceScene;
	}

	public void switchSpawnables(int pieceType)
	{
		if(selectedPiece is not null)
			selectedPiece.SpawnSpawnables(pieceType);
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
			selectedPiece.setGri(gridTile, getPosition());
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
		Piece.PieceType current = getSelectedPiece();
		if (selectedPiece is null)
		{
			return true;
		}
		else if (selectedPiece is King king)
		{
			if (king.canCapture() && pieceType != current)
				return true; 
			else
				return false; 
			
		}
		else
		{
			//GD.Print("ples");
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
	
	public void SetPoints(Godot.Collections.Dictionary<string, int> Resources)
	{
		selectedPiece.SetPoints(Resources);
	}

	public Godot.Collections.Dictionary<string, int> GivePiece()
	{
		return selectedPiece.GivePiece();
	}

	public void DamagePiece(int damage)
	{
		selectedPiece.damagePiece(damage);
	}

	public void gridPiece(Tile[,] grid)
	{
		if (selectedPiece is not null)
		{
			selectedPiece.setGri(grid, getPosition());
			gridTile = grid;
		}
	}
	
}
