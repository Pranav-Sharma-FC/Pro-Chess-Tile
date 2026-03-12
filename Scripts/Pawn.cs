using Godot;
using System;

public partial class Pawn : Piece
{
	[Export] Texture2D whitePawn;
	protected override void SetPoints()
	{
		if (CurrentPosition == Points[0])
		{
			
		}
	}

	public override void GivePiece()
	{
		GD.Print("Giving piece");
		if (ChessBoard == null)
		{
			GD.Print("No chess board");
		}
		ChessBoard.SetSelectedPiece(this);
	}

	//Logic to make sure piece can move there
	public override bool Move(Vector2 NextPosition)
	{
		bool moveFlag = True;
		if(CurrentPosition.X == NextPosition.X)
		{
			if(CurrentPosition.Y < NextPosition.Y <= (CurrentPosition.Y + 2))
			{
				return true;
			}
		}
		return false;
	}
}
