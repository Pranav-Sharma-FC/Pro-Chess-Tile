using Godot;
using System;
using System.Globalization;
using UIProject.Scripts;

public partial class Chessboard : Node2D
{
	//Why do we live just to suffer
	//Update: everything that I made doesnt bloody work because tilemaplayers scene collections are **********
	//Update: On track to complete first sprint by second sprint, cant do anything about tilemaplayers but oh well
	//Update: Yo only a bit behind its just checking which shouldnt be too hard right                         right?
	//Update: Pranav's code is incomprehensible
	//Update: ^Skill Issue^ && Also Special Abilities hooray (more exceptions)

	private Tile _selectedTile;
	private bool _isSelected;
	private Tile[,] grid;
	
	[Signal]
	public delegate void SpawnSwitchEventHandler(int pieceType);
	// allows SpawnSwitchEventHandler to be used and altered across scripts
	
	//[Export] private PackedScene pScene;
	[Export] private PackedScene circleScene;
	[Export] private Node2D circles;
	[Export] private int width = 8;
	[Export] private int height = 8;
	[Export] private BoardArt boardArt;
	private int pieceTypeM;
	private King white, black;
	private bool special = false;
	
	//Timer Variables
	[Export] private Button _specialButton;
	private int _whiteMana = 100, _blackMana = 100;
	private double _whiteTim, _blackTim;
	[Export] private Timer _whiteTimer, _blackTimer;
	[Export] private Label _whiteLab, _blackLab;
	private enum Turn
	{
		SelectWhite,
		PlaceWhite,
		SelectBlack,
		PlaceBlack
	}
	//informs the code who's turn it is
	private Turn turn  = Turn.SelectWhite;
	
	//Ready Script improved on by GPT to make easier tiles, further enhanced by me
	public override void _Ready()
	{
		_whiteTim = 590.0;
		_blackTim = 590.0;
		_blackTimer.Paused = true;
		_whiteTimer.Paused = false;
		boardArt.OnBoardArrived += OnTileClicked;
		grid = new Tile[width, height];
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
				this.SpawnSwitch += tile.switchSpawnables;
				tile.Death += Fallen;
				
				if(tile.getPosition().Y >= 4)
				{
					tile.setPiece(tile.getPieceScene(), true);
				}
				else
				{
					tile.setPiece(tile.getPieceScene());
				}
				tile.gridPiece(grid);
			}
		}
		

		// ile = GetTile(3, 0);
	}

	public override void _Process(double delta)
	{
		if (_whiteTimer.TimeLeft < _whiteTim)
		{
			_whiteTim -= 30;
			_whiteMana++;
		}
		else if (_blackTimer.TimeLeft < _blackTim)
		{
			_blackTim -= 30;
			_blackMana++;
		}
		_whiteLab.Text = _whiteTimer.TimeLeft.ToString(CultureInfo.InvariantCulture) + " Mana: "+ _whiteMana;
		_blackLab.Text = _blackTimer.TimeLeft.ToString(CultureInfo.InvariantCulture)+ " Mana: " + _blackMana;
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

	public void SpecialAccept()
	{
		special = true;
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
		else if (special)
		{
			//GD.Print("Special Deal Place");
			dealWithSpecials(pos, pieceType);
			special = false;
		}
		else if ((_selectedTile.getPosition() != tile.getPosition())&&(_selectedTile.canMove(tile.getPosition()))&&tile.hasPieceNot(_selectedTile.getSelectedPiece()) && !special)
		{
			//here is where you need you signal, stop old spawnables, start new, check turn 
			SetPieces(tile, _selectedTile.getPieceScene());
			tile.SetPoints(_selectedTile.GivePiece());
			_selectedTile.ClearPiece();
			_selectedTile = null;
			clearCircles();
			foreach (Node2D child in GetChildren())
			{
				if (child is Tile tiles)
				{
					tiles.gridPiece(grid); 
				}
			}
			SwitchTurns(pieceType);
			_specialButton.Disabled = true;
		}
		//Gd.Print(turn);
	}

	private void SwitchTurns(Piece.PieceType pieceType)
	{
		if (pieceType == Piece.PieceType.White)
		{
			turn = Turn.SelectBlack;
			pieceTypeM = (int)Piece.PieceType.White;
			_whiteTimer.Paused = true;
			_blackTimer.Paused = false;
			EmitSignal(SignalName.SpawnSwitch,pieceTypeM);
		}
		else
		{
			turn = Turn.SelectWhite;
			pieceTypeM = (int)Piece.PieceType.Black;
			_whiteTimer.Paused = false;
			_blackTimer.Paused = true;
			EmitSignal(SignalName.SpawnSwitch, pieceTypeM);
		}
	}

	private void dealWithSpecials(Vector2I pos, Piece.PieceType pieceType)
	{
		//GD.Print("rook");
		//GD.Print(pieceType==Piece.PieceType.Black,_blackMana >= _selectedTile.getPieceMana());

		clearCircles();
		_selectedTile.specialActivation();
		SwitchTurns(pieceType);
		_specialButton.Disabled = true;
		if((pieceType==Piece.PieceType.Black))
			_blackMana -= _selectedTile.getPieceMana();
		else if ((pieceType==Piece.PieceType.White)) //Mana Dealing
		{
			_whiteMana -= _selectedTile.getPieceMana();
		}
	}

	
	//Implenet death clear circles
	private void Fallen()
	{
		foreach (Node2D child in GetChildren())
		{
			if (child is Tile tiles)
			{
				tiles.gridPiece(grid, isFallen:true); 
			}
		}
		//if(_selectedTile.getPosition() == )
		EmitSignal(SignalName.SpawnSwitch, pieceTypeM);
		foreach (Node2D child in GetChildren())
		{
			if (child is Tile tiles)
			{
				tiles.gridPiece(grid, isFallen:true); 
			}
		}
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
		//Gd.Print("Bruh" + tile + pieceType);
		_selectedTile = tile;
		bool hasMove = false;
		if(!tile.hasPieceNot())
		{
			//Gd.Print("King Please");
			if(pieceType==tile.getSelectedPiece())
			{
				//Gd.Print("King Please");
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
					bool canP = pieceType == _selectedTile.getSelectedPiece();
					bool whiteMana = (pieceType == Piece.PieceType.White) &&
									 (_whiteMana >= _selectedTile.getPieceMana());
					bool blackMana = (pieceType == Piece.PieceType.Black) && 
									 (_blackMana >= _selectedTile.getPieceMana());
					_specialButton.Disabled = !_selectedTile.canGetMana() && (whiteMana || blackMana) && canP;
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
			//Gd.Print("E");
			
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
