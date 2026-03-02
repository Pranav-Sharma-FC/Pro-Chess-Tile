using Godot;
using System;

public partial class Chessboard : TileMapLayer
{
	//Why do we live just to suffer
	private Piece _selectedPiece;
	private bool _isSelected;
	//private bool _isWhiteTurn;
	public override void _Ready()
	{
		foreach (Node child in GetChildren())
		{
			if (child is Piece tile)
			{
				tile.ChessBoard = this;
			}
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 mapCords = Cordinates(); 
		GD.Print(mapCords);
	}

	public void SetSelectedPiece(Piece piece)
	{
		_selectedPiece = piece;
	}

	public void SetSelectedTile(Piece tile)
	{
		if (_selectedPiece.Move(tile))
		{
			Vector2I cellCord = LocalToMap(tile.Position);
			SetCell(cellCord, 1);
		}
	}

	private Vector2 Cordinates()
	{
		//Improved by Chatgpt
		Vector2 mouseWorldPos = GetGlobalMousePosition();
		Vector2I cell = LocalToMap(ToLocal(mouseWorldPos));
		//Hi
		return cell;
	}
	
	
}
