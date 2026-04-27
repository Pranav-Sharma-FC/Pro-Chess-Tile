using Godot;
using System;
using Godot.Collections;

namespace UIProject.Scripts;

public partial class Pawn : Piece
{
	//The pawn has more exceptions than a chemistry class
	//For being a pawn this thing sure acting like its the king of my sanity
	bool isFirstMove;
	int thing;
	public override void _Ready()
	{
		thing = (pieceType == PieceType.Black) ? -1 : 1;
		spriteNum = sprite.Frame;
		MovementResource northe = new MovementResource();
		northe.setValues(0, 1 * thing);
		Movements.Add(northe);
		MovementResource eastw = new MovementResource();
		eastw.setValues(-1, 1* thing);
		Movements.Add(eastw);
		MovementResource westw = new MovementResource();
		westw.setValues(1, 1* thing);
		Movements.Add(westw);	
		MovementResource westww = new MovementResource();
		westww.setValues(0, 2* thing);
		Movements.Add(westww);	
	}

	public override void PieceBlocking(Vector2I CurrentPosition, Tile[,]  tiles)
	{
		//GD.Print("u");
		Vector2I temp = new Vector2I(-1, -1);
		CurrentPosition += temp;
		foreach (MovementResource moveResource in Movements)
		{
			//GD.Print(moveResource.xmov + " : " + moveResource.ymov);
			moveResource.closest = new Vector2I(-1, -1);
			Vector2I newPosition = new Vector2I(CurrentPosition.X, CurrentPosition.Y);
			//GD.Print("NewPosition: " + newPosition);
			newPosition += new Vector2I(moveResource.xmov, moveResource.ymov);
			double xmov = newPosition.X - CurrentPosition.X;
			double ymov = newPosition.Y - CurrentPosition.Y;
			double current = (Math.Sqrt((ymov*ymov)+(xmov*xmov)));
			if(current == Mathf.Sqrt(4))
			{
				Vector2I pawnsNewPosition = new Vector2I(thing + newPosition.X, newPosition.Y);
				moveResource.closest = pawnBlock(pawnsNewPosition, tiles, false);
				if(moveResource.closest == temp)
					moveResource.closest = pawnBlock(newPosition, tiles, false);
			}
			else if(current == Mathf.Sqrt(2))
			{
				moveResource.closest = pawnBlock(newPosition, tiles, true);
			}
			else
			{
				moveResource.closest = pawnBlock(newPosition, tiles, false);
			}
		}
	}

	public Vector2I pawnBlock(Vector2I newPosition, Tile[,] tiles, bool isSide)
	{
		Vector2I closest = new Vector2I(-1, -1);
		if (newPosition.X is >= 0 and < 8 && newPosition.Y is >= 0 and < 8)
			{
				try
				{
					Tile tile = tiles[newPosition.X, newPosition.Y];
					//GD.Print(newPosition + "Has Piece: " + !tile.hasPieceNot());
					if (tile.hasPiece() && !isSide)
					{
						//GD.Print(newPosition + "PositionTile");
						closest = newPosition;
					}
					else if(tile.hasPieceNot() && isSide)
					{
						closest = newPosition;
					}
				}
				catch (IndexOutOfRangeException)
				{
					GD.Print("Index Out of Range Knight");
				}
			}
		return closest;
	}

	public override void SetPoints(Godot.Collections.Dictionary<string, int> Resources)
	{
		Health = Resources["Health"];
		isFirstMove = false;
	}

	public override Godot.Collections.Dictionary<string, int> GivePiece()
	{
		return new Dictionary<string, int>{
			{"Health", Health},
		};
	}

	//Logic to make sure piece can move there
	public override bool Move(Vector2I NextPosition, Vector2I CurrentPosition)
	{
			//GD.Print("u");
		bool moveFlag = false;
		Vector2I temp = new Vector2I(-1, -1);
		CurrentPosition += temp;
		NextPosition += temp;
		double xmov = NextPosition.X - CurrentPosition.X;
		double ymov = NextPosition.Y - CurrentPosition.Y;
		double current = (Math.Sqrt((ymov*ymov)+(xmov*xmov)));
		double imov = 0.0;
		Vector2I closestCurrent = new Vector2I(-1, -1);
		foreach (MovementResource moveResource in Movements)
		{
			//GD.Print(moveResource.xmov + " : " + moveResource.ymov);
			moveResource.closest = new Vector2I(-1, -1);
			Vector2I newPosition = new Vector2I(CurrentPosition.X, CurrentPosition.Y);
			//GD.Print("NewPosition: " + newPosition);
			newPosition += new Vector2I(moveResource.xmov, moveResource.ymov);
			double xmovn = newPosition.X - CurrentPosition.X;
			double ymovn = newPosition.Y - CurrentPosition.Y;
			double moveCurrent = (Math.Sqrt((ymovn*ymovn)+(xmovn*xmovn)));
			if(moveCurrent == current && ymov == ymovn)
			{
				imov = ymovn;
				closestCurrent = moveResource.closest;
			}
		}
		if(closestCurrent == new Vector2I(-1, -1))
		{
			if(ymov == imov && (((current == Mathf.Sqrt(4)) && isFirstMove)||current == Mathf.Sqrt(1)))
			{
				moveFlag = true;	
			}
		}
		else if(current == Mathf.Sqrt(2))
		{
			if(gridPiece[NextPosition.X, NextPosition.Y].getSelectedPiece() != pieceType)
				moveFlag = true;
		}
		return moveFlag;
	}
}
