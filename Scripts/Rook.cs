using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;

public partial class Rook : Piece
{
	//AI helped me remember Godot.COllectins
	[Export] private Array<MovementResource> Movements = new Array<MovementResource>();
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
		if(!((NextPosition.X == CurrentPosition.X) || (NextPosition.Y == CurrentPosition.Y)))
		{
			moveFlag = false;
		}

		GD.Print(moveFlag);
		return moveFlag;
	}
}
