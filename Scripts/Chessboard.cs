using Godot;
using System;

public partial class Chessboard : TileMapLayer
{
	//Why do we live just to suffer
	private Piece _selectedPiece;
	private Piece _selectedTile;
	private bool _isSelected;
	private bool _isWhiteTurn;
	public override void _PhysicsProcess(double delta)
	{
		Vector2 mapCords = Cordinates(); 
		GD.Print(mapCords);

		if (_isSelected && (_selectedPiece.IsWhite == _isWhiteTurn))
		{
			
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
