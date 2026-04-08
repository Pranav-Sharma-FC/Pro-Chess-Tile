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
	protected Vector2I CrrentPosition;
	[Export] Texture2D whitePawn;
	
	public enum PieceType
	{
		Nothing,
		White,
		Black
	}
	private PieceType pieceType = PieceType.White;

	public PieceType returnType()
	{
		return pieceType;
	}

	protected abstract void SetPoints();
	public abstract void PieceBlocking(Vector2I CurrentPosition, Tile[,]  tiles);
	public abstract bool Move(Vector2I NextPosition,  Vector2I CurrentPosition);
	public abstract void GivePiece();
	
}
