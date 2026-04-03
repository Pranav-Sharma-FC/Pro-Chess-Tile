using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Loader;

public partial class Rook : Piece
{
	//AI helped me remember Godot.Collections
	[Export] private Array<MovementResource> Movements = new Array<MovementResource>();
	protected override void SetPoints()
	{
		
	}

	public override void GivePiece()
	{
		
	}

	public override void PieceBlocking(Vector2I CurrentPosition, Tile[,]  tiles)
	{
		GD.Print("u");
		CurrentPosition -= new Vector2I(1, 1);
		foreach (MovementResource moveResource in Movements)
		{
			GD.Print(moveResource.xmov + " : " + moveResource.ymov);
			moveResource.closest = new Vector2I(-1, -1);
			Vector2I newPosition = new Vector2I(CurrentPosition.X, CurrentPosition.Y);
			GD.Print("NewPosition: " + newPosition);
			bool flag = true;
			while(flag)
			{
				if(CurrentPosition != newPosition)
				{
					Tile tile = tiles[newPosition.X, newPosition.Y];
					if (!tile.hasPieceNot());
					{
						GD.Print(newPosition + "PositionTile");
						moveResource.closest = newPosition;
						flag = false;
					}
				}
				newPosition.X += moveResource.xmov;
				newPosition.Y += moveResource.ymov;

				if (newPosition.X is >= 0 and < 8 || newPosition.Y is >= 0 and < 8)
				{
					flag = false;
				}
				
			}
		}
	}

	private Vector2I FindSlope(int x, int y)
	{
		int xNext = 0;
		int yNext = 0;
		if (x > 0)
		{
			xNext = 1;
		}
		else if (x < 0)
		{
			xNext = -1;
		}

		if (y < 0)
		{
			yNext = 1;	
		}
		else if (y > 0)
		{
			yNext = -1;
		}
		
		return new Vector2I(xNext, yNext);
	}
	
	//Logic to make sure piece can move there
	public override bool Move(Vector2I NextPosition, Vector2I CurrentPosition)
	{
		GD.Print("Hi");
		bool moveFlag = false;
		if (((NextPosition.X == CurrentPosition.X) || (NextPosition.Y == CurrentPosition.Y)))
		{
			int ymoved = NextPosition.Y - CurrentPosition.Y;
			int xmoved = NextPosition.X - CurrentPosition.X;
			Vector2I movementSlope = FindSlope(xmoved, ymoved);
			Vector2I closestCurrent = new Vector2I(-1, -1);
			GD.Print(movementSlope + "EEEEE");
			foreach (MovementResource moveResource in Movements)
			{
				GD.Print(moveResource.closest + " : Very Current");
				if ((moveResource.xmov == movementSlope.X) && (moveResource.ymov == movementSlope.Y))
				{
					GD.Print("Identified");
					closestCurrent = moveResource.closest;
					GD.Print(moveResource.closest);
				}
			}
			Vector2I toNext = (CurrentPosition-NextPosition).Abs();
			GD.Print(toNext);
			Vector2I toClosest = (CurrentPosition-closestCurrent).Abs();
			GD.Print(toClosest);
			if (closestCurrent == new Vector2I(-1, -1)) 
				moveFlag = true;
			else if (toNext <= toClosest)
			{
				moveFlag = true;
			}
		}

		GD.Print(moveFlag);
		return moveFlag;
	}
}
