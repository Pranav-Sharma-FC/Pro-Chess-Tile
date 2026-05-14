using Godot;
using System;
using System.IO.IsolatedStorage;
using UIProject.Scripts;

public partial class Tile : Node2D
{
	
	[Signal]
	public delegate void DeathEventHandler();
	[Signal]
	public delegate void GameOverEventHandler(int pieceType);

	[Export] private Vector2I position;
	private Piece selectedPiece;
	[Export] private PackedScene pieceScene;
	private Tile[,] gridTile;

	private AnimatedSprite2D selectedMine;
	[Export] private PackedScene mineScene;
	private bool hasMine;
	private bool blackMine;

	public Vector2I getPosition()
	{
		return position;
	}

	public bool canGetMana()
	{
		if(selectedPiece is not null)
			return selectedPiece.getCanMana();
		else
			return false;
	}
	public void setMine(bool isBlack)
	{
		blackMine = isBlack;
		hasMine = true;
		//Piece fry = pieceScene.Instantiate<Piece>();
		selectedMine = mineScene.Instantiate<AnimatedSprite2D>();
		AddChild(selectedMine);
	}

	public PackedScene getPieceScene()
	{
		return pieceScene;
	}

	public void switchSpawnables(int pieceType)
	{
		if(selectedPiece is not null)
			selectedPiece.SpawnSpawnables(pieceType, getPosition());
	}

	public void setPiece(PackedScene pScene, bool isBlack = false)
	{
		if(pScene is not null)
		{
			ClearPiece(true);
			pieceScene = pScene;
			Piece fry = pieceScene.Instantiate<Piece>();
			selectedPiece = fry;
			if(isBlack)
			{
				selectedPiece.blackPiece();
			}
			if(fry is Pawn paw)
			{
				paw.Passant += EnPassant;
			}
			selectedPiece.setGri(gridTile, getPosition(), this);
			AddChild(fry);
			if(hasMine && isBlack != blackMine)
			{
				GD.Print("Cool");
				selectedPiece.damageAfterMadePiece();
				hasMine = false;
				selectedMine.QueueFree();
			}
		}
	}

	public int getPieceMana()
	{
		if (selectedPiece is not null)
			return selectedPiece.getMana();
		else
			return Int32.MaxValue;
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

	public override void _Process(double delta)
	{
		if (selectedPiece is not null)
		{
			if (selectedPiece.getHealth() <= 0)
			{
				EmitSignal(SignalName.Death);
				ClearPiece();
			}
		}
	}

	public void specialActivation()
	{
		if(selectedPiece is not null)
			selectedPiece.ActivateSpecial();
	}

	public void EnPassant(int pTye)
	{
		if(selectedPiece is not null)
		{
			Piece.PieceType truth = (Piece.PieceType)pTye;
			Vector2I truePos = new Vector2I(position.X-1 , position.Y -1);
			canPassant(truth, (truePos + (new Vector2I(-1, 0))));
			canPassant(truth, (truePos + (new Vector2I(1, 0))));
		}
	}

	private void canPassant(Piece.PieceType truth, Vector2I loco)
	{
		if(loco.X is >= 0 and < 8 && loco.Y is >= 0 and < 8)
		{
			Vector2I noLoco = new Vector2I(position.X-1 , position.Y -1);
			Tile tile = gridTile[loco.X, loco.Y];
			tile.sendPassant(truth, noLoco);
		}
		
	}

	public void sendPassant(Piece.PieceType truth, Vector2I loco)
	{
		if(selectedPiece is not null)
		{
			//GD.Print(loco);
			if(selectedPiece is Pawn pin && (truth != selectedPiece.returnType()))
			{
				pin.Passanting(loco);
			}
		}
	}

	public void ClearPiece(bool isClear = false)
	{
		if(selectedPiece is not null)
		{
			if (isClear)
			{
				//Gd.Print("Shaurya");
				if(selectedPiece is King king)
					EmitSignal(SignalName.GameOver, (int)selectedPiece.returnType());
			}
			selectedPiece.QueueFree();
			selectedPiece = null;
		}
	}

	public void AttackAlls(bool isFriend)
	{
		selectedPiece.AttackAll(isFriend);
	}

	public Piece.PieceType getSelectedPiece()
	{
		if(selectedPiece is not null)
			return selectedPiece.returnType();
		else
			return Piece.PieceType.Nothing;
	}

	public bool hasPiece(bool needsPawn = false)
	{
		if(selectedPiece is not null)
			if(needsPawn)
				return (selectedPiece is Pawn);
			else
				return true;
		else
			return false;
	}

	//If it works it works ok
	public bool hasPieceNot(Piece.PieceType pieceType = Piece.PieceType.Nothing)
	{
		Piece.PieceType current = getSelectedPiece();
		if (selectedPiece is null)
		{
			return true;
		}
		else if (selectedPiece is King king)
		{
			if(pieceType == Piece.PieceType.Nothing)
				return false;
			else if (king.canCapture() && pieceType != current)
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
		if(selectedPiece is not null)
			selectedPiece.damagePiece(damage);
	}

	public void gridPiece(Tile[,] grid, bool isFallen = false)
	{
		gridTile = grid;
		if (selectedPiece is not null)
		{
			selectedPiece.setGri(grid, getPosition(), this);
			if(isFallen)
				block(grid);
		}
	}
	
}
