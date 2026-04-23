using Godot;

public partial class MovementResource : Resource
{
	[Export] public int xmov, ymov;
	[Export] public Vector2I closest = new Vector2I(-1, -1);

	public void setValues(int x, int y)
	{
		xmov = x;
		ymov = y;
	}
}
