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
	[Export] protected AnimatedSprite2D sprite;
	[Export] protected ProgressBar bar;
	protected bool canSpawn = false;
	protected Tile[,] gridPiece;
	protected int spriteNum;
	
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
		spriteNum = sprite.Frame;
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

	public void setGri(Tile[,] grid, Vector2I CurrentPosition)
	{
		gridPiece = grid;
		CrrentPosition =  CurrentPosition;
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
					//GD.Print(moveResource.closest, pieceType, cur.getSelectedPiece(), CrrentPosition);
					Vector2I curp = CrrentPosition + temp;
					float xmov = moveResource.closest.X - curp.X;
					float ymov = moveResource.closest.Y - curp.Y;
					float dist = Mathf.Sqrt((ymov*ymov)+(xmov*xmov));
					//The pawn is so special bro
					if ((cur.getSelectedPiece() != pieceType) && moveResource.pawny)
					{
						//GD.Print("Does This work?");
						//cur.DamagePiece(Damage);
						Spawnables spawnings = spawning.Instantiate<Spawnables>();
						
						//GD.Print(moveResource.closest, curp);
						float tan = (Mathf.Atan2(ymov, xmov));
						bool black = pieceType == PieceType.Black;
						float tim = (dist/spawnings.getSpeed())*100f;
						//GD.Print("Time: " + tim + ", Tan: " + tan + ", YMov, XMov" + ymov + ", " + xmov);
						spawnings.setInstances(tim, spriteNum, black, tan, cur, Damage);
						AddChild(spawnings);
					}
				}
			}
		}
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

}
