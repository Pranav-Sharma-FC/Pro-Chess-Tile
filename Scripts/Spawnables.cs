using Godot;

namespace UIProject.Scripts;

public partial class Spawnables : CharacterBody2D
{
	[Export] private float speed;
	[Export] private AnimatedSprite2D sprite;
	[Export] private Timer timeren;
	private float angle; 
	private Tile tilly;
	private int dama;

	public void setInstances(float time, int num, bool isBlack, float tan, Tile til, int damag)
	{
		if(isBlack)
			sprite.Play("BlackPieces");
		sprite.Frame = num;
		timeren.WaitTime = time;
		angle = tan;
		dama = damag;
		tilly = til;
		this.Rotation = tan;
	}

	public void deleteSpawnables()
	{
		tilly.DamagePiece(dama);
		this.QueueFree();
	}

	public override void _Process(double delta)
	{
		Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		Position += direction * speed * (float)delta;
	}

	public float getSpeed()
	{
		return speed;
	}
}
