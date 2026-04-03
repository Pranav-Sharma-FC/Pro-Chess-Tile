using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;

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
		foreach (MovementResource moveResource in Movements)
		{
			int xstart = FindStart(moveResource.xmov, CurrentPosition.X);
			int ystart = FindStart(moveResource.ymov, CurrentPosition.Y);
			Vector2I newPosition = new Vector2I(xstart, ystart);
			moveResource.closest = newPosition;
			while(newPosition != CurrentPosition)
			{
				GD.Print(newPosition);
				Tile tile = tiles[newPosition.X, newPosition.Y];
				if (!tile.hasPieceNot());
				{
					moveResource.closest = newPosition;
				}
				newPosition.X += moveResource.xmov;
				newPosition.Y += moveResource.ymov;
			}
		}
	}

	private int FindStart(int vari, int current)
	{
		if (vari == 0)
			return current;
		else if (vari == 1)
		{
			return 0;
		}
		else if (vari == -1)
		{
			return (8-current);
		}
		else
		{
			GD.Print("Someone screwed up");
			return 42;
		}
	}

	private Vector2I FindSlope(int x, int y)
	{
		int xNext = 0;
		int yNext = 0;
		if (x < 0)
		{
			xNext = 1;
		}
		else if (x > 0)
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
			foreach (MovementResource moveResource in Movements)
			{
				if ((moveResource.xmov == movementSlope.X) && (moveResource.ymov == movementSlope.Y))
				{
					closestCurrent = moveResource.closest;
				}
			}

			if (((NextPosition.X-CurrentPosition.X)<=int.Abs(closestCurrent.X-CurrentPosition.X))&&
				((NextPosition.Y-CurrentPosition.Y)<=int.Abs(closestCurrent.Y-CurrentPosition.Y)))
			{
				moveFlag = true;
			}
		}

		GD.Print(moveFlag);
		return moveFlag;
	}
}
