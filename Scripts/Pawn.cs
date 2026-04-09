using Godot;
using System;

public partial class Pawn : Piece
{
	public override void PieceBlocking(Vector2I CurrentPosition, Tile[,]  tiles)
	{
	}
	protected override void SetPoints()
	{
		
	}

	public override void GivePiece()
	{
		
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
