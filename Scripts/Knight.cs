using Godot;
using Godot.Collections;
using System;

public partial class Knight : Piece
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
