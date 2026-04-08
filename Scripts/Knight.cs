using Godot;
using System;

public partial class Knight : Piece
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
