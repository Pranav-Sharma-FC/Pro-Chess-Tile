using Godot;

public partial class MovementResource : Resource
{
	[Export] int xmov, ymov;
	[Export] Vector2I closest;
}
