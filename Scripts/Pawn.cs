using Godot;
using System;
using Godot.Collections;

public partial class Pawn : Piece
{
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

	public override void SpawnSpawnables(int pieceType)
	{

	}

	public override void setGrid(Tile[,] grid)
	{
		gridPiece = grid;
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
		return moveFlag;
	}
}
