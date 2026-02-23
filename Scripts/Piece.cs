using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public abstract partial class Piece : CharacterBody2D
{
	[Export] protected Chessboard Chessboard;
	[Export] protected PackedScene PieceScene;
	[Export] protected Vector2[] Points;
	protected Vector2 CurrentPosition;
	public bool IsWhite;
	
	public override void _PhysicsProcess(double delta)
	{
		
	}

	protected abstract void SetPoints();
}
