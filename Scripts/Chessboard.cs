using Godot;
using System;

public partial class Chessboard : Node2D
{
	//Why do we live just to suffer
	//Update: everything that I made doesnt bloody work because tilemaplayers scene collections are **********
	private Piece _selectedPiece;
	private bool _isSelected;
	private Tile[,] grid;
	
	[Export] private PackedScene pScene;

	[Export] private int width = 8;
	[Export] private int height = 8;
	[Export] private BoardArt boardArt;
	
	//Ready Script improved on by GPT to make easier tiles
	public override void _Ready()
	{
		boardArt.OnBoardArrived += OnTileClicked;
		grid = new Tile[width, height];
		GD.Print("Sup");
		foreach (Node2D child in GetChildren())
		{
			if (child is Tile tile)
			{
				Vector2I pos = tile.getPosition();
				grid[pos.X-1, pos.Y-1] = tile;
				GD.Print(tile.getPosition());
				tile.Position *= new Vector2(50, 50);
			}
		}
	}

	private void OnTileClicked(Vector2I pos)
	{
		GD.Print(pos);
		pos -= new Vector2I(-1, -1);
		Tile tile = GetTile(pos.X, pos.Y);
		SetPieces(tile);
	}

	private void SetPieces(Tile tile)
	{
		tile.setPiece(pScene);
	}

	public Tile GetTile(int x, int y)
	{
		if (x < 0 || y < 0 || x >= width || y >= height)
			return null;

		return grid[x, y];
	}

	/*private Vector2 Cordinates()
	{
		//Improved by Chatgpt
		Vector2 mouseWorldPos = GetGlobalMousePosition();
		Vector2I cell = LocalToMap(ToLocal(mouseWorldPos));
		//Hi
		return cell;
	}
	public void SetSelectedTile(Piece tile)
	{
		if (_selectedPiece.Move(tile))
		{
			Vector2I cellCord = LocalToMap(tile.Position);
			SetCell(cellCord, 1);
		}
	}
	
	*/
	public void SetSelectedPiece(Piece piece)
	{
		_selectedPiece = piece;
	}


	
}
