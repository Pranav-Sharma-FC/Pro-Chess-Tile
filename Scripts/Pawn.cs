using Godot;
using System;

public partial class Pawn : Piece
{
	[Export] private bool a;
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

	public override bool Move(Piece tile)
	{
		return true;
	}
}
