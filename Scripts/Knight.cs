using Godot;
using Godot.Collections;
using System;

namespace UIProject.Scripts;

public partial class Knight : Piece
{
	public override void _Ready()
	{
		spriteNum = sprite.Frame;
		MovementResource north = new MovementResource();
		north.setValues(2, 1);
		Movements.Add(north);
		MovementResource south = new MovementResource();
		south.setValues(2, -1);
		Movements.Add(south);
		MovementResource east = new MovementResource();
		east.setValues(-2, -1);
		Movements.Add(east);
		MovementResource west = new MovementResource();
		west.setValues(-2, 1);
		Movements.Add(west);	
		MovementResource northe = new MovementResource();
		northe.setValues(1, 2);
		Movements.Add(northe);
		MovementResource southe = new MovementResource();
		southe.setValues(1, -2);
		Movements.Add(southe);
		MovementResource eastw = new MovementResource();
		eastw.setValues(-1, -2);
		Movements.Add(eastw);
		MovementResource westw = new MovementResource();
		westw.setValues(-1, 2);
		Movements.Add(westw);	
	}

	public override bool PieceBlocking(Vector2I CurrentPosition, Tile[,]  tiles)
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
					GD.Print("Index Out of Range Knight");
				}
			}
		}
		return false;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
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
		bool moveFlag = true;
		double DISKNIGHT = Math.Sqrt(5);
		double xmov = NextPosition.X - CurrentPosition.X;
		double ymov = NextPosition.Y - CurrentPosition.Y;
		if(!(Math.Sqrt((ymov*ymov)+(xmov*xmov))==DISKNIGHT))
		{
			moveFlag = false;
		}
		GD.Print(moveFlag);
		return moveFlag;
	}
}
