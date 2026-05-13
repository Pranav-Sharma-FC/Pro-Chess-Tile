using Godot;
using System;
using System.Collections.Generic;
using System.Xml;
using Godot.Collections;

namespace UIProject.Scripts;

public abstract partial class Piece : CharacterBody2D
{
	protected Array<MovementResource> Movements = new Array<MovementResource>();
	private static readonly PackedScene spawning = GD.Load<PackedScene>("res://Scenes/spawnables.tscn");
	protected Vector2I CrrentPosition;
	
	protected Vector2I TrueCurPosition;
	[Export] protected AnimatedSprite2D sprite;
	[Export] protected ProgressBar bar;
	protected bool canSpawn = false;
	protected Tile[,] gridPiece;
	protected int spriteNum;
	protected Node2D boardNode;
	[Export] protected int manaNeed = 1;
	
	[Export] protected Timer timer;

	protected bool timerDone;
	//Spawnables
	[Export] protected int Health = 100;
	[Export] protected int Damage;
	protected int spawnableSpecial = 0;
	
	public enum PieceType
	{
		Nothing,
		White,
		Black
	}
//creates constant variables for different kinds of pieces 
	public int getMana()
	{
		return manaNeed;
	}

	protected PieceType pieceType = PieceType.White;

	public void blackPiece()
	{
		spriteNum = sprite.Frame;
		pieceType = PieceType.Black;
		sprite.Play("BlackPieces");
		sprite.Frame = spriteNum;
	}
// "sprite.Frame=spriteNum" Deals with the sprite frame going back to its default frame, that's why its there twice
	
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
//Collects the data to see what coordinate the selected piece is going to 

	public void damagePiece(int damage)
	{
		Health -= damage;
	}
//Uses the health variable as an integer, having the damage be subtract from it, with the original health exported earlier 

	public void setGri(Tile[,] grid, Vector2I CurrentPosition, Node2D board)
	{
		gridPiece = grid;
		CrrentPosition =  CurrentPosition;
		boardNode = board;
	}

	public void SpawnSpawnables(int pType, Vector2I curPos)
	{
		timer.WaitTime = 0.75;
		CrrentPosition = curPos;
		canSpawn = (this.pieceType == (PieceType)pType);
		PieceBlocking(CrrentPosition, gridPiece);
		//GD.Print("Is Connected" + canSpawn + curPos);
		//GD.Print(gridPiece[0,0].getSelectedPiece());
		if (canSpawn)
			timer.Start();
		else
			timer.Stop();
		foreach (MovementResource moveResource in Movements)
		{
			//GD.Print(moveResource.closest);
		}
	}

	public int getHealth()
	{
		return Health;
	}

	public override void _Process(double delta)
	{
		bar.Value = Health;

		if (canSpawn && timerDone)
		{
			//GD.Print(Health);
			//GD.Print("Spawn Done");
			timerDone = false;
			timer.Start();
			foreach (MovementResource moveResource in Movements)
			{
				Vector2I temp = new Vector2I(-1, -1);
				if (moveResource.closest != temp)
				{
					Tile cur = gridPiece[moveResource.closest.X, moveResource.closest.Y];
					Vector2I curp = CrrentPosition + temp;
					//GD.Print(moveResource.closest, pieceType, cur.getSelectedPiece(), CrrentPosition);
					//The pawn is so special bro
					if ((cur.getSelectedPiece() != pieceType) && moveResource.pawny && (cur.getSelectedPiece() != PieceType.Nothing))
					{
						spawnThings(cur, moveResource.closest, curp, Damage);
					}
				}
			}
		}
	}

	public void AttackAll(bool friend)
	{
		for(int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				Vector2I curp = CrrentPosition + new Vector2I(-1, -1);
				Vector2I next = new Vector2I(i+curp.X, j+curp.Y);
				if(next.X is >= 0 and < 8 && next.Y is >= 0 and < 8)
				{
					Tile cur = gridPiece[next.X, next.Y]; 
					if ((cur.getSelectedPiece() != PieceType.Nothing) && (next != curp))
					{
						if ((cur.getSelectedPiece() != pieceType) && !friend)
						{
							spawnThings(cur, next, curp, 50);
						}
						else if ((cur.getSelectedPiece() == pieceType) && friend)
						{
							spawnThings(cur, next, curp, 1);
						}
					}
				}
			}
		}
	}

	private void spawnThings(Tile cur, Vector2I spawnVec, Vector2I spawn, int dam)
	{
		float xmov = spawnVec.X - spawn.X;
		float ymov = spawnVec.Y - spawn.Y;
		float dist = Mathf.Sqrt((ymov*ymov)+(xmov*xmov));
		//GD.Print("Does This work?");
		//cur.DamagePiece(Damage);
		Spawnables spawnings = spawning.Instantiate<Spawnables>();
		spawnings.Position = this.Position;
		//GD.Print(moveResource.closest, curp);
		float tan = (Mathf.Atan2(ymov, xmov));
		bool black = pieceType == PieceType.Black;
		float tim = Mathf.Sqrt((2 * dist) / spawnings.getSpeed()) * 10f;
		//GD.Print(tan + "Tan");

		//GD.Print("Time: " + tim + ", Tan: " + tan + ", YMov, XMov" + ymov + ", " + xmov);
		GD.Print(spawnableSpecial);
		spawnings.setInstances(tim, spriteNum, black, tan, cur, dam, spawnableSpecial);
		boardNode.AddChild(spawnings);
	}
	
	public void TimerDone()
	{
		timerDone = true;
	}
	

//abstract class, spawnables 

	public abstract void SetPoints(Godot.Collections.Dictionary<string, int> Resources);
	public abstract void PieceBlocking(Vector2I CurrentPosition, Tile[,]  tiles);
	public abstract bool Move(Vector2I NextPosition,  Vector2I CurrentPosition);
	public abstract Godot.Collections.Dictionary<string, int> GivePiece();
	public abstract void ActivateSpecial();
}
