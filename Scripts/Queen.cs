using Godot;
using System;

public partial class Queen : Piece
{
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
		bool moveFlag = false;
		if((Math.Abs(CurrentPosition.X-NextPosition.X)==Math.Abs(CurrentPosition.Y-NextPosition.Y)))
		{
			
			moveFlag = true;
		}
		else if(((NextPosition.X == CurrentPosition.X) || (NextPosition.Y == CurrentPosition.Y)))
		{
			moveFlag = true;
		}
		
		GD.Print(moveFlag);
		return moveFlag;
	}
}
