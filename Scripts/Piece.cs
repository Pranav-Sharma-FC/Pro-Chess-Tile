using Godot;
using System;

public partial class Piece : CharacterBody2D
{
	[Export] private PackedScene pieceScene;
	[Export] private Vector2 pieceCord;
}
