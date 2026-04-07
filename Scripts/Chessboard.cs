using Godot;
using System;

public partial class Chessboard : Node2D
{
	//Why do we live just to suffer
	//Update: everything that I made doesnt bloody work because tilemaplayers scene collections are **********
	//Update: On track to complete first sprint by second sprint, cant do anything about tilemaplayers but oh well

	private Tile _selectedTile;
	private bool _isSelected;
	private Tile[,] grid;
	
	
	[Export] private PackedScene pScene;
	[Export] private PackedScene circleScene;
	[Export] private Node2D circles;
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
		int i = 0;
		foreach (Node2D child in GetChildren())
		{
			if (child is Tile tile)
			{
				
				Vector2I pos = tile.getPosition();
				grid[pos.X-1, pos.Y-1] = tile;
				tile.Position *= new Vector2(100, 100);
				tile.Position -= new Vector2(50, 50);
			}
		}
		foreach (Node2D child in GetChildren())
		{
			if (child is Tile tile)
			{
				i++;
				if(i<=8)
				{
					tile.setPiece(pScene);
				}
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
		if ((_selectedTile.canMove(tile.getPosition()))&&tile.hasPieceNot(_selectedTile.getSelectedPiece()))
		{
			SetPieces(tile, pScene);
			_selectedTile.ClearPiece();
			_selectedTile = null;
			turn = Turn.Select;
			foreach (Node2D anim in circles.GetChildren())
			{
				anim.QueueFree();
			}
		}
	}

	private void Select(Vector2I pos)
	{
		Tile tile = GetTile(pos.X, pos.Y);
		_selectedTile = tile;
		turn = Turn.Place;
		_selectedTile.block(grid);
		for (int i = 0; i < 8; i++)
		{

			for(int j = 0; j < 8; j++)
			{	
				Tile e = GetTile(i, j);
				if ((_selectedTile.canMove(e.getPosition())) && e.hasPieceNot(_selectedTile.getSelectedPiece()))
				{
					Node2D fry = circleScene.Instantiate() as Node2D;
					circles.AddChild(fry);
					fry.Position = (e.getPosition()*100);
				}
			}
		}
	}

	private void SetPieces(Tile tile, PackedScene PieceScene)
	{
		if(tile is null)
			return;
		tile.setPiece(pScene);
	}

	public Tile GetTile(int x, int y)
	{
		if (x < 0 || y < 0 || x >= width || y >= height)
		{
			return null;
	}
		//GD.Print(grid[x, y]);
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
		if (_selectedTile.Move(tile))
		{
			Vector2I cellCord = LocalToMap(tile.Position);
			SetCell(cellCord, 1);
		}
	}
	
	*/


	
}
