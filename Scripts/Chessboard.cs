using Godot;
using System;
using UIProject.Scripts;

public partial class Chessboard : Node2D
{
	//Why do we live just to suffer
	//Update: everything that I made doesnt bloody work because tilemaplayers scene collections are **********
	//Update: On track to complete first sprint by second sprint, cant do anything about tilemaplayers but oh well
	//Update: Yo only a bit behind its just checking which shouldnt be too hard right                         right?

	private Tile _selectedTile;
	private bool _isSelected;
	private Tile[,] grid;
	
	
	//[Export] private PackedScene pScene;
	[Export] private PackedScene circleScene;
	[Export] private Node2D circles;
	[Export] private int width = 8;
	[Export] private int height = 8;
	[Export] private BoardArt boardArt;
	private King white, black;

	private enum Turn
	{
		SelectWhite,
		PlaceWhite,
		SelectBlack,
		PlaceBlack
	}
	private Turn turn  = Turn.SelectWhite;
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
				tile.GameOver += EndGame;
				if(tile.getPosition().Y >= 3)
				{
					tile.setPiece(tile.getPieceScene(), true);
				}
				else
				{
					tile.setPiece(tile.getPieceScene());
				}
			}
		}
		

		// ile = GetTile(3, 0);
	}

	private void OnTileClicked(Vector2I pos)
	{
		switch(turn)
		{
			case Turn.SelectWhite:
				Select(pos, Piece.PieceType.White);
				break;
			case Turn.PlaceWhite:
				Place(pos, Piece.PieceType.White);
				break;
			case Turn.SelectBlack:
				Select(pos, Piece.PieceType.Black);
				break;
			case Turn.PlaceBlack:
				Place(pos, Piece.PieceType.Black);
				break;

		}
	}

	private void Place(Vector2I pos, Piece.PieceType pieceType)
	{
		Tile tile = GetTile(pos.X, pos.Y);
		if (tile.getSelectedPiece() == _selectedTile.getSelectedPiece() && (_selectedTile.getPosition() != tile.getPosition()))
		{
			clearCircles();
			if(pieceType == Piece.PieceType.Black)
				turn = Turn.SelectBlack;
			else
				turn = Turn.SelectWhite;
			Select(pos, pieceType);
		}
		else if ((_selectedTile.getPosition() != tile.getPosition())&&(_selectedTile.canMove(tile.getPosition()))&&tile.hasPieceNot(_selectedTile.getSelectedPiece()))
		{
			if(pieceType == Piece.PieceType.White)
				turn = Turn.SelectBlack;
			else
				turn = Turn.SelectWhite;
			SetPieces(tile, _selectedTile.getPieceScene());
			_selectedTile.ClearPiece();
			_selectedTile = null;
			clearCircles();
		}
		GD.Print(turn);
	}

	private void clearCircles()
	{
		foreach (Node2D anim in circles.GetChildren())
		{
			anim.QueueFree();
		}
	}

	private void Select(Vector2I pos, Piece.PieceType pieceType)
	{
		Tile tile = GetTile(pos.X, pos.Y);
		GD.Print("Bruh" + tile + pieceType);
		_selectedTile = tile;
		bool hasMove = false;
		if(!tile.hasPieceNot())
		{
			if(pieceType==tile.getSelectedPiece())
			{
				tile.block(grid);
				for (int i = 0; i < 8; i++)
				{

					for(int j = 0; j < 8; j++)
					{	
						Tile e = GetTile(i, j);
						if ((_selectedTile.canMove(e.getPosition())) && e.hasPieceNot(_selectedTile.getSelectedPiece()))
						{
							hasMove = true;
							_selectedTile = tile;
							Node2D fry = circleScene.Instantiate() as Node2D;
							circles.AddChild(fry);
							fry.Position = (e.getPosition()*100);
						}
					}
				}
				if(!hasMove)
				{
					_selectedTile = null;
				}
				else
				{
					if(pieceType == Piece.PieceType.White)
						turn = Turn.PlaceWhite;
					else
						turn = Turn.PlaceBlack;
				}
			}
		}
	}

	private void SetPieces(Tile tile, PackedScene PieceScene)
	{
		//GD.Print(_selectedTile.getSelectedPiece());
		bool isBlack = (Piece.PieceType.Black == _selectedTile.getSelectedPiece());
		if(tile is null)
			return;
		//GD.Print("PieceType: " + isBlack);
		tile.setPiece(_selectedTile.getPieceScene(), isBlack);
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

	private void EndGame(int pieceType)
	{
		Piece.PieceType truth = (Piece.PieceType)pieceType;
		if (truth == Piece.PieceType.White)
		{
			GD.Print("E");
			
		}
		GetTree().Paused = true;
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
