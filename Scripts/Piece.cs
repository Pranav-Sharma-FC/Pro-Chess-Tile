using Godot;
using System;
using System.Collections.Generic;
using System.Xml;
using Godot.Collections;

public abstract partial class Piece : CharacterBody2D
{
	public Chessboard ChessBoard;
	[Export] protected PackedScene PieceScene;
	[Export] protected Vector2[] Points;
	protected Vector2 CurrentPosition;
	protected enum PieceType
	{
		Nothing,
		White,
		Black
	}
	private PieceType pieceType = PieceType.Nothing;
	
	public override void _PhysicsProcess(double delta)
	{
		
	}

	protected abstract void SetPoints();
	public abstract bool Move(Piece tile);
	public abstract void GivePiece();
}
