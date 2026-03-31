using Godot;
using System;

public partial class Chessboard : Node2D
{
	//Why do we live just to suffer
	//Update: everything that I made doesnt bloody work because tilemaplayers scene collections are **********
	//Update: On track to complete first sprint by second sprint, cant do anything about tilemaplayers but oh well

	private Tile _selectedPiece;
	private bool _isSelected;
	private Tile[,] grid;
	
	[Export] private PackedScene pScene;

	[Export] private int width = 8;
	[Export] private int height = 8;
	[Export] private BoardArt boardArt;

	private enum Turn
	{
		Select,
		Place,
		Change
	}
	private Turn turn  = Turn.Select;
	//Ready Script improved on by GPT to make easier tiles, further enhanced by me
	public override void _Ready()
	{
		
		boardArt.OnBoardArrived += OnTileClicked;
		grid = new Tile[width, height];
		GD.Print("Sup");
		int i = 0;
		foreach (Node2D child in GetChildren())
		{
			if (child is Tile tile)
			{
				i++;
				GD.Print("I: " + i);
				if(i%8==0)
				{
					tile.setPiece(pScene);
				}
				Vector2I pos = tile.getPosition();
				grid[pos.X-1, pos.Y-1] = tile;
				GD.Print(tile.getPosition());
				tile.Position *= new Vector2(100, 100);
				tile.Position -= new Vector2(50, 50);
			}
		}
	}

	private void OnTileClicked(Vector2I pos)
	{
		switch(turn)
		{
			case Turn.Select:
				Select(pos);
				break;
			case Turn.Place:
				Place(pos);
				break;

		}
	}

	private void Place(Vector2I pos)
	{
		Tile tile = GetTile(pos.X, pos.Y);
		GD.Print("Placing");
		if (tile.getPosition() != _selectedPiece.getPosition())//(_selectedPiece.canMove(tile.getPosition(), _selectedPiece.getPosition()))
		{
			SetPieces(tile, pScene);
			_selectedPiece.ClearPiece();
			_selectedPiece = null;
			turn = Turn.Select;
			GD.Print("Placed");

		}
	}

	private void Select(Vector2I pos)
	{
		GD.Print("Selecting");
		GD.Print(pos);
		Tile tile = GetTile(pos.X, pos.Y);
		if (tile.hasPiece())
		{
			_selectedPiece = tile;
			turn = Turn.Place;
			GD.Print("Selected");
		}
	}

	private void SetPieces(Tile tile, PackedScene PieceScene)
	{
		if(tile is null)
			GD.Print("E");
		tile.setPiece(pScene);
	}

	public Tile GetTile(int x, int y)
	{
		if (x < 0 || y < 0 || x >= width || y >= height)
		{
			return null;
	}
		GD.Print(grid[x, y]);
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


	
}
