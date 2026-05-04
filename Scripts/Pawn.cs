using Godot;
using System;
using Godot.Collections;

namespace UIProject.Scripts;

public partial class Pawn : Piece
{
	//The pawn has more exceptions than a chemistry class
	//For being a pawn this thing sure acting like its the king of my sanity
	[Signal]
	public delegate void PassantEventHandler(int pieceTyp);
	private Tile tillyPawn;
	private Vector2I passantAngle;
	private bool isFirstMove = true;
	private bool canPassat = false;
	private int thing;
	private int passan = 0;
	//isPawn = true;
	public override void _Ready()
	{
		thing = (pieceType == PieceType.Black) ? -1 : 1;	
		spriteNum = sprite.Frame;
		MovementResource northe = new MovementResource();
		northe.setValues(0, 1 * thing);
		northe.pawn();
		Movements.Add(northe);
		MovementResource eastw = new MovementResource();
		eastw.setValues(-1, 1* thing);
		Movements.Add(eastw);
		MovementResource westw = new MovementResource();
		westw.setValues(1, 1* thing);
		Movements.Add(westw);	
		MovementResource westww = new MovementResource();
		westww.setValues(0, 2* thing);
		westww.pawn();
		Movements.Add(westww);	
	}

	public override void ActivateSpecial()
	{
		
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
			//GD.Print(current+ " E"+((int)(current*current) == 2));
			if(current == Mathf.Sqrt(4))
			{
				Vector2I pawnsNewPosition = new Vector2I(newPosition.X, newPosition.Y - thing);
				moveResource.closest = pawnBlock(pawnsNewPosition, tiles, false);
				//(moveResource.closest + "4" + pawnsNewPosition);
				if(moveResource.closest == temp)
					moveResource.closest = pawnBlock(newPosition, tiles, false);
			}
			else if((int)(current*current) == 2)
			{
				moveResource.closest = pawnBlock(newPosition, tiles, true);
			}
			else
			{
				moveResource.closest = pawnBlock(newPosition, tiles, false);
			}
			//GD.Print(moveResource.closest);
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
					else if(!tile.hasPieceNot() && isSide)
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
		if(Resources["Passant"] == 1)
		{
			EmitSignal(SignalName.Passant, (int)pieceType);
		}

	}

	public void Passanting(Vector2I vec)
	{
		canPassat = true;
		tillyPawn = gridPiece[vec.X, vec.Y];
		passantAngle = vec;
	}

	public override Godot.Collections.Dictionary<string, int> GivePiece()
	{
		if(passan == 2)
		{
			tillyPawn.ClearPiece();
		}
		return new Dictionary<string, int>{
			{"Health", Health},
			{"Passant", passan},
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
		Vector2I closestCurrent = new Vector2I(-1, -1);
		foreach (MovementResource moveResource in Movements)
		{
			//GD.Print(moveResource.xmov + " : " + moveResource.ymov + moveResource.closest)
			Vector2I newPosition = new Vector2I(CurrentPosition.X, CurrentPosition.Y);
			//GD.Print("NewPosition: " + newPosition);
			newPosition += new Vector2I(moveResource.xmov, moveResource.ymov);
			if(newPosition == NextPosition)
			{
				closestCurrent = moveResource.closest;
			}
		}
		//GD.Print(closestCurrent);
		if(closestCurrent == new Vector2I(-1, -1))
		{
			if(((current == Mathf.Sqrt(4)) && isFirstMove && (int)(ymov/2) == thing)||(current == Mathf.Sqrt(1) && ymov == thing))
			{
				//GD.Print("Interesting");
				moveFlag = true;	
				passan = 0;
				if(current == Mathf.Sqrt(4))
					passan = 1;
			}
			else if(((int)(current*current) == 2) && canPassat && ymov == thing && (passantAngle.X == NextPosition.X))
			{
				//GD.Print("CanMove");
				if(gridPiece[NextPosition.X, NextPosition.Y].getSelectedPiece() != pieceType)
				{
					moveFlag = true;
					passan = 2;
				}
			}
		}
		else if((int)(current*current) == 2)
		{
			//GD.Print("CanMove");
			if(gridPiece[NextPosition.X, NextPosition.Y].getSelectedPiece() != pieceType)
			{
				moveFlag = true;
				passan = 0;
			}
		}
		return moveFlag;
	}
}
