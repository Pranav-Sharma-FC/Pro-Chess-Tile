using Godot;
namespace UIProject.Scripts;
using Godot.Collections;
public partial class King : Piece
{
	public override bool PieceBlocking(Vector2I CurrentPosition, Tile[,]  tiles)
	{
		return true;
	}
	public override void setGrid(Tile[,] grid)
	{
		gridPiece = grid;
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
	public override void SpawnSpawnables(int pieceType)
	{
	}
	public bool canCapture()
	{
		return true;
	}
	public PieceType capturedKing()
	{
		return pieceType;
	}
	//Logic to make sure piece can move there
	public override bool Move(Vector2I NextPosition, Vector2I CurrentPosition)
	{
		bool moveFlag = true;
		if(!(NextPosition.X >= (CurrentPosition.X - 1) && (NextPosition.X <= (CurrentPosition.X + 1))))
		{
			moveFlag = false;
		}
		if (!(NextPosition.Y >= (CurrentPosition.Y - 1) && (NextPosition.Y <= (CurrentPosition.Y + 1))))
		{
			moveFlag = false;
		}
		
		GD.Print("Lomg"+ moveFlag);
		return moveFlag;
	}
}
