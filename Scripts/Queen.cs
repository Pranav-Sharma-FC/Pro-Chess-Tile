using System;
using Godot;
using Godot.Collections;

namespace UIProject.Scripts;

public partial class Queen : Piece
{
	[Export] private Array<MovementResource> Movements = new Array<MovementResource>();

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
			bool flag = true;
			newPosition += new Vector2I(moveResource.xmov, moveResource.ymov);
			while(flag)
			{
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
							flag = false;
						}
					}
					catch (IndexOutOfRangeException)
					{
						//GD.Print(newPosition + "Error");
						flag = false;
					}
				}
				else
				{
					flag = false;
				}
				newPosition += new Vector2I(moveResource.xmov, moveResource.ymov);
				
			}
		}

		return false;
	}
	public override void setGrid(Tile[,] grid)
	{
		gridPiece = grid;
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
	
	public override void SpawnSpawnables(int pType, Vector2I curPos)
	{
		if (this.pieceType == (PieceType)pType)
		{
			
		}

		else
		{
			
		}
	}

	//Logic to make sure piece can move there
	public override bool Move(Vector2I NextPosition, Vector2I CurrentPosition)
	{
		CurrentPosition -= new Vector2I(1, 1);
		NextPosition -= new Vector2I(1, 1);
		bool moveFlag = false;
		Vector2I closestCurrent = new Vector2I(-1, -1);
		if((Math.Abs(CurrentPosition.X-NextPosition.X)==Math.Abs(CurrentPosition.Y-NextPosition.Y))||((NextPosition.X == CurrentPosition.X) || (NextPosition.Y == CurrentPosition.Y)))
		{
			int ymoved = NextPosition.Y - CurrentPosition.Y;
			int xmoved = NextPosition.X - CurrentPosition.X;
			Vector2I movementSlope = FindSlope(xmoved, ymoved);
			foreach (MovementResource moveResource in Movements)
			{
				//GD.Print(moveResource.closest + " : Very Current : " + moveResource.xmov + " : " + moveResource.ymov);
				if ((moveResource.xmov == movementSlope.X) && (moveResource.ymov == movementSlope.Y))
				{
					//GD.Print("Identified");
					closestCurrent = moveResource.closest;
					//GD.Print(moveResource.closest);
				}
			}
			int toNext = (Math.Abs(CurrentPosition.X-NextPosition.X)+Math.Abs(CurrentPosition.Y-NextPosition.Y));
			int toClosest = (Math.Abs(CurrentPosition.X-closestCurrent.X)+Math.Abs(CurrentPosition.Y-closestCurrent.Y));
			GD.Print("Current: " + CurrentPosition + ", Next: " + NextPosition + ", Cloests: " + closestCurrent);
			GD.Print("Next: " + toNext + " Closest: " + toClosest);
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
