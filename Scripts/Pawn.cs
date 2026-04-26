using Godot;
using System;
using Godot.Collections;

namespace UIProject.Scripts;

public partial class Pawn : Piece
{
	public override void _Ready()
	{
		spriteNum = sprite.Frame;
		if(pieceType == PieceType.White)
		{
			spriteNum = sprite.Frame;
			MovementResource northe = new MovementResource();
			northe.setValues(0, -1);
			Movements.Add(northe);
			MovementResource eastw = new MovementResource();
			eastw.setValues(-1, -1);
			Movements.Add(eastw);
			MovementResource westw = new MovementResource();
			westw.setValues(1, -1);
			Movements.Add(westw);	
		}
		else
		{
			spriteNum = sprite.Frame;
			MovementResource northe = new MovementResource();
			northe.setValues(1, 1);
			Movements.Add(northe);
			MovementResource southe = new MovementResource();
			southe.setValues(1, -1);
			Movements.Add(southe);
			MovementResource eastw = new MovementResource();
			eastw.setValues(-1, -1);
			Movements.Add(eastw);
			MovementResource westw = new MovementResource();
			westw.setValues(-1, 1);
			Movements.Add(westw);	
		}
	}
	public override bool PieceBlocking(Vector2I CurrentPosition, Tile[,]  tiles)
	{
		return true;
	}

	public override void SetPoints(Godot.Collections.Dictionary<string, int> Resources)
	{
		Health = Resources["Health"];
	}

	public override Godot.Collections.Dictionary<string, int> GivePiece()
	{
		return new Dictionary<string, int>{
			{"Health", Health}
		};
	}

	//Logic to make sure piece can move there
	public override bool Move(Vector2I NextPosition, Vector2I CurrentPosition)
	{
		bool moveFlag = false;
		if(CurrentPosition.X == NextPosition.X)
		{
			if (pieceType == PieceType.White && (CurrentPosition.Y < NextPosition.Y) && (NextPosition.Y <= (CurrentPosition.Y + 1)))
			{
				moveFlag = true;
			}
			else if(pieceType == PieceType.Black && (CurrentPosition.Y > NextPosition.Y) && (NextPosition.Y >= (CurrentPosition.Y - 1)))
			{
				moveFlag = true;
			}
		}
		GD.Print("Lomg");
		return moveFlag;
	}
}
