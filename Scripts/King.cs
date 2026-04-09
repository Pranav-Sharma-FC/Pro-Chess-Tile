using Godot;

namespace UIProject.Scripts;

public partial class King : Piece
{
	[Signal]
	public delegate void GameOverEventHandler(int pieceType);
	public override void PieceBlocking(Vector2I CurrentPosition, Tile[,]  tiles)
	{
	}

	public bool canCapture()
	{
		return true;
	}
	protected override void SetPoints()
	{
		
	}

	public override void GivePiece()
	{
		
	}

	public void capturedKing()
	{
		EmitSignal(SignalName.GameOver, (int)pieceType);
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
		
		GD.Print(moveFlag);
		return moveFlag;
	}
}
