using Godot;
using System;

public partial class Pawn : Piece
{
	[Export] Texture2D whitePawn;
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
		if(CurrentPosition.X == NextPosition.X)
		{
			GD.Print("X is Chill");
			if ((CurrentPosition.Y < NextPosition.Y) && (NextPosition.Y <= (CurrentPosition.Y + 1)))
			{
				return true;
			}
		}
		return false;
	}
}
