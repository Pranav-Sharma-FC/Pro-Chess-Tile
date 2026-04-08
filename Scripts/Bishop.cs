using Godot;
using System;

public partial class Bishop : Piece
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
		GD.Print("Hi");
		bool moveFlag = true;
		if(!(Math.Abs(CurrentPosition.X-NextPosition.X)==Math.Abs(CurrentPosition.Y-NextPosition.Y)))
		{
			
			moveFlag = false;
		}
		GD.Print(moveFlag);
		return moveFlag;
	}
}
