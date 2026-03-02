using Godot;
using System;

public partial class Nothing : Piece
{
	[Export] private bool a;
	public override void _Ready()
	{
		PieceType e = PieceType.White;
	}
	protected override void SetPoints()
	{
		if (CurrentPosition == Points[0])
		{
			
		}
	}

	public override void GivePiece()
	{
		GD.Print("Giving piece");
		ChessBoard.SetSelectedTile(this);
	}

	public override bool Move(Piece tile)
	{
		return true;
	}
}
