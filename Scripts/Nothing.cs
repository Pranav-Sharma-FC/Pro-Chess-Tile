using Godot;
using System;

public partial class Nothing : Piece
{
	[Export] private bool a;
	public override void PieceBlocking(Vector2I CurrentPosition, Tile[,]  tiles)
	{
		
		}
	protected override void SetPoints()
	{
		
	}

	public override void GivePiece()
	{
		GD.Print("Giving piece");
		//ChessBoard.SetSelectedTile(this);
	}

	public override bool Move(Vector2I tile, Vector2I currentPosition)
	{
		return true;
	}
}
