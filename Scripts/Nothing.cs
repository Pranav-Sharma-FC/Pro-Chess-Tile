using Godot;
using Godot.Collections;
using System;

public partial class Nothing : Piece
{
	[Export] private bool a;
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

	public override bool Move(Vector2I tile, Vector2I currentPosition)
	{
		return true;
	}
	
	public override void SpawnSpawnables(int pieceType)
	{
	}
	public override void setGrid(Tile[,] grid)
	{
		gridPiece = grid;
	}

}
