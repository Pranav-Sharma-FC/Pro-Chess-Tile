using Godot;

namespace UIProject.Scripts;

public partial class Spawnables : CharacterBody2D
{
	[Export] private float MaxSpeed = 1000f;
	[Export] private float Acceleration = 400f;

	private float _currentSpeed = 0f;
	[Export] private AnimatedSprite2D sprite;
	[Export] private Timer timeren;
	private float angle; 
	private Tile tilly;
	private int dama;
	private int spec;

	public void setInstances(float time, int num, bool isBlack, float tan, Tile til, int damag, int special)
	{
		if (isBlack)
			sprite.Play("BlackPieces");
		sprite.Frame = num;
		angle = tan;
		dama = damag;
		tilly = til;
		this.Rotation = tan + ((float)(Mathf.Pi) / 2f);
		spec = special;
		if (spec == 1 || spec == 2)
		{
			MaxSpeed *= 2f; 
			Acceleration *= 2f;
			time /= 2;
		}
		timeren.WaitTime = time;
}

	public void deleteSpawnables()
	{
		if (spec == 1) //Bishop
			tilly.AttackAlls(true);
		tilly.DamagePiece(dama);
		this.QueueFree();
	}

	public override void _Process(double delta)
	{
		Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		_currentSpeed = Mathf.MoveToward(_currentSpeed, MaxSpeed, Acceleration * (float)delta);

		// 2. Apply speed to the direction
		// Ensure direction is normalized (length of 1) to avoid speed spikes
		Velocity = direction.Normalized() * _currentSpeed;

		MoveAndSlide();
		
		if(tilly.getSelectedPiece() == Piece.PieceType.Nothing)
			QueueFree();
	}

	public float getSpeed()
	{
		return Acceleration;
	}
}
