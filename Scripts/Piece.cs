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
	[Export] Texture2D blackRes;
	[Export] AnimatedSprite2D sprite;
	[Export] protected ProgressBar bar;
	protected bool canSpawn = false;
	protected Tile[,] gridPiece;
	
	[Export] protected Timer timer;

	protected bool timerDone;
	//Spawnables
	[Export] protected int Health = 100;
	[Export] protected int Damage;
	
	public enum PieceType
	{
		Nothing,
		White,
		Black
	}


	protected PieceType pieceType = PieceType.White;

	public void blackPiece()
	{
		int spriteNum = sprite.Frame;
		pieceType = PieceType.Black;
		sprite.Play("BlackPieces");
		sprite.Frame = spriteNum;
	}
	
	public PieceType returnType()
	{
		return pieceType;
	}
	
	protected Vector2I FindSlope(int x, int y)
	{
		int xNext = 0;
		int yNext = 0;
		if (x > 0)
		{
			xNext = 1;
		}
		else if (x < 0)
		{
			xNext = -1;
		}

		if (y > 0)
		{
			yNext = 1;	
		}
		else if (y < 0)
		{
			yNext = -1;
		}
		
		return new Vector2I(xNext, yNext);
	}

	public void damagePiece(int damage)
	{
		Health -= damage;
	}

	public void setGri(Tile[,] grid)
	{
		gridPiece = grid;
	}
	

//abstract class, spawnables 

	public abstract void SetPoints(Godot.Collections.Dictionary<string, int> Resources);
	public abstract bool PieceBlocking(Vector2I CurrentPosition, Tile[,]  tiles);
	public abstract bool Move(Vector2I NextPosition,  Vector2I CurrentPosition);
	public abstract Godot.Collections.Dictionary<string, int> GivePiece();
	public abstract void SpawnSpawnables(int pieceType);
	public abstract void setGrid(Tile[,] grid);

}
