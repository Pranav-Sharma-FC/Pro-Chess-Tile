using Godot;
using System;
using Godot.Collections;

namespace UIProject.Scripts;
public partial class King : Piece
{
	public override void _Ready()
	{
		spriteNum = sprite.Frame;
		MovementResource north = new MovementResource();
		north.setValues(0, 1);
		Movements.Add(north);
		MovementResource south = new MovementResource();
		south.setValues(0, -1);
		Movements.Add(south);
		MovementResource east = new MovementResource();
		east.setValues(1, 0);
		Movements.Add(east);
		MovementResource west = new MovementResource();
		west.setValues(-1, 0);
		Movements.Add(west);	
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

	public override void PieceBlocking(Vector2I CurrentPosition, Tile[,]  tiles)
	{
		//GD.Print("u");
		CurrentPosition -= new Vector2I(1, 1);
		foreach (MovementResource moveResource in Movements)
		{
			//GD.Print(moveResource.xmov + " : " + moveResource.ymov);
			moveResource.closest = new Vector2I(-1, -1);
			Vector2I newPosition = new Vector2I(CurrentPosition.X, CurrentPosition.Y);
			//GD.Print("NewPosition: " + newPosition);
			newPosition += new Vector2I(moveResource.xmov, moveResource.ymov);
			if (newPosition.X is >= 0 and < 8 && newPosition.Y is >= 0 and < 8)
			{
				try
				{
					Tile tile = tiles[newPosition.X, newPosition.Y];
					//GD.Print(newPosition + "Has Piece: " + !tile.hasPieceNot());
					if (!tile.hasPieceNot())
					{
						//GD.Print(newPosition + "PositionTile");
						moveResource.closest = newPosition;
					}
				}
				catch (IndexOutOfRangeException)
				{
					GD.Print("Index Out of Range King");
				}
			}
		}
	}
	public override void ActivateSpecial()
	{
		
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

	public bool canCapture()
	{
		return true;
	}
	public PieceType capturedKing()
	{
		return pieceType;
	}
	//Logic to make sure piece can move there
	public override bool Move(Vector2I NextPosition, Vector2I CurrentPosition)
	{
		bool moveFlag = true;
		if(!(NextPosition.X >= (CurrentPosition.X - 1) && (NextPosition.X <= (CurrentPosition.X + 1))))
		{
			moveFlag = false;
		}
		else if (!(NextPosition.Y >= (CurrentPosition.Y - 1) && (NextPosition.Y <= (CurrentPosition.Y + 1))))
		{
			moveFlag = false;
		}
		
		//GD.Print("Lomg"+ moveFlag);
		return moveFlag;
	}
}
